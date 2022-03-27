using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private OneManDbContext _context;
        public EmployeeRepository(OneManDbContext context)
        {
            _context = context;
        }
        public IEnumerable<EmployeeDto> GetAllEmployeeDto()
        {
            List<EmployeeDto> employeeDtoList = new List<EmployeeDto>();
            var employees = _context
                .Employees
                .Include(x => x.EmployeePrivilegeEmployees)
                .ThenInclude(y => y.EpeIdEmployeePrivilageNavigation)
                .Select(x => new
                {
                    x.EmpId,
                    x.EmpLogin,
                    x.EmpName,
                    x.EmpSurname,
                    x.EmpEmail,
                    x.EmpPhoneNumber,
                    Roles = x.EmployeePrivilegeEmployees.Select(y => y.EpeIdEmployeePrivilageNavigation.EpvName),
                }).ToList();
            foreach (var emp in employees )
            {
                employeeDtoList.Add(new EmployeeDto()
                {
                    EmpId = emp.EmpId,
                    EmpLogin = emp.EmpLogin,
                    EmpName = emp.EmpName,
                    EmpSurname = emp.EmpSurname,
                    EmpEmail = emp.EmpEmail,
                    EmpPhoneNumber = emp.EmpPhoneNumber,
                    Roles = emp.Roles
                });
            }

            return employeeDtoList;
        }

        public EmployeeDto GetEmployeeByIdDto(int idEmployee)
        {
            var employee = _context
                .Employees
                .Include(x => x.EmployeePrivilegeEmployees)
                .ThenInclude(y => y.EpeIdEmployeePrivilageNavigation)
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
                    Roles = x.EmployeePrivilegeEmployees.Select(y => y.EpeIdEmployeePrivilageNavigation.EpvName),
                    Teams = x.EmployeeTeams.Select(y => y.EtmIdTeamNavigation)
                })
                .Where(x => x.EmpId == idEmployee)
                .FirstOrDefault();
            return new EmployeeDto()
            {
                EmpId = employee.EmpId,
                EmpLogin = employee.EmpLogin,
                EmpName = employee.EmpName,
                EmpSurname = employee.EmpSurname,
                EmpEmail = employee.EmpEmail,
                EmpPhoneNumber = employee.EmpPhoneNumber,
                Roles = employee.Roles,
                EmployeeTeams = employee.Teams
            };
        }
    }
}