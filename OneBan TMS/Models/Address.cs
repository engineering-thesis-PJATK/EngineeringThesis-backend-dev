using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Address
    {
        public int AdrId { get; set; }
        public string AdrTown { get; set; }
        public string AdrStreet { get; set; }
        public string AdrStreetNumber { get; set; }
        public string AdrPostCode { get; set; }
        public string AdrCountry { get; set; }
        public int AdrIdCompany { get; set; }

        public virtual Company AdrIdCompanyNavigation { get; set; }
    }
}
