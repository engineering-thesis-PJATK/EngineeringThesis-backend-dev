using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OrganizationalTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OtkId { get; set; }
        public string OtkDescription { get; set; }
        public int OtkIdEmployee { get; set; }

        public virtual Employee OtkIdEmployeeNavigation { get; set; }
    }
}
