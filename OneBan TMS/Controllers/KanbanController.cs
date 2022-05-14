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
        public async Task<ActionResult<List<KanbanElement>>> GetKanbanElements(int employeeId, string status)
        {
            var kanbanElementsList = await _kanbanRepository.GetKanbanElements(employeeId, status);
            if (kanbanElementsList is null)
                return NoContent();
            return Ok(kanbanElementsList);
        }
    /*
        [HttpGet("/a")]
        public async Task<IActionResult> UpdateElementStatus(int elementId, int elementType, string status)
        {
            await _kanbanRepository.UpdateKanbanElementStatus(elementId, elementType, status);
            return Ok();
        }
      */
        [HttpPut]
        public async Task<IActionResult> UpdateElementStatus([FromBody]UpdatedElement element)
        {
            await _kanbanRepository.UpdateKanbanElementStatus(element.ElementId, element.ElementType, element.Status);
            return Ok();
        }
    }
}