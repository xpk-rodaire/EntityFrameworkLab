CREATE TABLE [TypeA].[t_Object1] (
    [TypeAObject1Id]                                 INT            IDENTITY (1, 1) NOT NULL,
    [TypeAObject1_Property1]                         NVARCHAR (200) NULL,
    [TypeAObject1_Property2]                         NVARCHAR (MAX) NULL,
    [TypeAObject1_Property3]                         NVARCHAR (MAX) NULL,
    [TypeASecondLevelObject_SecondLevelObjectBaseId] INT            NULL,
    CONSTRAINT [PK_TypeA.t_Object1] PRIMARY KEY CLUSTERED ([TypeAObject1Id] ASC),
    CONSTRAINT [FK_TypeA.t_Object1_TypeA.t_TypeASecondLevel_TypeASecondLevelObject_SecondLevelObjectBaseId] FOREIGN KEY ([TypeASecondLevelObject_SecondLevelObjectBaseId]) REFERENCES [TypeA].[t_TypeASecondLevel] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TypeASecondLevelObject_SecondLevelObjectBaseId]
    ON [TypeA].[t_Object1]([TypeASecondLevelObject_SecondLevelObjectBaseId] ASC);

