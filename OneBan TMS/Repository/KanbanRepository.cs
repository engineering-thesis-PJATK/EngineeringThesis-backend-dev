using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Repository
{
    public class KanbanRepository : IKanbanRepository
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IOrganizationalTaskRepository _organizationalTaskRepository;
        private readonly IOrganizationalTaskStatusHandler _taskStatusHandler;
        
        public KanbanRepository(ITicketRepository ticketRepository, IOrganizationalTaskRepository organizationalTaskRepository, IOrganizationalTaskStatusHandler taskStatusHandler)
        {
            _ticketRepository = ticketRepository;
            _organizationalTaskRepository = organizationalTaskRepository;
            _taskStatusHandler = taskStatusHandler;
        }
        public async Task<List<KanbanElement>> GetKanbanElements(int employeeId, string status)
        {
            List<KanbanElement> kanbanElements = new List<KanbanElement>();
            int ticketsStatusId = await _ticketRepository.GetTicketStatusId(status);
            int tasksStatusId = await _taskStatusHandler.GetStatusId(status);
            kanbanElements.AddRange(await _ticketRepository.GetTicketsForEmployeeByStatus(ticketsStatusId, employeeId));
            kanbanElements.AddRange(await _organizationalTaskRepository.GetTaskForEmployee(tasksStatusId, employeeId));
            return kanbanElements;
        }
        public async Task UpdateKanbanElementStatus(int elementId, int elementType, string status)
        {
            int statusId;
            switch (elementType)
            {
                case (int)KanbanType.Ticket:
                    statusId = await _ticketRepository.GetTicketStatusId(status);
                    await _ticketRepository.UpdateTicketStatus(elementId, statusId);
                    break;
                case (int)KanbanType.Task:
                    statusId = await _taskStatusHandler.GetStatusId(status);
                    await _organizationalTaskRepository.UpdateTaskStatus(elementId, statusId);
                    break;
                default:
                    throw new ArgumentException("Element type is not valid");
            }
            
        }
    }
}