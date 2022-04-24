using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Repository
{
    public class KanbanRepository : IKanbanRepository
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IOrganizationalTaskRepository _organizationalTaskRepository;
        public KanbanRepository(ITicketRepository ticketRepository, IOrganizationalTaskRepository organizationalTaskRepository)
        {
            _ticketRepository = ticketRepository;
            _organizationalTaskRepository = organizationalTaskRepository;
        }

        public async Task<List<KanbanElement>> GetKanbanElements(int employeeId, int statusId)
        {
            List<KanbanElement> kanbanElements = new List<KanbanElement>();
            kanbanElements.AddRange(await _ticketRepository.GetTicketsForEmployeeByStatus(statusId, employeeId));
            //kanbanElements.AddRange(await _organizationalTaskRepository.GetTaskForEmployee(employeeId));
            return kanbanElements;
        }

        public async Task UpdateKanbanElementStatus(int elementId, int elementType, int statusId)
        {
            switch (elementType)
            {
                case 0:
                    await _ticketRepository.UpdateTicketStatusId(elementId, statusId);
                    break;
                case 1:
                    await _organizationalTaskRepository.UpdateTaskStatus(elementId, statusId);
                    break;
                default:
                    throw new ArgumentException("Element type is not valid");
            }
            
        }
    }
}