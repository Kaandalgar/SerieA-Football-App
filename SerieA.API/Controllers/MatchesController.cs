using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerieA.API.Context;
using SerieA.API.Entities;

namespace SerieA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly SerieAContext _context;

        public MatchesController(SerieAContext context)
        {
            _context = context;
        }

        // GET: api/matches
        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            return Ok(matches);
        }

        // GET: api/matches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            var homeTeam = await _context.Teams.FindAsync(match.HomeTeamId);
            var awayTeam = await _context.Teams.FindAsync(match.AwayTeamId);

            var goals = await _context.MatchGoals
                .Where(x => x.MatchId == id)
                .OrderBy(x => x.Minute)
                .ToListAsync();

            var cards = await _context.MatchCards
                .Where(x => x.MatchId == id)
                .OrderBy(x => x.Minute)
                .ToListAsync();

            var substitutions = await _context.Substitutions
                .Where(x => x.MatchId == id)
                .OrderBy(x => x.Minute)
                .ToListAsync();

            var result = new
            {
                match.Id,
                match.Week,
                match.MatchDate,
                match.HomeScore,
                match.AwayScore,
                match.Status,
                match.Stadium,

                HomeTeam = homeTeam,
                AwayTeam = awayTeam,

                Goals = goals,
                Cards = cards,
                Substitutions = substitutions
            };

            return Ok(result);
        }

        // POST: api/matches
        [HttpPost]
        public async Task<IActionResult> CreateMatch(Match match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                return BadRequest("Ev sahibi ve deplasman takımı aynı olamaz.");
            }

            var teamAlreadyHasMatch = await _context.Matches.AnyAsync(x =>
                x.Week == match.Week &&
                (x.HomeTeamId == match.HomeTeamId ||
                 x.AwayTeamId == match.HomeTeamId ||
                 x.HomeTeamId == match.AwayTeamId ||
                 x.AwayTeamId == match.AwayTeamId));

            if (teamAlreadyHasMatch)
            {
                return BadRequest("Bir takım aynı hafta içerisinde birden fazla maçta yer alamaz.");
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return Ok(match);
        }

        // PUT: api/matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, Match match)
        {
            var existingMatch = await _context.Matches.FindAsync(id);

            if (existingMatch == null)
            {
                return NotFound();
            }

            if (match.HomeTeamId == match.AwayTeamId)
            {
                return BadRequest("Ev sahibi ve deplasman takımı aynı olamaz.");
            }

            existingMatch.HomeTeamId = match.HomeTeamId;
            existingMatch.AwayTeamId = match.AwayTeamId;
            existingMatch.Week = match.Week;
            existingMatch.MatchDate = match.MatchDate;
            existingMatch.HomeScore = match.HomeScore;
            existingMatch.AwayScore = match.AwayScore;
            existingMatch.Status = match.Status;
            existingMatch.Stadium = match.Stadium;

            await _context.SaveChangesAsync();

            return Ok(existingMatch);
        }

        // DELETE: api/matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{matchId}/goals")]
        public async Task<IActionResult> AddGoal(int matchId, MatchGoal goal)
        {
            var match = await _context.Matches.FindAsync(matchId);

            if (match == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            var teamExists = await _context.Teams.AnyAsync(x => x.Id == goal.TeamId);

            if (!teamExists)
            {
                return BadRequest("Takım bulunamadı.");
            }

            if (goal.TeamId != match.HomeTeamId && goal.TeamId != match.AwayTeamId)
            {
                return BadRequest("Golü atan takım bu maçta yer almıyor.");
            }

            goal.MatchId = matchId;

            _context.MatchGoals.Add(goal);
            await _context.SaveChangesAsync();

            return Ok(goal);
        }

        [HttpPost("{matchId}/cards")]
        public async Task<IActionResult> AddCard(int matchId, MatchCard card)
        {
            var match = await _context.Matches.FindAsync(matchId);

            if (match == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            var teamExists = await _context.Teams.AnyAsync(x => x.Id == card.TeamId);

            if (!teamExists)
            {
                return BadRequest("Takım bulunamadı.");
            }

            if (card.TeamId != match.HomeTeamId && card.TeamId != match.AwayTeamId)
            {
                return BadRequest("Kart gören takım bu maçta yer almıyor.");
            }

            card.MatchId = matchId;

            _context.MatchCards.Add(card);
            await _context.SaveChangesAsync();

            return Ok(card);
        }


        [HttpPost("{matchId}/substitutions")]
        public async Task<IActionResult> AddSubstitution(int matchId, Substitution substitution)
        {
            var match = await _context.Matches.FindAsync(matchId);

            if (match == null)
            {
                return NotFound("Maç bulunamadı.");
            }

            var teamExists = await _context.Teams
                .AnyAsync(x => x.Id == substitution.TeamId);

            if (!teamExists)
            {
                return BadRequest("Takım bulunamadı.");
            }

            if (substitution.TeamId != match.HomeTeamId &&
                substitution.TeamId != match.AwayTeamId)
            {
                return BadRequest("Oyuncu değişikliği yapan takım bu maçta yer almıyor.");
            }

            substitution.MatchId = matchId;

            _context.Substitutions.Add(substitution);
            await _context.SaveChangesAsync();

            return Ok(substitution);
        }


        // GET: api/matches/week/1
        [HttpGet("week/{week}")]
        public async Task<IActionResult> GetMatchesByWeek(int week)
        {
            var matches = await _context.Matches
                .Where(x => x.Week == week)
                .OrderBy(x => x.MatchDate)
                .ToListAsync();

            var teams = await _context.Teams.ToListAsync();

            var result = matches.Select(match =>
            {
                var homeTeam = teams.FirstOrDefault(x => x.Id == match.HomeTeamId);
                var awayTeam = teams.FirstOrDefault(x => x.Id == match.AwayTeamId);

                return new
                {
                    match.Id,
                    match.Week,
                    match.MatchDate,
                    match.HomeScore,
                    match.AwayScore,
                    match.Status,
                    match.Stadium,

                    HomeTeam = new
                    {
                        Id = homeTeam?.Id,
                        Name = homeTeam?.Name,
                        LogoUrl = homeTeam?.LogoUrl
                    },

                    AwayTeam = new
                    {
                        Id = awayTeam?.Id,
                        Name = awayTeam?.Name,
                        LogoUrl = awayTeam?.LogoUrl
                    }
                };
            });

            return Ok(result);
        }
    }
}