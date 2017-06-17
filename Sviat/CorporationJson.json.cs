using Starcounter;

namespace Sviat
{
    using Sviat.Database;

    partial class CorporationJson : Json
    {
        static CorporationJson()
        {
            DefaultTemplate.Franchises.ElementType.InstanceType = typeof(FranchisesInCorporationPage);
        }

        void Handle(Input.NewFranchiseTrigger action)
        {
            var newFr = new Franchise
            {
                Corporation = this.Data as Corporation,
                Name = this.NewFranchiseName
            };
            Transaction.Commit();
        }
    }
}
