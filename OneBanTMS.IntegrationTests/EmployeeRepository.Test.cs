using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Validators.EmployeeValidators;

namespace OneBanTMS.IntegrationTests
{
    public class EmployeeRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<EmployeeToUpdate> _validator;
        public EmployeeRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _validator = new EmployeeToUpdateValidator();
        }
    }
}