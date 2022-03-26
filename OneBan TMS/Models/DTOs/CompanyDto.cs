using System.Reflection.Metadata.Ecma335;

namespace OneBan_TMS.Models.DTOs
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public string Nip { get; set; }
        public string NipPrefix { get; set; }
        public string Regon { get; set; }
        public string KrsNumber { get; set; }
        public string Landline { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        
    }
}