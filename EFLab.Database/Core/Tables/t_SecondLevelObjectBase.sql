CREATE TABLE [Core].[t_SecondLevelObjectBase] (
    [SecondLevelObjectBaseId] INT            IDENTITY (1, 1) NOT NULL,
    [Identifier]              NVARCHAR (10)  NULL,
    [SecondBase_Property1]    NVARCHAR (200) NULL,
    [SecondBase_Property2]    NVARCHAR (200) NULL,
    [SecondBase_Property3]    NVARCHAR (200) NULL,
    [Parent_TopLevelObjectId] INT            NULL,
    CONSTRAINT [PK_Core.t_SecondLevelObjectBase] PRIMARY KEY CLUSTERED ([SecondLevelObjectBaseId] ASC),
    CONSTRAINT [FK_Core.t_SecondLevelObjectBase_Core.t_TopLevelObject_Parent_TopLevelObjectId] FOREIGN KEY ([Parent_TopLevelObjectId]) REFERENCES [Core].[t_TopLevelObject] ([TopLevelObjectId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Parent_TopLevelObjectId]
    ON [Core].[t_SecondLevelObjectBase]([Parent_TopLevelObjectId] ASC);

