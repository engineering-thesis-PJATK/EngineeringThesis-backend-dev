using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class EmployeePrivilegesHandler : IEmployeePrivilegesHandler
    {
        private readonly OneManDbContext _context;
        public EmployeePrivilegesHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task DeletePrivilegesFromEmployee(int employeeId, IEnumerable<int> employeePrivileges)
        {
            var listOfExistingPriveleges = await _context
                .EmployeePrivilegeEmployees
                .Where(x => x.EpeIdEmployee == employeeId)
                .ToListAsync();
            var tmp = listOfExistingPriveleges.Where(x => employeePrivileges.Contains(x.EpeIdEmployeePrivilage));
        }
    }
}