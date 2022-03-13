using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CompanyNote
    {
        public int CntId { get; set; }
        public string CndContent { get; set; }
        public int CntIdCompany { get; set; }

        public virtual Company CntIdCompanyNavigation { get; set; }
    }
}
