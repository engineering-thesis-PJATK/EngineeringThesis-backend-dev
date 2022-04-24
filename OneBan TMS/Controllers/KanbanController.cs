using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Controllers
{
     [Route("Api/[controller]")]
     [ApiController]
     public class KanbanController : ControllerBase
    {
        private readonly IKanbanRepository _kanbanRepository;
        public KanbanController(IKanbanRepository kanbanRepository)
        {
            _kanbanRepository = kanbanRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<KanbanElement>>> getKanbanElements(int employeeId, int statusId)
        {
            var kanbanElementsList = await _kanbanRepository.getKanbanElements(employeeId, statusId);
            if (kanbanElementsList is null)
                return NoContent();
            return Ok(kanbanElementsList);
        }
    }
}