CREATE TABLE [TypeA].[t_TypeASecondLevel] (
    [SecondLevelObjectBaseId] INT            NOT NULL,
    [TypeASecond_Property4]   NVARCHAR (MAX) NULL,
    [TypeASecond_Property5]   NVARCHAR (MAX) NULL,
    [TypeASecond_Property6]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TypeA.t_TypeASecondLevel] PRIMARY KEY CLUSTERED ([SecondLevelObjectBaseId] ASC),
    CONSTRAINT [FK_TypeA.t_TypeASecondLevel_Core.t_SecondLevelObjectBase_SecondLevelObjectBaseId] FOREIGN KEY ([SecondLevelObjectBaseId]) REFERENCES [Core].[t_SecondLevelObjectBase] ([SecondLevelObjectBaseId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SecondLevelObjectBaseId]
    ON [TypeA].[t_TypeASecondLevel]([SecondLevelObjectBaseId] ASC);

