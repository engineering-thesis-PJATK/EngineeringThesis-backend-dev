using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Attachment
    {
        public int AttId { get; set; }
        public string AttBinarydata { get; set; }
        public string AttName { get; set; }
        public int AttIdCorrespondence { get; set; }

        public virtual Correspondence AttIdCorrespondenceNavigation { get; set; }
    }
}
