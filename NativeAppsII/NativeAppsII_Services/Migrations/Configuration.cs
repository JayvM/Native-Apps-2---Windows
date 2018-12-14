namespace NativeAppsII_Services.Migrations
{
    using NativeAppsII_Services.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NativeAppsII_Services.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NativeAppsII_Services.Models.ServiceContext context)
        {
         
            context.Categorieën.AddOrUpdate(x => x.Id,
                new Categorie()
                {
                    Id = 1,
                    Naam = "Informatica"
                },
                  new Categorie()
                  {
                      Id = 2,
                      Naam = "Horeca"
                  },
                    new Categorie()
                    {
                        Id = 3,
                        Naam = "Online Webservice"
                    }


                );
            context.Ondernemingen.AddOrUpdate(x => x.Id,
        new Onderneming()
        {
            Id = 1,
            Gemeente = " 9050 Gentbrugge",
            Land = "Belgie",
            Naam = "Orbid",
            Openingsuur = "9:00",
            Sluituur = "10:00",
            Straat = "Tineke Van Heulestraat 5",
            Website = "orbid.be",
            Telefooonnummer = "092312930",
            Information = "L Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat",
            CategorieId = 1
        },
        new Models.Onderneming()
        {
            Id = 2,
            Gemeente = " 9000 Gent",
            Land = "Belgie",
            Naam = "Orbid",
            Openingsuur = "9:00",
            Sluituur = "20:15",
            Straat = "Tineke Van Heulestraat 5",
            Website = "orbid.be",
            Telefooonnummer = "092312930",
            Information = "L Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat",
            CategorieId = 2
        },
        new Models.Onderneming()
        {
            Id = 3,
            Gemeente = " 9000 Gent",
            Land = "Belgie",
            Naam = "Orbid",
            Openingsuur = "9:00",
            Sluituur = "21:00",
            Straat = "Tineke Van Heulestraat 5",
            Website = "orbid.be",
            Telefooonnummer = "092312930",
            Information = "L Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat",
            CategorieId = 1
        }, new Models.Onderneming()
        {
            Id = 4,
            Gemeente = " 9000 Gent",
            Land = "Belgie",
            Naam = "Coolblue",
            Openingsuur = "9:00",
            Sluituur = "11:00",
            Straat = "Tineke Van Heulestraat 5",
            Website = "orbid.be",
            Telefooonnummer = "092312930",
            Information = "L Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat",
            CategorieId = 1
        }
        );
            context.Acties.AddOrUpdate(x => x.Id,
             new Actie()
             {
                 Id = 1,
                 Beschrijving = "Alles aan 50%",
                 GeldigTot = new DateTime(2018, 12, 31, 8, 30, 52),
                 OndernemingId = 1
        },
               new Actie()
               {
                   Id = 2,
                   Beschrijving = "Gratis zonnebril bij bezoek",
                   GeldigTot = new DateTime(2018, 12, 31, 8, 30, 52),
                   OndernemingId = 2

               },
                 new Actie()
                 {
                     Id = 3,
                     Beschrijving = "20% korting bij inleveren van bon",
                     GeldigTot = new DateTime(2018, 12, 31, 8, 30, 52),
                     OndernemingId = 3

                 }


             );
            context.Evenementen.AddOrUpdate(x => x.Id,
             new Evenement()
             {
                 Id = 1,
                 Beschrijving = "Opendeurdag",
                 Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                 Plaats = "Gravesteen Gent",
                 OndernemingId = 1
             },
               new Evenement()
               {
                   Id = 2,
                   Beschrijving = "Nieuwjaar vieren",
                   Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                   Plaats = "Gravesteen Gent",
                   OndernemingId = 2

               },
                 new Evenement()
                 {
                     Id = 3,
                     Beschrijving = "Boekenbeurs 2018",
                     Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                     Plaats = "Gravesteen Gent",
                     OndernemingId = 3

                 },
               new Evenement()
               {
                   Id = 2,
                   Beschrijving = "Nieuwjaar vieren",
                   Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                   Plaats = "Gravesteen Gent",
                   OndernemingId = 1

               },
               new Evenement()
               {
                   Id = 4,
                   Beschrijving = "Nieuwjaar vieren",
                   Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                   Plaats = "Gravesteen Gent",
                   OndernemingId = 1

               },
               new Evenement()
               {
                   Id = 5,
                   Beschrijving = "Nieuwjaar vieren",
                   Datum = new DateTime(2018, 12, 31, 8, 30, 52),
                   Plaats = "Gravesteen Gent",
                   OndernemingId = 1

               }


             );

        }
    }
}
