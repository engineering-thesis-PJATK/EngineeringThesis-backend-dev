using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurId { get; set; }
        public string CurName { get; set; }
        public string CurSurname { get; set; }
        public string CurEmail { get; set; }
        public string CurPhoneNumber { get; set; }
        public string CurPosition { get; set; }
        public string CurComments { get; set; }
        public DateTime CurCreatedAt { get; set; }
        public int CurIdCompany { get; set; }
        [JsonIgnore]
        public virtual Company CurIdCompanyNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
