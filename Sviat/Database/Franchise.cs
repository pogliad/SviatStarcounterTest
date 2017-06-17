namespace Sviat.Database
{
    using Starcounter;

    [Database]
    public class Franchise
    {
        public Address Address;

        public Corporation Corporation;

        public string Name;

        public QueryResultRows<Home> Homes => Db.SQL<Home>("SELECT h FROM Home h WHERE h.Vendor = ?", this);

        public long HomesCount => Db.SQL<long>("SELECT COUNT(h) FROM Home h WHERE h.Vendor = ?", this).First;

        public decimal TotalCommission => Db.SQL<decimal>("SELECT SUM(h.Commission) FROM Home h WHERE h.Vendor = ?", this).First;

        public decimal AvgCommission => Db.SQL<decimal>("SELECT AVG(h.Commission) FROM Home h WHERE h.Vendor = ?", this).First;
    }
}