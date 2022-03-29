using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdrId { get; set; }
        public string AdrTown { get; set; }
        public string AdrStreet { get; set; }
        public string AdrStreetNumber { get; set; }
        public string AdrPostCode { get; set; }
        public string AdrCountry { get; set; }
        public int AdrIdCompany { get; set; }
        [JsonIgnore]
        public virtual Company AdrIdCompanyNavigation { get; set; }
    }
}
