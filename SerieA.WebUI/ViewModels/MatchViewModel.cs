namespace SerieA.WebUI.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public DateTime MatchDate { get; set; }

        public int? HomeScore { get; set; }

        public int? AwayScore { get; set; }

        public string Status { get; set; }

        public string Stadium { get; set; }

        public TeamSummaryViewModel HomeTeam { get; set; }

        public TeamSummaryViewModel AwayTeam { get; set; }
    }

    public class TeamSummaryViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public string City { get; set; }
        public string Stadium { get; set; }
    }
}