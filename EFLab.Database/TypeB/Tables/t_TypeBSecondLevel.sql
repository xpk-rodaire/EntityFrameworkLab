CREATE TABLE [TypeB].[t_TypeBSecondLevel] (
    [SecondLevelObjectBaseId] INT            NOT NULL,
    [TypeBSecond_Property4]   NVARCHAR (MAX) NULL,
    [TypeBSecond_Property5]   NVARCHAR (MAX) NULL,
    [TypeBSecond_Property6]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TypeB.t_TypeBSecondLevel] PRIMARY KEY CLUSTERED ([SecondLevelObjectBaseId] ASC),
    CONSTRAINT [FK_TypeB.t_TypeBSecondLevel_Core.t_SecondLevelObjectBase_SecondLevelObjectBaseId] FOREIGN KEY ([SecondLevelObjectBaseId]) REFERENCES [Core].[t_SecondLevelObjectBase] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SecondLevelObjectBaseId]
    ON [TypeB].[t_TypeBSecondLevel]([SecondLevelObjectBaseId] ASC);

