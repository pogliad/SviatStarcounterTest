namespace Sviat
{
    using System;

    using Starcounter;

    using Sviat.Database;

    class Program
    {
        static void Main()
        {
            CreateTestData();

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            RegisterHandlers();
        }

        private static void CreateTestData()
        {
            Db.Transact(
                () =>
                    {
                        var anyone = Db.SQL<Corporation>("SELECT c FROM Corporation c").First;
                        if (anyone == null)
                        {
                            var corp = new Corporation { Name = "Real Estate Stars" };
                            var f1 = new Franchise
                                         {
                                             Name = "Stockholm West",
                                             Corporation = corp,
                                             Address = new Address { City = "Stockholm" }
                                         };
                            var f2 = new Franchise
                                         {
                                             Name = "Stockholm East",
                                             Corporation = corp,
                                             Address = new Address { City = "Stockholm" }
                                         };
                            var f3 = new Franchise
                                         {
                                             Name = "Stockholm South",
                                             Corporation = corp,
                                             Address = new Address { City = "Stockholm" }
                                         };
                            var f4 = new Franchise
                                         {
                                             Name = "Stockholm North",
                                             Corporation = corp,
                                             Address = new Address { City = "Stockholm" }
                                         };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today.AddMonths(3),
                                    Price = 5000000,
                                    Commission = 75000,
                                    Address = new Address { ZipCode = "w1" }
                                };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today.AddMonths(3),
                                    Price = 5000000,
                                    Commission = 75000,
                                    Address = new Address { ZipCode = "w2" }
                                };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today.AddMonths(3),
                                    Price = 5000000,
                                    Commission = 75000,
                                    Address = new Address { ZipCode = "w3" }
                                };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today.AddMonths(3),
                                    Price = 3000000,
                                    Commission = 65000,
                                    Address = new Address { ZipCode = "w4" }
                                };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today.AddMonths(3),
                                    Price = 3000000,
                                    Commission = 65000,
                                    Address = new Address { ZipCode = "w5" }
                                };
                            new Home
                                {
                                    Vendor = f1,
                                    Date = DateTime.Today,
                                    Price = 3000000,
                                    Commission = 60002,
                                    Address = new Address { ZipCode = "thisMonth" }
                                };
                            new Home
                                {
                                    Vendor = f3,
                                    Date = DateTime.Today,
                                    Price = 5000000,
                                    Commission = 20000,
                                    Address = new Address { ZipCode = "thisMonthAlone" }
                                };
                        }
                    });
        }

        private static void RegisterHandlers()
        {
            Handle.GET(
                "/Sviat",
                () =>
                    {
                        return Db.Scope(
                            () =>
                                {
                                    var corporations = Db.SQL<Corporation>("SELECT c FROM Corporation c");
                                    var json = new CorporationListJson { Corporations = corporations };

                                    if (Session.Current == null)
                                    {
                                        Session.Current = new Session(SessionOptions.PatchVersioning);
                                    }
                                    json.Session = Session.Current;

                                    return json;
                                });
                    });

            Handle.GET(
                "/Sviat/franchise/{?}",
                (string id) =>
                    {
                        return Db.Scope(
                            () =>
                                {
                                    var page = new FranchisePage
                                                   {
                                                       Data =
                                                           Db.SQL<Franchise>(
                                                               "SELECT f FROM Franchise f WHERE f.ObjectID = ?",
                                                               id).First
                                                   };

                                    if (Session.Current == null)
                                    {
                                        Session.Current = new Session(SessionOptions.PatchVersioning);
                                    }
                                    page.Session = Session.Current;

                                    return page;
                                });
                    });

            Handle.GET(
                "/Sviat/home/{?}",
                (string id) =>
                    {
                        return Db.Scope(
                            () =>
                                {
                                    var page = new HomeEditPage
                                                   {
                                                       Data =
                                                           Db.SQL<Home>(
                                                               "SELECT h FROM Home h WHERE h.ObjectID = ?",
                                                               id).First
                                                   };

                                    if (Session.Current == null)
                                    {
                                        Session.Current = new Session(SessionOptions.PatchVersioning);
                                    }
                                    page.Session = Session.Current;

                                    return page;
                                });
                    });
        }
    }
}