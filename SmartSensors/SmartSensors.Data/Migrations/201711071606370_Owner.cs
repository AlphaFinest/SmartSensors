namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Owner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Sensors", "OwnerId");
            AddForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Sensors", new[] { "OwnerId" });
            DropColumn("dbo.Sensors", "OwnerId");
        }
    }
}
