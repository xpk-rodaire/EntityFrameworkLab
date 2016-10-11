namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTopLevelObjectRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", "Core.t_TopLevelObject");
            DropIndex("Core.t_SecondLevelObjectBase", new[] { "Parent_TopLevelObjectId" });
            AlterColumn("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", c => c.Int(nullable: false));
            CreateIndex("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId");
            AddForeignKey("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", "Core.t_TopLevelObject", "TopLevelObjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", "Core.t_TopLevelObject");
            DropIndex("Core.t_SecondLevelObjectBase", new[] { "Parent_TopLevelObjectId" });
            AlterColumn("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", c => c.Int());
            CreateIndex("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId");
            AddForeignKey("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", "Core.t_TopLevelObject", "TopLevelObjectId");
        }
    }
}
