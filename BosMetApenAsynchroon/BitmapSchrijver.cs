using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BosMetApenAsynchroon
{
    class BitmapSchrijver
    {
        public async Task maakBitMap(Bos bos, string path)
        {
            Bitmap bm = new Bitmap(bos.Xmax*10, bos.Ymax*10);
            Graphics graphics = Graphics.FromImage(bm);
            Pen pen = new Pen(Color.Green, 1);
            foreach(Boom boom in bos.Bomen)
            {
                graphics.DrawEllipse(pen, boom.X*10-2, boom.Y*10-2, 4, 4);
            }
            foreach(Aap aap in bos.Apen)
            {
                Random rand = new Random();
                int r = rand.Next(256);
                int g = rand.Next(256);
                int b = rand.Next(256);
                Color c = Color.FromArgb(r, g, b);
                Pen linePen = new Pen(c);
                Brush brush = new SolidBrush(c);
                graphics.FillEllipse(brush, aap.bezochteBomen[0].X *10-2, aap.bezochteBomen[0].Y*10-2, 4, 4);
                for(int x=1; x < aap.bezochteBomen.Count; x++)
                {
                    graphics.DrawLine(linePen, aap.bezochteBomen[x - 1].X*10, aap.bezochteBomen[x - 1].Y*10, aap.bezochteBomen[x].X*10, aap.bezochteBomen[x].Y*10);
                }
            }
            bm.Save(Path.Combine(path, bos.Bomen.ToString() + "_escapeRoutes.jpg"), ImageFormat.Jpeg);
        }

        
    }
}
