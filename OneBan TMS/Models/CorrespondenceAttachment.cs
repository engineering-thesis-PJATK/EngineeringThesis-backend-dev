using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class CorrespondenceAttachment
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public byte[] CatBinaryData { get; set; }
        public int CatIdCorrespondence { get; set; }

        public virtual Correspondence CatIdCorrespondenceNavigation { get; set; }
    }
}
