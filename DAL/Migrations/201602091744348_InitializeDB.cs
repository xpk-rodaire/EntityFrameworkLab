namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SecondLevelObjectBases",
                c => new
                    {
                        SecondLevelObjectBaseId = c.Int(nullable: false, identity: true),
                        SecondBase_Property1 = c.String(maxLength: 200),
                        SecondBase_Property2 = c.String(),
                        SecondBase_Property3 = c.String(),
                        SecondLevelObjectId = c.Int(),
                        TypeASecond_Property4 = c.String(),
                        TypeASecond_Property5 = c.String(),
                        TypeASecond_Property6 = c.String(),
                        SecondLevelObjectId1 = c.Int(),
                        TypeBSecond_Property4 = c.String(),
                        TypeBSecond_Property5 = c.String(),
                        TypeBSecond_Property6 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Parent_TopLevelObjectId = c.Int(),
                    })
                .PrimaryKey(t => t.SecondLevelObjectBaseId)
                .ForeignKey("dbo.TopLevelObjects", t => t.Parent_TopLevelObjectId)
                .Index(t => t.Parent_TopLevelObjectId);
            
            CreateTable(
                "dbo.TopLevelObjects",
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
                .ForeignKey("dbo.SecondLevelObjectBases", t => t.TypeASecondLevelObject_SecondLevelObjectBaseId)
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
                .ForeignKey("dbo.SecondLevelObjectBases", t => t.TypeBSecondLevelObject_SecondLevelObjectBaseId)
                .Index(t => t.TypeBSecondLevelObject_SecondLevelObjectBaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("TypeB.t_Object1", "TypeBSecondLevelObject_SecondLevelObjectBaseId", "dbo.SecondLevelObjectBases");
            DropForeignKey("TypeA.t_Object1", "TypeASecondLevelObject_SecondLevelObjectBaseId", "dbo.SecondLevelObjectBases");
            DropForeignKey("dbo.SecondLevelObjectBases", "Parent_TopLevelObjectId", "dbo.TopLevelObjects");
            DropIndex("TypeB.t_Object1", new[] { "TypeBSecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("TypeA.t_Object1", new[] { "TypeASecondLevelObject_SecondLevelObjectBaseId" });
            DropIndex("dbo.SecondLevelObjectBases", new[] { "Parent_TopLevelObjectId" });
            DropTable("TypeB.t_Object1");
            DropTable("TypeA.t_Object1");
            DropTable("dbo.TopLevelObjects");
            DropTable("dbo.SecondLevelObjectBases");
        }
    }
}
