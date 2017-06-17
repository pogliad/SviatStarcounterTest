using Starcounter;

namespace Sviat
{
    partial class HomeEditPage : Json
    {
        static HomeEditPage()
        {
            DefaultTemplate.Address.InstanceType = typeof(AddressEditPage);
        }

        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }
    }
}
