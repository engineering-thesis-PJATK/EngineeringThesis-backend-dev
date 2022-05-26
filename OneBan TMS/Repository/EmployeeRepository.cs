using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private OneManDbContext _context;
        private readonly IPasswordHandler _passwordHandler;
        public EmployeeRepository(OneManDbContext context, IPasswordHandler passwordHandler)
        {
            _context = context;
            _passwordHandler = passwordHandler;
        }
        public async Task<IEnumerable<EmployeeForListDto>> GetAllEmployeeDto()
        {
            List<EmployeeForListDto> employeeDtoList = new List<EmployeeForListDto>();
            var employees = await _context
                .Employees
                .Select(x => new
                {
                    x.EmpId,
                    x.EmpLogin,
                    x.EmpName,
                    x.EmpSurname,
                    x.EmpEmail,
                    x.EmpPhoneNumber
                }).ToListAsync();
            foreach (var emp in employees )
            {
                employeeDtoList.Add(new EmployeeForListDto()
                {
                    EmpId = emp.EmpId,
                    EmpLogin = emp.EmpLogin,
                    EmpName = emp.EmpName,
                    EmpSurname = emp.EmpSurname,
                    EmpEmail = emp.EmpEmail,
                    EmpPhoneNumber = emp.EmpPhoneNumber,
                    Roles = await GetEmployeePrivileges(emp.EmpId)
                });
            }
            return employeeDtoList;
        }

        public async Task<EmployeeForListDto> GetEmployeeByIdDto(int idEmployee)
        {
            var employee = await _context
                .Employees
                .Include(x => x.EmployeeTeams)
                .ThenInclude(y => y.EtmIdTeamNavigation)
                .Select(x => new
                {
                    x.EmpId,
                    x.EmpLogin,
                    x.EmpName,
                    x.EmpSurname,
                    x.EmpEmail,
                    x.EmpPhoneNumber,
                    Teams = x.EmployeeTeams.Select(y => y.EtmIdTeamNavigation)
                })
                .Where(x => x.EmpId == idEmployee)
                .FirstOrDefaultAsync();
            return new EmployeeForListDto()
            {
                EmpId = employee.EmpId,
                EmpLogin = employee.EmpLogin,
                EmpName = employee.EmpName,
                EmpSurname = employee.EmpSurname,
                EmpEmail = employee.EmpEmail,
                EmpPhoneNumber = employee.EmpPhoneNumber,
                Roles = await GetEmployeePrivileges(employee.EmpId),
                EmployeeTeams = employee.Teams
            };
        }

        public async Task<Employee> AddEmployee(EmployeeDto newEmployee)
        {
            _passwordHandler.CreatePasswordHash(newEmployee.EmpPassword, out byte[] passwordHash, out byte[] passwordSalt);
            StringBuilder passwordConnector = new StringBuilder();
            passwordConnector.Append(_passwordHandler.ConvertByteArrayToString(passwordHash));
            passwordConnector.Append(_passwordHandler.ConvertByteArrayToString(passwordSalt));
            var employee = new Employee()
            {
                EmpLogin = newEmployee.EmpEmail,
                EmpName = newEmployee.EmpName,
                EmpSurname = newEmployee.EmpSurname,
                EmpEmail = newEmployee.EmpEmail,
                EmpPhoneNumber = newEmployee.EmpPhoneNumber,
                EmpCreatedAt = DateTime.Now,
                EmpPassword = passwordConnector.ToString()
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task UpdateEmployee(int employeeId, EmployeeToUpdate employeeUpdated)
        { 
            var employeeToUpdate = await _context
                .Employees
                .Where(x => x.EmpId == employeeId)
                .SingleOrDefaultAsync();
            employeeToUpdate.EmpLogin = employeeUpdated.EmpLogin;
            employeeToUpdate.EmpEmail = employeeUpdated.EmpEmail;
            employeeToUpdate.EmpName = employeeUpdated.EmpName;
            employeeToUpdate.EmpSurname = employeeUpdated.EmpSurname;
            employeeToUpdate.EmpPhoneNumber = employeeUpdated.EmpPhoneNumber;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsEmployee(int employeeId)
        {
            return await _context
                .Employees
                .Where(x => x.EmpId == employeeId)
                .AnyAsync();
        }
        public async Task<List<EmployeePrivilegeGetDto>> GetEmployeePrivileges()
        {
            var privilages = await _context
                                                      .EmployeePrivileges
                                                      .ToListAsync();
            return 
                privilages
                .Select(ChangeEmployeePrivilegeBaseToDto)
                .ToList();
        }

        public async Task<EmployeePrivilegeGetDto> GetEmployeePrivilegeById(int privilegeId)
        {
            var privilage = await _context
                                                     .EmployeePrivileges
                                                     .Where(privilege => privilege.EpvId == privilegeId)
                                                     .SingleOrDefaultAsync();
            if (privilage is not null)
            {
                return
                    ChangeEmployeePrivilegeBaseToDto(privilage);
            }

            return
                null;
        }

        public async Task<bool> ExistsEmployeePrivileges(List<int> privileges)
        {
            foreach (int privilege in privileges)
            {
                if (!await _context.EmployeePrivileges.Where(x => x.EpvId == privilege).AnyAsync())
                    return false;
            }
            return true;
        }

        public async Task<string> ChangePassword(string employeeEmail)
        {
            var employee = await GetEmployeeByEmail(employeeEmail);
            if (employee is null)
                throw new ArgumentException("Employee does not exist");
            string newRandomPassword = GenerateRandomPassword();
            _passwordHandler.CreatePasswordHash(newRandomPassword, out byte[] passwordHash, out byte[] passwordSalt);
            StringBuilder passwordBuilder = new StringBuilder();
            passwordBuilder.Append(_passwordHandler.ConvertByteArrayToString(passwordHash));
            passwordBuilder.Append(_passwordHandler.ConvertByteArrayToString(passwordSalt));
            employee.EmpPassword = passwordBuilder.ToString();
            await _context.SaveChangesAsync();
            return newRandomPassword;
        }

        private async Task<Employee> GetEmployeeByEmail(string employeeEmail)
        {
            var employee = await _context
                .Employees
                .Where(x =>
                    x.EmpEmail.Equals(employeeEmail))
                .SingleOrDefaultAsync();
            return employee;
        }

        private string GenerateRandomPassword()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();
            char randomChar;
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                randomChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                passwordBuilder.Append(randomChar);
            }
            return passwordBuilder.ToString();
        }
        private EmployeePrivilegeGetDto ChangeEmployeePrivilegeBaseToDto(EmployeePrivilege employeePrivilege)
        {
            return 
                new EmployeePrivilegeGetDto()
                {
                    EpvDescription = employeePrivilege.EpvDescription,
                    EpvId = employeePrivilege.EpvId,
                    EpvName = employeePrivilege.EpvName
                };
        }

        private async Task<List<EmployeePrivilege>> GetEmployeePrivileges(int employeeId)
        {
            var privileges = await _context
                .EmployeePrivilegeEmployees
                .Where(x =>
                    x.EpeIdEmployeeNavigation.EmpId == employeeId)
                .Select(x => new
                {
                    x.EpeIdEmployeePrivilageNavigation.EpvId,
                    x.EpeIdEmployeePrivilageNavigation.EpvName,
                    x.EpeIdEmployeePrivilageNavigation.EpvDescription
                })
                .ToListAsync();
            List<EmployeePrivilege> employeePrivileges = new List<EmployeePrivilege>();
            privileges.ForEach(x => 
                employeePrivileges.Add(new EmployeePrivilege()
                {
                    EpvId = x.EpvId,
                    EpvName = x.EpvName,
                    EpvDescription = x.EpvDescription
                }));
            return employeePrivileges;
        }

        public async Task<bool> ExistsEmployeeByEmail(string employeeEmail)
        {
            bool result = await _context
                .Employees
                .AnyAsync(x => x.EmpEmail.Equals(employeeEmail));
            return result;
        }
        public async Task AddPrivilegesToUser(int employeeId, List<int> privileges)
        {
            foreach (int privilege in privileges)
            {
                await _context.EmployeePrivilegeEmployees.AddAsync(new EmployeePrivilegeEmployee()
                {
                    EpeIdEmployee = employeeId,
                    EpeIdEmployeePrivilage = privilege
                });
            }

            await _context.SaveChangesAsync();
        }
        
    }
}