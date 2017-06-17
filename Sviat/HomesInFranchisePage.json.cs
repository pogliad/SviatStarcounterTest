namespace Sviat
{
    using Starcounter;

    using Sviat.Database;

    partial class HomesInFranchisePage : Json
    {
        static HomesInFranchisePage()
        {
            DefaultTemplate.Address.InstanceType = typeof(AddressEditPage);
        }

        public string EditUrl => $"/Sviat/home/{this.Data.GetObjectID()}";

        void Handle(Input.RemoveHomeTrigger action)
        {
            var home = this.Data as Home;
            home.Delete();
            Transaction.Commit();
        }
    }
}