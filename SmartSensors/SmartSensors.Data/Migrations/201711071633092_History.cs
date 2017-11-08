namespace SmartSensors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class History : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.History",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpdateDate = c.DateTime(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.History");
        }
    }
}
