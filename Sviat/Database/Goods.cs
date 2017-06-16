namespace Sviat.Database
{
    using System;

    using Starcounter;

    [Database]
    public class Goods
    {
        public decimal Commission;

        public DateTime Date;

        public decimal Price;

        public Franchise Vendor;

        public string Comment;
    }
}