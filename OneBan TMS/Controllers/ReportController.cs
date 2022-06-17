using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<TimeEntryHeaderDto>>> GetGroupDataForReport(int employeeId, string dateFrom,
            string dateTo, int groupType)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest(MessageHelper.GetBadRequestMessage("Employee does not exist"));
            if (!(DateTime.TryParse(dateFrom, out DateTime parsedDateFrom)))
                return BadRequest(MessageHelper.GetBadRequestMessage("DataFrom have bad format"));
            if (!(DateTime.TryParse(dateTo, out DateTime parsedDateTo)))
                return BadRequest(MessageHelper.GetBadRequestMessage("DateTo have bad format"));
            var result = await _reportRepository.GetGroupDataForReport(employeeId, parsedDateFrom, parsedDateTo, groupType);
            if (!(result.Any()))
                return NoContent();
            return Ok(result);
        }
    }
}