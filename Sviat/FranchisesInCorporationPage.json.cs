using Starcounter;

namespace Sviat
{
    partial class FranchisesInCorporationPage : Json
    {
        public string EditUrl => $"/Sviat/franchise/{this.Data.GetObjectID()}";
    }
}
