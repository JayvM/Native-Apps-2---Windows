namespace NativeAppsII_Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ondernemings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        naam = c.String(),
                        Openingsuur = c.String(),
                        Sluituur = c.String(),
                        Categorie = c.String(),
                        Gemeente = c.String(),
                        Straat = c.String(),
                        Land = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ondernemings");
        }
    }
}
