namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeC : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.TypeCObject1Id)
                .ForeignKey("TypeC.t_TypeCSecondLevel", t => t.TypeCSecondLevelObject_SecondLevelObjectBaseId)
                .Index(t => t.TypeCSecondLevelObject_SecondLevelObjectBaseId);
            
            CreateTable(
                "TypeC.t_TypeCSecondLevel",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false),
                        TypeCSecond_Property4 = c.String(),
                        TypeCSecond_Property5 = c.String(),
                        TypeCSecond_Property6 = c.String(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId)
                .ForeignKey("Core.t_SecondLevelObjectBase", t => t.SecondLevelObjectBaseId)
                .Index(t => t.SecondLevelObjectBaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("TypeC.t_TypeCSecondLevel", "SecondLevelObjectBaseId", "Core.t_SecondLevelObjectBase");
            DropForeignKey("TypeC.t_Object1", "TypeCSecondLevelObject_SecondLevelObjectBaseId", "TypeC.t_TypeCSecondLevel");
            DropIndex("TypeC.t_TypeCSecondLevel", new[] { "SecondLevelObjectBaseId" });
            DropIndex("TypeC.t_Object1", new[] { "TypeCSecondLevelObject_SecondLevelObjectBaseId" });
            DropTable("TypeC.t_TypeCSecondLevel");
            DropTable("TypeC.t_Object1");
        }
    }
}
