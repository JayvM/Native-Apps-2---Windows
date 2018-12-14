namespace NativeAppsII_Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actie1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Acties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beschrijving = c.String(),
                        Image = c.String(),
                        GeldigTot = c.DateTime(nullable: false),
                        OndernemingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ondernemings", t => t.OndernemingId, cascadeDelete: true)
                .Index(t => t.OndernemingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acties", "OndernemingId", "dbo.Ondernemings");
            DropIndex("dbo.Acties", new[] { "OndernemingId" });
            DropTable("dbo.Acties");
        }
    }
}
