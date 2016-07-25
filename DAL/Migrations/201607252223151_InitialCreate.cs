namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Core.t_SecondLevelObjectBase",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false, identity: true),
                        Identifier = c.String(maxLength: 10),
                        SecondBase_Property1 = c.String(maxLength: 200),
                        SecondBase_Property2 = c.String(maxLength: 200),
                        SecondBase_Property3 = c.String(maxLength: 200),
                        Parent_TopLevelObjectId = c.Int(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId)
                .ForeignKey("Core.t_TopLevelObject", t => t.Parent_TopLevelObjectId)
                .Index(t => t.Parent_TopLevelObjectId);
            
            CreateTable(
                "Core.t_TopLevelObject",
                c => new
                    {
                        TopLevelObjectId = c.Int(nullable: false, identity: true),
                        TopLevel_Property1 = c.String(),
                        TopLevel_Property2 = c.String(),
                        TopLevel_Property3 = c.String(),
                    })
                .PrimaryKey(t => t.TopLevelObjectId);
            
            CreateTable(
                "TypeA.t_Object1",
                c => new
                    {
                        TypeAObject1Id = c.Int(nullable: false, identity: true),
                        TypeAObject1_Property1 = c.String(maxLength: 200),
                        TypeAObject1_Property2 = c.String(),
                        TypeAObject1_Property3 = c.String(),
                        TypeASecondLevelObject_SecondLevelObjectBaseId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeAObject1Id)
                .ForeignKey("TypeA.t_TypeASecondLevel", t => t.TypeASecondLevelObject_SecondLevelObjectBaseId)
                .Index(t => t.TypeASecondLevelObject_SecondLevelObjectBaseId);
            
            CreateTable(
                "TypeB.t_Object1",
                c => new
                    {
                        TypeBObject1Id = c.Int(nullable: false, identity: true),
                        TypeBObject1_Property1 = c.String(),
                        TypeBObject1_Property2 = c.String(),
                        TypeBObject1_Property3 = c.String(),
                        TypeBSecondLevelObject_SecondLevelObjectBaseId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeBObject1Id)
                .ForeignKey("TypeB.t_TypeBSecondLevel", t => t.TypeBSecondLevelObject_SecondLevelObjectBaseId)
                .Index(t => t.TypeBSecondLevelObject_SecondLevelObjectBaseId);
            
            CreateTable(
                "TypeA.t_TypeASecondLevel",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false),
                        TypeASecond_Property4 = c.String(),
                        TypeASecond_Property5 = c.String(),
                        TypeASecond_Property6 = c.String(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId)
                .ForeignKey("Core.t_SecondLevelObjectBase", t => t.SecondLevelObjectBaseId)
                .Index(t => t.SecondLevelObjectBaseId);
            
            CreateTable(
                "TypeB.t_TypeBSecondLevel",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false),
                        TypeBSecond_Property4 = c.String(),
                        TypeBSecond_Property5 = c.String(),
                        TypeBSecond_Property6 = c.String(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId)
                .ForeignKey("Core.t_SecondLevelObjectBase", t => t.SecondLevelObjectBaseId)
                .Index(t => t.SecondLevelObjectBaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("TypeB.t_TypeBSecondLevel", "SecondLevelObjectBaseId", "Core.t_SecondLevelObjectBase");
            DropForeignKey("TypeA.t_TypeASecondLevel", "SecondLevelObjectBaseId", "Core.t_SecondLevelObjectBase");
            DropForeignKey("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId", "TypeB.t_TypeBSecondLevel");
            DropForeignKey("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId", "TypeA.t_TypeASecondLevel");
            DropForeignKey("Core.t_SecondLevelObjectBase", "Parent_TopLevelObjectId", "Core.t_TopLevelObject");
            DropIndex("TypeB.t_TypeBSecondLevel", new[] { "SecondLevelObjectBaseId" });
            DropIndex("TypeA.t_TypeASecondLevel", new[] { "SecondLevelObjectBaseId" });
            DropIndex("TypeB.t_Object1", new[] { "TypeBSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeA.t_Object1", new[] { "TypeASecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("Core.t_SecondLevelObjectBase", new[] { "Parent_TopLevelObjectId" });
            DropTable("TypeB.t_TypeBSecondLevel");
            DropTable("TypeA.t_TypeASecondLevel");
            DropTable("TypeB.t_Object1");
            DropTable("TypeA.t_Object1");
            DropTable("Core.t_TopLevelObject");
            DropTable("Core.t_SecondLevelObjectBase");
        }
    }
}
