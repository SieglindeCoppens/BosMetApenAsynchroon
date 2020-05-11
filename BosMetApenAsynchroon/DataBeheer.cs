using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class DataBeheer
    {
        private string connectionString;

        public DataBeheer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public async Task VoegWoodRecordToe(Bos bos)
        {
            Console.WriteLine($"start toevoegen woodrecords voor bos {bos.BosId}");
            SqlConnection connection = getConnection();
            string queryWood = "INSERT INTO dbo.WoodRecords(woodId,treeId,x,y) VALUES(@woodId, @treeId, @x, @y)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@woodId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@treeId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@x", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@y", SqlDbType.Int));
                    command.CommandText = queryWood;
                        command.Parameters["@woodId"].Value = bos.BosId;
                        foreach(Boom boom in bos.Bomen)
                        {
                            command.Parameters["@treeId"].Value = boom.Id;
                            command.Parameters["@x"].Value = boom.X;
                            command.Parameters["@y"].Value = boom.Y;
                            command.ExecuteNonQuery();
                        }
                    

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            Console.WriteLine($"stop toevoegen woodrecords voor bos {bos.BosId}");
        }
        public async Task VoegMonkeyRecordToe(Bos bos)
        {
            Console.WriteLine($"start toevoegen monkeyrecords voor bos {bos.BosId}");
            SqlConnection connection = getConnection();
            string queryMonkey = "INSERT INTO dbo.MonkeyRecords(monkeyId, monkeyName, woodId, seqnr, treeId, x, y) VALUES(@monkeyId, @monkeyName, @woodId, @seqnr, @treeId, @x, @y)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@monkeyId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@monkeyName", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@woodId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@seqnr", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@treeId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@x", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@y", SqlDbType.Int));
                    command.CommandText = queryMonkey;

                        for(int aapTeller = 0; aapTeller < bos.Apen.Count; aapTeller++)
                        {
                            Aap aap = bos.Apen[aapTeller];
                            for(int boomTeller = 0; boomTeller < aap.bezochteBomen.Count; boomTeller++)
                            {
                                Boom boom = aap.bezochteBomen[boomTeller];
                                command.Parameters["@monkeyId"].Value = aap.Id;
                                command.Parameters["@monkeyName"].Value = aap.Naam;
                                command.Parameters["@woodId"].Value = bos.BosId;
                                command.Parameters["@seqnr"].Value = boomTeller;
                                command.Parameters["@treeId"].Value = boom.Id;
                                command.Parameters["@x"].Value = boom.X;
                                command.Parameters["@y"].Value = boom.Y;
                                command.ExecuteNonQuery();
                            }
                        }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
                Console.WriteLine($"stop toevoegen monkeyrecords voor bos {bos.BosId}");
            }

        }
        public async Task voegLogsToe(Bos bos)
        {
            Console.WriteLine($"start toevoegen logs voor bos {bos.BosId}");
            SqlConnection connection = getConnection();
            string queryLogs = "INSERT INTO dbo.logs(woodId, monkeyId, message) VALUES(@woodId, @monkeyId, @message)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@woodId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@monkeyId", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@message", SqlDbType.NVarChar));
                    command.CommandText = queryLogs;
                        for (int aapTeller = 0; aapTeller < bos.Apen.Count; aapTeller++)
                        {
                            Aap aap = bos.Apen[aapTeller];
                            for (int boomTeller = 0; boomTeller < aap.bezochteBomen.Count; boomTeller++)
                            {
                                Boom boom = aap.bezochteBomen[boomTeller];
                                command.Parameters["@monkeyId"].Value = aap.Id;
                                command.Parameters["@woodId"].Value = bos.BosId;
                                command.Parameters["@message"].Value = $"{aap.Naam} is now in tree {boom.Id} at location ({boom.X},{boom.Y})";
                                command.ExecuteNonQuery();
                            }
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            Console.WriteLine($"stop toevoegen logs voor bos {bos.BosId}");
        }

    }
}
