namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUrls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urls", "SensorUrl", c => c.String());
            DropColumn("dbo.Urls", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Urls", "Url", c => c.String());
            DropColumn("dbo.Urls", "SensorUrl");
        }
    }
}
