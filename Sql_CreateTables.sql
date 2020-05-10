CREATE TABLE [dbo].[WoodREcords]
(
	[recordId] INT NOT NULL IDENTITY, 
	[woodId] INT NOT NULL, 
	[treeId] INT NOT NULL,
	[x] INT NOT NULL,
	[y] INT NOT NULL,
	PRIMARY KEY CLUSTERED([recordId] ASC)
);
CREATE TABLE [dbo].[MonkeyRecords]
(
	[recordId] INT NOT NULL IDENTITY,
	[monkeyId] INT NOT NULL,
	[monkeyName] NVARCHAR(40) NULL,
	[woodID] INT NOT NULL,
	[seqnr] INT NOT NULL,
	[treeId] INT NOT NULL,
	[x] INT NOT NULL,
	[y] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([recordId] ASC)
	);

CREATE TABLE [dbo].[logs]
(
	[Id] INT NOT NULL IDENTITY,
	[woodId] INT NOT NULL,
	[monkeyId] INT NOT NULL,
	[message] NVARCHAR(200) NOT NULL,
	);