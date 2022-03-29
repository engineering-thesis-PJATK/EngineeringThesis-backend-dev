using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CompanyNoteAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CnaId { get; set; }
        public string CnaName { get; set; }
        public byte[] CnaBinaryData { get; set; }
        public int CnaIdCompanyNote { get; set; }

        public virtual CompanyNote CnaIdCompanyNoteNavigation { get; set; }
    }
}
