using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerieA.API.Context;

namespace SerieA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly SerieAContext _context;

        public StandingsController(SerieAContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandings()
        {
            var teams = await _context.Teams.ToListAsync();

            var finishedMatches = await _context.Matches
                .Where(x => x.Status == "Finished")
                .ToListAsync();

            var standings = teams.Select(team =>
            {
                var teamMatches = finishedMatches
                    .Where(x =>
                        x.HomeTeamId == team.Id ||
                        x.AwayTeamId == team.Id)
                    .ToList();

                int played = teamMatches.Count;
                int won = 0;
                int drawn = 0;
                int lost = 0;
                int goalsFor = 0;
                int goalsAgainst = 0;

                foreach (var match in teamMatches)
                {
                    int teamScore;
                    int opponentScore;

                    if (match.HomeTeamId == team.Id)
                    {
                        teamScore = match.HomeScore ?? 0;
                        opponentScore = match.AwayScore ?? 0;
                    }
                    else
                    {
                        teamScore = match.AwayScore ?? 0;
                        opponentScore = match.HomeScore ?? 0;
                    }

                    goalsFor += teamScore;
                    goalsAgainst += opponentScore;

                    if (teamScore > opponentScore)
                    {
                        won++;
                    }
                    else if (teamScore == opponentScore)
                    {
                        drawn++;
                    }
                    else
                    {
                        lost++;
                    }
                }

                // SON 5 MAÇ
                var lastFive = teamMatches
                    .OrderByDescending(x => x.MatchDate)
                    .Take(5)
                    .Select(match =>
                    {
                        int teamScore;
                        int opponentScore;

                        if (match.HomeTeamId == team.Id)
                        {
                            teamScore = match.HomeScore ?? 0;
                            opponentScore = match.AwayScore ?? 0;
                        }
                        else
                        {
                            teamScore = match.AwayScore ?? 0;
                            opponentScore = match.HomeScore ?? 0;
                        }

                        if (teamScore > opponentScore)
                            return "G";

                        if (teamScore == opponentScore)
                            return "B";

                        return "M";
                    })
                    .Reverse()
                    .ToList();

                int points = (won * 3) + drawn;
                int goalDifference = goalsFor - goalsAgainst;

                return new
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    LogoUrl = team.LogoUrl,

                    Played = played,
                    Won = won,
                    Drawn = drawn,
                    Lost = lost,

                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,

                    GoalDifference = goalDifference,
                    Points = points,

                    LastFive = lastFive
                };
            })
            .OrderByDescending(x => x.Points)
            .ThenByDescending(x => x.GoalDifference)
            .ThenByDescending(x => x.GoalsFor)
            .ToList();

            var result = standings
                .Select((team, index) => new
                {
                    Position = index + 1,

                    team.TeamId,
                    team.TeamName,
                    team.LogoUrl,

                    team.Played,
                    team.Won,
                    team.Drawn,
                    team.Lost,

                    team.GoalsFor,
                    team.GoalsAgainst,

                    team.GoalDifference,
                    team.Points,

                    team.LastFive
                });

            return Ok(result);
        }
    }
}