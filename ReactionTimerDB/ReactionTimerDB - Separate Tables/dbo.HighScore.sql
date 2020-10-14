CREATE TABLE [dbo].[HighScore] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [userID]    INT           NOT NULL,
    [highScore] FLOAT (53)    NOT NULL,
    [scoreDate] SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_HighScore] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([userID]) REFERENCES [dbo].[User] ([Id])
);

