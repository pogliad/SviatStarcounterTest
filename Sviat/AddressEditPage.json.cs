using Starcounter;

namespace Sviat
{
    using System.Text;

    partial class AddressEditPage : Json
    {
        public string FullAddress
        {
            get
            {
                var sb = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Street))
                {
                    sb.Append(Street);
                    if (!string.IsNullOrWhiteSpace(StreetNumber))
                    {
                        sb.AppendFormat(" {0}", StreetNumber);
                    }
                }

                if (sb.Length > 0 && (!string.IsNullOrWhiteSpace(ZipCode) || !string.IsNullOrWhiteSpace(City)))
                {
                    sb.Append(",");
                }

                if (!string.IsNullOrWhiteSpace(ZipCode))
                {
                    sb.AppendFormat(" {0}", ZipCode);
                }

                if (!string.IsNullOrWhiteSpace(City))
                {
                    sb.AppendFormat(" {0}", City);
                }

                if (!string.IsNullOrWhiteSpace(Country))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }

                    sb.AppendFormat(" {0}", Country);
                }

                return sb.ToString();
            }
        }

    }
}
