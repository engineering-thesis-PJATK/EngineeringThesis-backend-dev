using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Kanban;
using OneBan_TMS.Models.DTOs.OrganizationalTask;

namespace OneBan_TMS.Controllers
{
     [Route("Api/[controller]")]
     [ApiController]
     public class KanbanController : ControllerBase
    {
        private readonly IKanbanRepository _kanbanRepository;
        private readonly IOrganizationalTaskStatusHandler _organizationalTaskStatusHandler;
        private readonly IOrganizationalTaskRepository _organizationalTaskRepository;
        private readonly ITicketStatusHandler _ticketStatusHandler;
        public KanbanController(IKanbanRepository kanbanRepository, IOrganizationalTaskStatusHandler organizationalTaskStatusHandler, IOrganizationalTaskRepository organizationalTaskRepository, ITicketStatusHandler ticketStatusHandler)
        {
            _kanbanRepository = kanbanRepository;
            _organizationalTaskStatusHandler = organizationalTaskStatusHandler;
            _organizationalTaskRepository = organizationalTaskRepository;
            _ticketStatusHandler = ticketStatusHandler;
        }
        [HttpGet]
        public async Task<ActionResult<List<KanbanElement>>> GetKanbanElements(int employeeId, string status)
        {
            if (!(await _organizationalTaskStatusHandler.StatusExists(status)) &&
                !(await _ticketStatusHandler.TicketStatusExists(status)))
                return NoContent();
            var kanbanElementsList = await _kanbanRepository.GetKanbanElements(employeeId, status);
            if (kanbanElementsList is null)
                return NoContent();
            return Ok(kanbanElementsList);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateElementStatus([FromBody]UpdatedElement element)
        {
            switch (element.ElementType)
            {
                case (int)KanbanType.Ticket:
                    if (!(await _ticketStatusHandler.TicketStatusExists(element.Status)))
                        return BadRequest("Ticket status not exists");
                    break;
                case (int)KanbanType.Task:
                    if (!(await _organizationalTaskStatusHandler.StatusExists(element.Status)))
                        return BadRequest("Task status not exists");
                    break;
                default:
                    return BadRequest("Kanban element not exists");
            }
            await _kanbanRepository.UpdateKanbanElementStatus(element.ElementId, element.ElementType, element.Status);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrganizationalTask([FromBody] NewOrganizationalTask organizationalTask)
        {
            if (!(await _organizationalTaskStatusHandler.StatusExists(organizationalTask.otk_OrganizationalTaskStatus)))
                return BadRequest("Task status not exists");
            var organizationalTaskId = await _organizationalTaskRepository.AddNewOrganizationalTask(organizationalTask);
            return Ok(organizationalTaskId);
        }
    }
}