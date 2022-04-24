using System;
using OneBan_TMS.Enum;

namespace OneBan_TMS.Models.DTOs.Kanban
{
    public class KanbanElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public DateTime? DueDate { get; set; }
        public string Type { get; set; }
    }
}