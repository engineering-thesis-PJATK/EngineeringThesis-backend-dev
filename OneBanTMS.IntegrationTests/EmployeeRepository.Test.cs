using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Repository;

namespace OneBanTMS.IntegrationTests
{
    public class EmployeeRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<EmployeeToUpdate> _employeeToUpdateValidator;
        private readonly IValidator<EmployeeDto> _employeeToAddValidator;
        private readonly IPasswordHandler _passwordHandler;
        public EmployeeRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
        }
        [Test, Isolated]
        public async Task AddEmployee_PassValid_ShouldAddEmployeeToDatabase()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _employeeToUpdateValidator, _employeeToAddValidator);
            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "testName",
                EmpPassword = "123@@Test",
                EmpSurname = "TestSurname",
                EmpPhoneNumber = "123-123-132"
            };
            await employeeRepository.AddEmployee(employeeDto);
            var employeeCountAfter = _context
                .Employees
                .CountAsync(x =>
                    x.EmpEmail == employeeDto.EmpEmail
                    && x.EmpName == employeeDto.EmpName
                    && x.EmpSurname == employeeDto.EmpSurname
                    && x.EmpPhoneNumber == employeeDto.EmpPhoneNumber);
            Assert.That(employeeCountAfter, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task UpdateEmployee_PassValid_ShouldUpdateEmployeeInDatabase()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _employeeToUpdateValidator, _employeeToAddValidator);
            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "testName",
                EmpPassword = "123@@Test",
                EmpSurname = "TestSurname",
                EmpPhoneNumber = "123-123-132"
            };
            await employeeRepository.AddEmployee(employeeDto);
            var employeeId = await _context
                .Employees
                .Where(x =>
                    x.EmpEmail == employeeDto.EmpEmail
                    && x.EmpName == employeeDto.EmpName
                    && x.EmpSurname == employeeDto.EmpSurname
                    && x.EmpPhoneNumber == employeeDto.EmpPhoneNumber)
                .Select(x => x.EmpId)
                .SingleOrDefaultAsync();
            EmployeeToUpdate employeeToUpdate = new EmployeeToUpdate()
            {
                EmpEmail = "1test1@test.pl",
                EmpName = "testUpdatedName",
                EmpSurname = "testUpdatedSurname",
                EmpPhoneNumber = "312-312-312",
                EmpLogin = "1test1@test.pl"
            };
            await employeeRepository.UpdateEmployee(employeeId, employeeToUpdate);
            var employeeCountAfter = _context
                .Employees
                .CountAsync(x =>
                    x.EmpEmail == employeeToUpdate.EmpEmail
                    && x.EmpName == employeeToUpdate.EmpName
                    && x.EmpSurname == employeeToUpdate.EmpSurname
                    && x.EmpPhoneNumber == employeeToUpdate.EmpPhoneNumber);
            Assert.That(employeeCountAfter, Is.EqualTo(1));
        }
    }
}