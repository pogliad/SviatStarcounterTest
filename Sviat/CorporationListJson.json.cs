using Starcounter;

namespace Sviat
{
    using Sviat.Database;

    partial class CorporationListJson : Json
    {
        static CorporationListJson()
        {
            DefaultTemplate.Corporations.ElementType.InstanceType = typeof(CorporationJson);
        }

        void Handle(Input.NewCorporationTrigger action)
        {
            new Corporation { Name = this.NewCorporationName };
            Transaction.Commit();
        }

    }
}
