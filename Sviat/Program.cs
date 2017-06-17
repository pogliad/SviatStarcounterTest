namespace Sviat
{
    using System;

    using Starcounter;

    using Sviat.Database;

    class Program
    {
        static void Main()
        {
            Db.Transact(
                () =>
                    {
                        var anyone = Db.SQL<Corporation>("SELECT c FROM Corporation c").First;
                        if (anyone == null)
                        {
                            var corp = new Corporation { Name = "Test1"};
                            var f1 = new Franchise { Name = "F1", Corporation = corp, Address = new Address()};
                            var f2 = new Franchise { Name = "F2", Corporation = corp, Address = new Address() };
                            new Home { Vendor = f1, Date = DateTime.Today, Price = 6000000, Commission = 10000, Address = new Address() };
                            new Home { Vendor = f1, Date = DateTime.Today.AddDays(-2), Price = 4000000, Commission = 20000, Address = new Address() };
                            new Home { Vendor = f2, Date = DateTime.Today.AddDays(-3), Price = 5000000, Commission = 19000, Address = new Address() };
                        }
                    });

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            Blender.MapUri<Franchise>("/Sviat/franchise/{?}");

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

            Handle.GET("/Sviat/franchise/{?}", (string id) =>
            {
                    return Db.Scope<FranchisePage>(() =>
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

            Handle.GET("/Sviat/home/{?}", (string id) =>
            {
                    return Db.Scope<HomeEditPage>(() =>
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