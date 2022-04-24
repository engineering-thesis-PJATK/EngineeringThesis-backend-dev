using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IKanbanRepository
    {
        Task<List<KanbanElement>> GetKanbanElements(int employeeId, int statusId);
        Task UpdateKanbanElementStatus(int elementId, int elementType, int statusId);
    }
}