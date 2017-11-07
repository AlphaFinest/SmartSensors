namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sensor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        Url = c.String(maxLength: 100),
                        PollingInterval = c.Int(nullable: false),
                        MinRange = c.Int(nullable: false),
                        MaxRange = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Value = c.String(),
                        ValueType = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sensors");
        }
    }
}
