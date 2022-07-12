using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IEmployeePrivilegesHandler
    {
        Task DeletePrivilegesFromEmployee(int employeeId, IEnumerable<int> employeePrivileges);
    }
}