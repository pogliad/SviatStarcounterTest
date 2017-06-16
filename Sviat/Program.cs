namespace Sviat
{
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
                            new Corporation { Name = "Test" };
                        }
                    });

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            Handle.GET(
                "/Sviat",
                () =>
                    {
                        return Db.Scope(
                            () =>
                                {
                                    var corporation = Db.SQL<Corporation>("SELECT c FROM Corporation c").First;
                                    var json = new CorporationJson { Data = corporation };

                                    if (Session.Current == null)
                                    {
                                        Session.Current = new Session(SessionOptions.PatchVersioning);
                                    }
                                    json.Session = Session.Current;

                                    return json;
                                });
                    });
        }
    }
}