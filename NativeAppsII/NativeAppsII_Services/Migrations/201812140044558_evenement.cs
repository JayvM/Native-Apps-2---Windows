namespace NativeAppsII_Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class evenement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evenements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beschrijving = c.String(),
                        Plaats = c.String(),
                        Datum = c.DateTime(nullable: false),
                        OndernemingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ondernemings", t => t.OndernemingId, cascadeDelete: true)
                .Index(t => t.OndernemingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evenements", "OndernemingId", "dbo.Ondernemings");
            DropIndex("dbo.Evenements", new[] { "OndernemingId" });
            DropTable("dbo.Evenements");
        }
    }
}
