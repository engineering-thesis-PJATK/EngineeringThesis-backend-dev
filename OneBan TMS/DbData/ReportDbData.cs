using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.DbData;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.DbData
{
    public class ReportDbData : IReportDbData
    {
        private readonly OneManDbContext _context;
        public ReportDbData(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TimeEntryReportDto>> GetDataFromDb(int employeeId, DateTime dateFrom, DateTime dateTo)
        { 
            string queryString = @"SELECT 
	                                  [ter_id]
	                                , [ter_ticketTitle]
	                                , [ter_description]
	                                , [ter_timeValue]
	                                , [ter_date]
	                                , [ter_Company]
                                FROM DBO.GroupReport(@EmployeeId, @DateFrom, @DateTo)";
            var connectionString = _context
                .Database
                .GetConnectionString();
            List<TimeEntryReportDto> results = new List<TimeEntryReportDto>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = queryString;
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    sqlCommand.Parameters.AddWithValue("@DateFrom", dateFrom);
                    sqlCommand.Parameters.AddWithValue("@DateTo", dateTo);
                    await sqlConnection.OpenAsync();
                    SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                    while (await sqlDataReader.ReadAsync())
                    {
                        results.Add(new TimeEntryReportDto()
                        {
                            TerId = int.Parse(sqlDataReader["ter_id"].ToString() ?? throw new InvalidOperationException()),
                            TerTicketTitle = sqlDataReader["ter_ticketTitle"].ToString(),
                            TerDescription = sqlDataReader["ter_description"].ToString(),
                            TerTimeValue = TimeSpan.Parse(sqlDataReader["ter_timeValue"].ToString() ?? throw new InvalidOperationException()),
                            TerDate = DateTime.Parse(sqlDataReader["ter_date"].ToString() ?? throw new InvalidOperationException()),
                            TerCompany = sqlDataReader["ter_Company"].ToString()
                        });
                    }
                }
            }
            return results;
        }
    }
}