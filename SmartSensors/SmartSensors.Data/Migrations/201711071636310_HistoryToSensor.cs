namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryToSensor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.History", "SensorId", c => c.Int(nullable: false));
            CreateIndex("dbo.History", "SensorId");
            AddForeignKey("dbo.History", "SensorId", "dbo.Sensors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.History", "SensorId", "dbo.Sensors");
            DropIndex("dbo.History", new[] { "SensorId" });
            DropColumn("dbo.History", "SensorId");
        }
    }
}
