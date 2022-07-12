using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ITimeHandler
    {
        string GetTimeFromTicks(long ticks);
    }
}