using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CompanyNote
    {
        public CompanyNote()
        {
            CompanyNoteAttachments = new HashSet<CompanyNoteAttachment>();
        }

        public int CntId { get; set; }
        public string CntContent { get; set; }
        public int CntIdCompany { get; set; }

        public virtual Company CntIdCompanyNavigation { get; set; }
        public virtual ICollection<CompanyNoteAttachment> CompanyNoteAttachments { get; set; }
    }
}
