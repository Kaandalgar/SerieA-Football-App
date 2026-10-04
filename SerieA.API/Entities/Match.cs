namespace SerieA.API.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int Week { get; set; }

        public DateTime MatchDate { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public string Status { get; set; }

        public string Stadium { get; set; }
    }
}