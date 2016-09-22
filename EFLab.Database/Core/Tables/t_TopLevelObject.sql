CREATE TABLE [Core].[t_TopLevelObject] (
    [TopLevelObjectId]   INT            IDENTITY (1, 1) NOT NULL,
    [TopLevel_Property1] NVARCHAR (MAX) NULL,
    [TopLevel_Property2] NVARCHAR (MAX) NULL,
    [TopLevel_Property3] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Core.t_TopLevelObject] PRIMARY KEY CLUSTERED ([TopLevelObjectId] ASC)
);

