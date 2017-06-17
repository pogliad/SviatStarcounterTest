namespace Sviat.Database
{
    using Starcounter;

    [Database]
    public class Corporation
    {
        public string Name;

        //public QueryResultRows<Franchise> Franchises => Db.SQL<Franchise>("SELECT f FROM Franchise f WHERE f.Corporation = ?", this);
    }
}