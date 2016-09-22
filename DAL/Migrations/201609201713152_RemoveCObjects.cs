namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCObjects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId", "TypeC.t_TypeCSecondLevel");
            DropForeignKey("TypeC.t_TypeCSecondLevel", "SecondLevelObjectBaseId", "Core.t_SecondLevelObjectBase");
            DropIndex("TypeC.t_Object1", new[] { "TypeCSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeC.t_TypeCSecondLevel", new[] { "SecondLevelObjectBaseId" });
            DropTable("TypeC.t_Object1");
            DropTable("TypeC.t_TypeCSecondLevel");
        }
        
        public override void Down()
        {
            CreateTable(
                "TypeC.t_TypeCSecondLevel",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false),
                        TypeCSecond_Property4 = c.String(),
                        TypeCSecond_Property5 = c.String(),
                        TypeCSecond_Property6 = c.String(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId);
            
            CreateTable(
                "TypeC.t_Object1",
                c => new
                    {
                        TypeCObject1Id = c.Int(nullable: false, identity: true),
                        TypeCObject1_Property1 = c.String(),
                        TypeCObject1_Property2 = c.String(),
                        TypeCObject1_Property3 = c.String(),
                        TypeCSecondLevelObject_SecondLevelObjectBaseId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeCObject1Id);
            
            CreateIndex("TypeC.t_TypeCSecondLevel", "SecondLevelObjectBaseId");
            CreateIndex("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId");
            AddForeignKey("TypeC.t_TypeCSecondLevel", "SecondLevelObjectBaseId", "Core.t_SecondLevelObjectBase", "SecondLevelObjectBaseId");
            AddForeignKey("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId", "TypeC.t_TypeCSecondLevel", "SecondLevelObjectBaseId");
        }
    }
}
