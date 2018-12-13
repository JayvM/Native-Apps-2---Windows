namespace NativeAppsII_Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Ondernemings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Naam = c.String(),
                    Openingsuur = c.String(),
                    Sluituur = c.String(),
                    Gemeente = c.String(),
                    Straat = c.String(),
                    Land = c.String(),
                    Website = c.String(),
                    Telefooonnummer = c.String(),
                    Information = c.String()
    })
                .PrimaryKey(t => t.Id);
            AddColumn("dbo.Ondernemings", "CategorieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ondernemings", "CategorieId");
            AddForeignKey("dbo.Ondernemings", "CategorieId", "dbo.Categories", "Id", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ondernemings", "Categorie", c => c.String());
            DropForeignKey("dbo.Ondernemings", "CategorieId", "dbo.Categories");
            DropIndex("dbo.Ondernemings", new[] { "CategorieId" });
            DropColumn("dbo.Ondernemings", "CategorieId");
            DropTable("dbo.Categories");
        }
    }
}
