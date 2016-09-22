CREATE TABLE [TypeB].[t_Object1] (
    [TypeBObject1Id]                                 INT            IDENTITY (1, 1) NOT NULL,
    [TypeBObject1_Property1]                         NVARCHAR (MAX) NULL,
    [TypeBObject1_Property2]                         NVARCHAR (MAX) NULL,
    [TypeBObject1_Property3]                         NVARCHAR (MAX) NULL,
    [TypeBSecondLevelObject_SecondLevelObjectBaseId] INT            NULL,
    CONSTRAINT [PK_TypeB.t_Object1] PRIMARY KEY CLUSTERED ([TypeBObject1Id] ASC),
    CONSTRAINT [FK_TypeB.t_Object1_TypeB.t_TypeBSecondLevel_TypeBSecondLevelObject_SecondLevelObjectBaseId] FOREIGN KEY ([TypeBSecondLevelObject_SecondLevelObjectBaseId]) REFERENCES [TypeB].[t_TypeBSecondLevel] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TypeBSecondLevelObject_SecondLevelObjectBaseId]
    ON [TypeB].[t_Object1]([TypeBSecondLevelObject_SecondLevelObjectBaseId] ASC);

