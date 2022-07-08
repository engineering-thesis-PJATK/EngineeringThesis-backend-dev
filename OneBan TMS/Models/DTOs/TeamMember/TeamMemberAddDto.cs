namespace OneBan_TMS.Models.DTOs.TeamMember
{
    public class TeamMemberAddDto
    {
        public int TmrIdTeam { get; set; }
        public int TmrIdEmployee { get; set; }
        public int TmrIdRole { get; set; }

        public EmployeeTeam GetEmployeeTeam()
        {
            return new EmployeeTeam()
            {
                EtmIdEmployee = this.TmrIdTeam,
                EtmIdTeam = this.TmrIdEmployee,
                EtmIdRole = this.TmrIdRole
            };
        }
    }
}