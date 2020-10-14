CREATE TABLE [dbo].[GameScore] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [userID]    INT           NOT NULL,
    [gameScore] FLOAT (53)    NOT NULL,
    [gameDate]  SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_GameScore] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([userID]) REFERENCES [dbo].[User] ([Id])
);

