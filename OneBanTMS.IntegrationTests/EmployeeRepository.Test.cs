using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;
using OneBan_TMS.Validators.EmployeeValidators;

namespace OneBanTMS.IntegrationTests
{
    public class EmployeeRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<EmployeeToUpdate> _validatorToUpdate;
        private readonly IValidator<EmployeeDto> _validatorToAdd;
        private PasswordHandler _passwordHandler;
        public EmployeeRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _validatorToUpdate = new EmployeeToUpdateValidator();
            _validatorToAdd = new EmployeeToAddValidator(new ValidatorHandler());
            _passwordHandler = new PasswordHandler();
        }
        [Test, Isolated]
        public async Task AddEmployee_PassValid_ShouldAddEmployeeToDatabase()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _validatorToUpdate, _validatorToAdd);
            var newEmployee = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "Test",
                EmpSurname = "TestTest",
                EmpPassword = "Testtest123!",
                EmpPhoneNumber = "123123123"
            };
            await employeeRepository.AddEmployee(newEmployee);
            var employeesCountAfter = await _context
                .Employees
                .CountAsync(x =>
                    x.EmpEmail == newEmployee.EmpEmail
                    && x.EmpName == newEmployee.EmpName
                    && x.EmpSurname == newEmployee.EmpSurname
                    && x.EmpPhoneNumber == newEmployee.EmpPhoneNumber);
            Assert.That(employeesCountAfter, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task AddEmployee_PasswordWithoutUpperCharacter_ShouldThrowValidationException()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _validatorToUpdate, _validatorToAdd);
            var newEmployee = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "Test",
                EmpSurname = "TestTest",
                EmpPassword = "testtest123!",
                EmpPhoneNumber = "123123123"
            };
            Func<Task> action = async () => await employeeRepository
                .AddEmployee(newEmployee);
            await action.Should().ThrowExactlyAsync<ValidationException>();
        }
        [Test, Isolated]
        public async Task AddEmployee_PasswordWithoutSpecialCharacter_ShouldThrowValidationException()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _validatorToUpdate, _validatorToAdd);
            var newEmployee = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "Test",
                EmpSurname = "TestTest",
                EmpPassword = "Testtest123",
                EmpPhoneNumber = "123123123"
            };
            Func<Task> action = async () => await employeeRepository
                .AddEmployee(newEmployee);
            await action.Should().ThrowExactlyAsync<ValidationException>();
        }

        [Test, Isolated]
        public async Task UpdateEmployee_PassValid_ShouldUpdateEmployee()
        {
            var employeeRepository = new EmployeeRepository(_context, _passwordHandler, _validatorToUpdate, _validatorToAdd);
            var newEmployee = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "Test",
                EmpSurname = "TestTest",
                EmpPassword = "Testtest123!",
                EmpPhoneNumber = "123123123"
            };
            await employeeRepository.AddEmployee(newEmployee);
            var employeeId = await _context
                .Employees
                .Where(x => x.EmpEmail == newEmployee.EmpEmail)
                .Select(x => x.EmpId)
                .SingleOrDefaultAsync();
            var updatedEmployee = new EmployeeToUpdate()
            {
                EmpEmail = "test1@test.pl",
                EmpLogin = "test1@test.pl",
                EmpName = "Test",
                EmpSurname = "TestTest",
                EmpPhoneNumber = "123123123"
            };
            await employeeRepository.UpdateEmployee(employeeId, updatedEmployee);
            var countUpdatedEmployee = await _context
                .Employees
                .CountAsync(x => x.EmpEmail == updatedEmployee.EmpEmail);
            Assert.That(countUpdatedEmployee, Is.EqualTo(1));
        }
        //Todo: Dodać metodę ze złymi danymi
        
    }
}