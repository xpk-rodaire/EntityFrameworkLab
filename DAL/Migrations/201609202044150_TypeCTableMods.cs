namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeCTableMods : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "TypeC.t_TypeCSecondLevel", newName: "t_TypeCSecondLevel2");
            MoveTable(name: "TypeC.t_TypeCSecondLevel2", newSchema: "TypeC2");
        }
        
        public override void Down()
        {
            MoveTable(name: "TypeC2.t_TypeCSecondLevel2", newSchema: "TypeC");
            RenameTable(name: "TypeC.t_TypeCSecondLevel2", newName: "t_TypeCSecondLevel");
        }
    }
}
