CREATE TABLE [TypeC].[t_TypeCSecondLevel] (
    [SecondLevelObjectBaseId] INT            NOT NULL,
    [TypeCSecond_Property4]   NVARCHAR (MAX) NULL,
    [TypeCSecond_Property5]   NVARCHAR (MAX) NULL,
    [TypeCSecond_Property6]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TypeC.t_TypeCSecondLevel] PRIMARY KEY CLUSTERED ([SecondLevelObjectBaseId] ASC),
    CONSTRAINT [FK_TypeC.t_TypeCSecondLevel_Core.t_SecondLevelObjectBase_SecondLevelObjectBaseId] FOREIGN KEY ([SecondLevelObjectBaseId]) REFERENCES [Core].[t_SecondLevelObjectBase] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SecondLevelObjectBaseId]
    ON [TypeC].[t_TypeCSecondLevel]([SecondLevelObjectBaseId] ASC);

