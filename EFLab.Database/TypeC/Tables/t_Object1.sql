CREATE TABLE [TypeC].[t_Object1] (
    [TypeCObject1Id]                                 INT            IDENTITY (1, 1) NOT NULL,
    [TypeCObject1_Property1]                         NVARCHAR (MAX) NULL,
    [TypeCObject1_Property2]                         NVARCHAR (MAX) NULL,
    [TypeCObject1_Property3]                         NVARCHAR (MAX) NULL,
    [TypeCSecondLevelObject_SecondLevelObjectBaseId] INT            NULL,
    CONSTRAINT [PK_TypeC.t_Object1] PRIMARY KEY CLUSTERED ([TypeCObject1Id] ASC),
    CONSTRAINT [FK_TypeC.t_Object1_TypeC.t_TypeCSecondLevel_TypeCSecondLevelObject_SecondLevelObjectBaseId] FOREIGN KEY ([TypeCSecondLevelObject_SecondLevelObjectBaseId]) REFERENCES [TypeC].[t_TypeCSecondLevel] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TypeCSecondLevelObject_SecondLevelObjectBaseId]
    ON [TypeC].[t_Object1]([TypeCSecondLevelObject_SecondLevelObjectBaseId] ASC);

