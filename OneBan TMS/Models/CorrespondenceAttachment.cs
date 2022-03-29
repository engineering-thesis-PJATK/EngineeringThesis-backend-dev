using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CorrespondenceAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public byte[] CatBinaryData { get; set; }
        public int CatIdCorrespondence { get; set; }

        public virtual Correspondence CatIdCorrespondenceNavigation { get; set; }
    }
}
