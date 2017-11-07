namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_History : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.History",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SensorId = c.Int(),
                        UpdateDate = c.DateTime(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.SensorId)
                .Index(t => t.SensorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.History", "SensorId", "dbo.Sensors");
            DropIndex("dbo.History", new[] { "SensorId" });
            DropTable("dbo.History");
        }
    }
}
