namespace Sviat
{
    using Starcounter;

    using Sviat.Database;

    partial class CorporationJson : Json
    {
        static CorporationJson()
        {
            DefaultTemplate.Franchises.ElementType.InstanceType = typeof(FranchisesInCorporationPage);
        }

        public QueryResultRows<Franchise> Franchises
        {
            get
            {
                return Db.SQL<Franchise>($"SELECT f FROM Franchise f WHERE f.Corporation = ?{this.sortCommand}", this.Data as Corporation);
            }
        }

        private string sortCommand = string.Empty;

        void Handle(Input.NewFranchiseTrigger action)
        {
            var newFr = new Franchise
                            {
                                Corporation = this.Data as Corporation,
                                Name = this.NewFranchiseName,
                                Address = new Address()
                            };

            Transaction.Commit();
        }

        void Handle(Input.SortFranchiseByCountTrigger action)
        {
            this.sortCommand = " ORDER BY f.HomesCount DESC";
        }

        void Handle(Input.SortFranchiseByTotalSalesTrigger action)
        {
            this.sortCommand = " ORDER BY f.TotalCommission DESC";
        }

        void Handle(Input.SortFranchiseByAvgSalesTrigger action)
        {
            this.sortCommand = " ORDER BY f.AvgCommission DESC";
        }

        void Handle(Input.SortFranchiseByTrendTrigger action)
        {
            this.sortCommand = string.Empty;
        }
    }
}