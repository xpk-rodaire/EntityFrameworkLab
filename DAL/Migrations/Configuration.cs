namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using EFLab.DAL.BizObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<EFLab.DAL.DbEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFLab.DAL.DbEntities context)
        {
            TopLevelObject top = new TopLevelObject();
            top.PopulateTest();

            context.TopLevelObject.Add(top);
        }
    }
}
