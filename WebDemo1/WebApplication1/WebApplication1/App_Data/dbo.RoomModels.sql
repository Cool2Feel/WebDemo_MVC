CREATE TABLE [dbo].[RoomModels] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    [Peopel]    NVARCHAR (MAX) NULL,
    [Coustomer] NVARCHAR (MAX) NULL,
    [StartTime] DATETIME        NULL,
    [EndTime]   DATETIME       NULL,
    [Tips]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.RoomModels] PRIMARY KEY CLUSTERED ([ID] ASC)
);

