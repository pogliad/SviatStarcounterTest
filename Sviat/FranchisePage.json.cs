using Starcounter;

namespace Sviat
{
    partial class FranchisePage : Json
    {
        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
        }
    }
}
