using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class TicketNameHandler : ITicketNameHandler
    {
        private readonly OneManDbContext _context;
        public TicketNameHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetNewNameForTicket()
        {
            int? maxNumber = await GetTheBiggestNumber();
            if (maxNumber is null)
                throw new ArgumentException("Can not read tic name");
            string ticName = String.Format("#{0}", maxNumber.ToString());
            return ticName;
        }

        private async Task<int?> GetTheBiggestNumber()
        {
            string queryString = "SELECT dbo.f_biggest_number_of_ticket_name() AS [tic_max_number]";
            var connectionString = _context
                .Database.GetConnectionString();
            string result = "";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = queryString;
                    await sqlConnection.OpenAsync();
                    SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                    while (await sqlDataReader.ReadAsync())
                    {
                        result = sqlDataReader["tic_max_number"].ToString();
                    }
                }
            }
            if (int.TryParse(result,out int numberResult))
            {
                return numberResult;
            }
            return null;
        }
    }
}