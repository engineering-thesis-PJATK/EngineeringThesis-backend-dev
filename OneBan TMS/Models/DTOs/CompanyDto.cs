using System.Reflection.Metadata.Ecma335;

namespace OneBan_TMS.Models.DTOs
{
    public class CompanyDto
    {
        public string CmpName { get; set; }
        public string CmpNip { get; set; }
        public string CmpNipPrefix { get; set; }
        public string CmpRegon { get; set; }
        public string CmpKrsNumber { get; set; }
        public string CmpLandline { get; set; }
    }
}