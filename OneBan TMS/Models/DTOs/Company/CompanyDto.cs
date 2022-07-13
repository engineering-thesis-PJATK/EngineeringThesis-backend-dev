namespace OneBan_TMS.Models.DTOs.Company
{
    using OneBan_TMS.Models;
    public class CompanyDto 
    {
        public string CmpName { get; set; }
        public string CmpNip { get; set; }
        public string CmpNipPrefix { get; set; }
        public string CmpRegon { get; set; }
        public string CmpKrsNumber { get; set; }
        public string CmpLandline { get; set; }

        public Company GetCompany()
        {
            return new Company()
            {
                CmpName = this.CmpName,
                CmpNip = this.CmpNip,
                CmpNipPrefix = this.CmpNipPrefix,
                CmpRegon = this.CmpRegon,
                CmpKrsNumber = this.CmpKrsNumber,
                CmpLandline = this.CmpLandline
            };
        }

        public Company GetCompanyToUpdate(Company company)
        {
            company.CmpName = this.CmpName;
            company.CmpNip = this.CmpNip;
            company.CmpNipPrefix = this.CmpNipPrefix;
            company.CmpRegon = this.CmpRegon;
            company.CmpKrsNumber = this.CmpKrsNumber;
            company.CmpLandline = this.CmpLandline;
            return company;
        }
    }
}