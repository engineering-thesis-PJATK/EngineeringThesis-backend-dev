using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Handlers
{
    public class TimeHandler : ITimeHandler
    {
        private readonly OneManDbContext _context;
        public TimeHandler(OneManDbContext context)
        {
            _context = context;
        }
        public string GetTimeFromTicks(long ticks)
        {
            TimeSpan intervals = TimeSpan.FromTicks(ticks);
            return intervals.ToString();
        }
    }
}