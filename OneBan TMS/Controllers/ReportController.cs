using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Report;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ReportController(IReportRepository reportRepository, IEmployeeRepository employeeRepository)
        {
            _reportRepository = reportRepository;
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeEntryGroupedDto>>> GetGroupDataForReport(int employeeId, string dateFrom,
            string dateTo, int groupType)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Employee does not exist"));
            if (!(DateTime.TryParse(dateFrom, out DateTime parsedDateFrom)))
                return BadRequest(MessageProvider.GetBadRequestMessage("DataFrom have bad format"));
            if (!(DateTime.TryParse(dateTo, out DateTime parsedDateTo)))
                return BadRequest(MessageProvider.GetBadRequestMessage("DateTo have bad format"));
            var result = await _reportRepository.GetGroupDataForReport(employeeId, parsedDateFrom, parsedDateTo, groupType);
            if (!(result.Any()))
                return NoContent();
            return Ok(result);
        }
    }
}