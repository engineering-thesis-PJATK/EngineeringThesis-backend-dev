using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            IEnumerable<Team> teams = await _teamRepository.GetTeams();
            if (teams is null)
                return NotFound();
            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            Team team = await _teamRepository.GetTeamById(teamId);
            if (team is null)
            {
                return NotFound();
            }

            return Ok(team);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody]TeamDto teamDto)
        {
            await _teamRepository.AddNewTeam(teamDto);
            return Ok("Added new team");
        }
        [HttpPut("{teamId}")]
        public async Task<IActionResult> UpdateTeam([FromBody]TeamDto teamDto, int teamId)
        {
            await _teamRepository.UpdateTeam(teamDto, teamId);
            return Ok("Team updated");
        }

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            await _teamRepository.DeleteTeam(teamId);
            return Ok("Team deleted");
        }
    }
}