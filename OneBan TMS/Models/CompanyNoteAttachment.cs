using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CompanyNoteAttachment
    {
        public int CnaId { get; set; }
        public string CnaName { get; set; }
        public byte[] CnaBinaryData { get; set; }
        public int CnaIdCompanyNote { get; set; }

        public virtual CompanyNote CnaIdCompanyNoteNavigation { get; set; }
    }
}
