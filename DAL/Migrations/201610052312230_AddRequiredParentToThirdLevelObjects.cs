namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredParentToThirdLevelObjects : DbMigration
    {
        public override void Up()
        {
            DropIndex("TypeA.t_Object1", new[] { "TypeASecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeB.t_Object1", new[] { "TypeBSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeC.t_Object1", new[] { "TypeCSecondLevelObject_SecondLevelObjectBaseId" });
            AlterColumn("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId", c => c.Int(nullable: false));
            AlterColumn("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId", c => c.Int(nullable: false));
            AlterColumn("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId", c => c.Int(nullable: false));
            CreateIndex("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId");
            CreateIndex("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId");
            CreateIndex("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId");
        }
        
        public override void Down()
        {
            DropIndex("TypeC.t_Object1", new[] { "TypeCSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeB.t_Object1", new[] { "TypeBSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeA.t_Object1", new[] { "TypeASecondLevelObject_SecondLevelObjectBaseId" });
            AlterColumn("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId", c => c.Int());
            AlterColumn("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId", c => c.Int());
            AlterColumn("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId", c => c.Int());
            CreateIndex("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId");
            CreateIndex("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId");
            CreateIndex("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId");
        }
    }
}
