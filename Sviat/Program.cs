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
                            var f1 = new Franchise { Name = "F1", Corporation = corp };
                            var f2 = new Franchise { Name = "F2", Corporation = corp };
                            new Home { Vendor = f1, Date = DateTime.Today, Price = 6000000, Commission = 10000 };
                            new Home { Vendor = f1, Date = DateTime.Today.AddDays(-2), Price = 4000000, Commission = 20000 };
                            new Home { Vendor = f2, Date = DateTime.Today.AddDays(-3), Price = 5000000, Commission = 19000 };
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
                        FranchisePage page = new FranchisePage();

                        page.Data = Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.ObjectID = ?", id).First;

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