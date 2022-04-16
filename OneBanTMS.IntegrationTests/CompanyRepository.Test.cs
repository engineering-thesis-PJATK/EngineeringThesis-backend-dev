using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;

namespace OneBanTMS.IntegrationTests
{
    public class CompanyRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<CompanyDto> _validator;
        public CompanyRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _validator = new CompanyValidator(new CompanyHandler(_context));
        }

        [Test, Isolated]
        public async Task Add_PassValid_ShouldAddCompanyToDatabase()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var repository = new CompanyRepository(_context, _validator);
            await repository.AddNewCompany(companyDto);
            var userCountAfter = await _context.Companies.CountAsync(x => x.CmpName == companyDto.CmpName);
            Assert.That(userCountAfter, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task Update_PassValid_ShouldUpdateCompanyInDatabase()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var repository = new CompanyRepository(_context, _validator);
            await repository.AddNewCompany(companyDto);
            var idAddedCompany = await _context
                .Companies
                .Where(x =>
                    x.CmpName == companyDto.CmpName)
                .Select(x =>
                    x.CmpId)
                .SingleOrDefaultAsync();
            CompanyDto updatedCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany1",
                CmpNip = "0000000000",
                CmpKrsNumber = "1",
                CmpLandline = "1",
                CmpRegon = "1",
                CmpNipPrefix = "PL"
            };
            await repository.UpdateCompany(updatedCompanyDto, idAddedCompany);
            var countUpdatedCompany = await _context
                .Companies
                .CountAsync(x =>
                    x.CmpName == updatedCompanyDto.CmpName
                    && x.CmpNip == updatedCompanyDto.CmpNip
                    && x.CmpKrsNumber == updatedCompanyDto.CmpKrsNumber
                    && x.CmpLandline == updatedCompanyDto.CmpLandline
                    && x.CmpRegon == updatedCompanyDto.CmpRegon
                    && x.CmpNipPrefix == updatedCompanyDto.CmpNipPrefix);
            Assert.That(countUpdatedCompany, Is.EqualTo(1));
        }
        [Test, Isolated]
        public async Task Delete_PassValid_ShouldDeleteCompanyFromDatabase()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var repository = new CompanyRepository(_context, _validator);
            await repository.AddNewCompany(companyDto);
            var idAddedCompany = await _context
                .Companies
                .Where(x =>
                    x.CmpName == companyDto.CmpName)
                .Select(x =>
                    x.CmpId)
                .SingleOrDefaultAsync();

            await repository.DeleteCompany(idAddedCompany);
            var countUpdatedCompany = await _context
                .Companies
                .CountAsync(x =>
                    x.CmpName == companyDto.CmpName
                    && x.CmpNip == companyDto.CmpNip
                    && x.CmpKrsNumber == companyDto.CmpKrsNumber
                    && x.CmpLandline == companyDto.CmpLandline
                    && x.CmpRegon == companyDto.CmpRegon
                    && x.CmpNipPrefix == companyDto.CmpNipPrefix);
            Assert.That(countUpdatedCompany, Is.EqualTo(0));
        }
    }
}