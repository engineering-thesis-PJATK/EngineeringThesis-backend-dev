using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Address
    {
        public Address()
        {
            Companies = new HashSet<Company>();
        }

        public int AdrId { get; set; }
        public string AdrTown { get; set; }
        public string AdrStreet { get; set; }
        public string AdrStreetNumber { get; set; }
        public string AdrPostCode { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
