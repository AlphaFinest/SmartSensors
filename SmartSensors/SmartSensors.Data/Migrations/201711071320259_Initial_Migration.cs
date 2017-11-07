namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "Owner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sensors", "Owner_Id");
            AddForeignKey("dbo.Sensors", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Sensors", new[] { "Owner_Id" });
            DropColumn("dbo.Sensors", "Owner_Id");
        }
    }
}
