namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Many : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSensor",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        SensorRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.SensorRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorRefId, cascadeDelete: false)
                .Index(t => t.UserRefId)
                .Index(t => t.SensorRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSensor", "SensorRefId", "dbo.Sensors");
            DropForeignKey("dbo.UserSensor", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSensor", new[] { "SensorRefId" });
            DropIndex("dbo.UserSensor", new[] { "UserRefId" });
            DropTable("dbo.UserSensor");
        }
    }
}
