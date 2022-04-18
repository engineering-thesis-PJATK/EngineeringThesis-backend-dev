using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Repository;
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

        [Test, Isolated]
        public async Task UpdateEmployee_PassValid_ShouldUpdateExistedEmployee()
        {
            var userRepository = new UserRepository(_context);
            var employeeRepository = new EmployeeRepository(_context, _validator);
            byte[] PasswordHash = new byte[1];
            byte[] PasswordSalt = new byte[1];
            var newEmployee = new EmployeeDto()
            {
                EmpEmail = "tk@oneman.pl",
                EmpName = "Test",
                EmpSurname = "TestSurname",
                EmpPassword = "123",
                EmpPhoneNumber = "1231"
            };
            await userRepository.AddNewUser(newEmployee,PasswordHash, PasswordSalt);
            var employeeId = await _context
                .Employees
                .Where(x =>
                    x.EmpEmail == newEmployee.EmpEmail)
                .Select(x => x.EmpId)
                .SingleOrDefaultAsync();
            var employeeToUpdate = new EmployeeToUpdate()
            {
                EmpEmail = "test@test.pl",
                EmpLogin = "loginTest",
                EmpName = "Test",
                EmpSurname = "TestSurname",
                EmpPhoneNumber = "1231"
            };
            await employeeRepository.UpdateEmployee(employeeId, employeeToUpdate);
            var employeeCount = await _context
                .Employees
                .CountAsync(x =>
                    x.EmpEmail == employeeToUpdate.EmpEmail);
            Assert.That(employeeCount, Is.EqualTo(1));

        } 
    }
}