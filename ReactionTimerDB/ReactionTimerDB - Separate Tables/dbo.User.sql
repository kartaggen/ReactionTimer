CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [firstName] NVARCHAR (50)  NOT NULL,
    [lastName]  NVARCHAR (50)  NOT NULL,
    [username]  NVARCHAR (50)  NOT NULL,
    [password]  NVARCHAR (256) NOT NULL,
    [regDate]   SMALLDATETIME  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

