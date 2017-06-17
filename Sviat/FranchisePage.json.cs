namespace Sviat
{
    using System;

    using Starcounter;

    using Sviat.Database;

    partial class FranchisePage : Json
    {
        static FranchisePage()
        {
            DefaultTemplate.Homes.ElementType.InstanceType = typeof(HomesInFranchisePage);
            DefaultTemplate.Address.InstanceType = typeof(AddressEditPage);
            DefaultTemplate.AddressOfNewHome.InstanceType = typeof(AddressEditPage);
        }

        protected override void OnData()
        {
            base.OnData();

            UpdateDefaultValuesForNewHome();
        }

        private void UpdateDefaultValuesForNewHome()
        {
            var vendor = this.Data as Franchise;
            if (vendor != null)
            {
                if (string.IsNullOrWhiteSpace(this.AddressOfNewHome.Country))
                {
                    this.AddressOfNewHome.Country = vendor.Address.Country;
                }
                if (string.IsNullOrWhiteSpace(this.AddressOfNewHome.City))
                {
                    this.AddressOfNewHome.City = vendor.Address.City;
                }
                if (string.IsNullOrWhiteSpace(this.TransactionDateOfNewHome))
                {
                    this.TransactionDateOfNewHome = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
        }

        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
            UpdateDefaultValuesForNewHome();
        }

        void Handle(Input.RegisterHomeTrigger action)
        {
            var date = DateTime.Parse(this.TransactionDateOfNewHome);

            var address = new Address
                              {
                                  City = this.AddressOfNewHome.City,
                                  Country = this.AddressOfNewHome.Country,
                                  Street = this.AddressOfNewHome.Street,
                                  StreetNumber = this.AddressOfNewHome.StreetNumber,
                                  ZipCode = this.AddressOfNewHome.ZipCode
                              };
            new Home
                {
                    Address = address,
                    Commission = this.CommissionForNewHome,
                    Price = this.PriceOfNewHome,
                    Vendor = this.Data as Franchise,
                    Date = date
                };

            Transaction.Commit();
        }
    }
}