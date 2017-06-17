using Starcounter;

namespace Sviat
{
    partial class HomesInFranchisePage : Json
    {
        static HomesInFranchisePage()
        {
            DefaultTemplate.Address.InstanceType = typeof(AddressEditPage);
        }

        public string EditUrl => $"/Sviat/home/{this.Data.GetObjectID()}";
    }
}
