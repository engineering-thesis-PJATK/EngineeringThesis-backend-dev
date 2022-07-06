using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Repository;

namespace OneBanTMS.IntegrationTests
{
    public class EmployeeRepository_Test
    {
        private OneManDbContext _context;
        private IPasswordHandler _passwordHandler;
        private EmployeeRepository _employeeRepository;
        [SetUp]
        public void Init()
        {
            _context = DbContextFactory.GetOneManDbContext();
            _passwordHandler = new PasswordHandler();
            _employeeRepository = new EmployeeRepository(_context, _passwordHandler);
        }
        [Test, Isolated]
        public async Task AddEmployee_PassValid_ShouldAddEmployeeToDatabase()
        {
            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "testName",
                EmpPassword = "123@@Test",
                EmpSurname = "TestSurname",
                EmpPhoneNumber = "123-123-132"
            };
            await _employeeRepository.AddEmployee(employeeDto);
            var employeeCountAfter = await _context
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
            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmpEmail = "test@test.pl",
                EmpName = "testName",
                EmpPassword = "123@@Test",
                EmpSurname = "TestSurname",
                EmpPhoneNumber = "123-123-132"
            };
            await _employeeRepository.AddEmployee(employeeDto);
            var employeeId = await _context
                .Employees
                .Where(x =>
                    x.EmpEmail == employeeDto.EmpEmail
                    && x.EmpName == employeeDto.EmpName
                    && x.EmpSurname == employeeDto.EmpSurname
                    && x.EmpPhoneNumber == employeeDto.EmpPhoneNumber)
                .Select(x => x.EmpId)
                .SingleOrDefaultAsync();
            EmployeeToUpdateDto employeeToUpdate = new EmployeeToUpdateDto()
            {
                EmpEmail = "1test1@test.pl",
                EmpName = "testUpdatedName",
                EmpSurname = "testUpdatedSurname",
                EmpPhoneNumber = "312-312-312",
                EmpLogin = "1test1@test.pl"
            };
            await _employeeRepository.UpdateEmployee(employeeId, employeeToUpdate);
            var employeeCountAfter = await _context
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