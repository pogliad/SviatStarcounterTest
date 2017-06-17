using Starcounter;

namespace Sviat
{
    using System;
    using System.Diagnostics;

    using Sviat.Database;

    partial class FranchisePage : Json
    {
        static FranchisePage()
        {
            //DefaultTemplate.Homes.ElementType.InstanceType = typeof(CorporationJson);
            DefaultTemplate.Address.InstanceType = typeof(AddressEditPage);
            DefaultTemplate.AddressOfNewHome.InstanceType = typeof(AddressEditPage);
        }

        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }

        void Handle(Input.RegisterHomeTrigger action)
        {
            var date = DateTime.Parse(this.TransactionDateOfNewHome);

            var a = this.AddressOfNewHome;
            var address = new Address
                              {
                                  City = this.AddressOfNewHome.City,
                                  Country = this.AddressOfNewHome.Country,
                                  Street = this.AddressOfNewHome.Street,
                                  StreetNumber = this.AddressOfNewHome.StreetNumber,
                                  ZipCode = this.AddressOfNewHome.ZipCode,
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
