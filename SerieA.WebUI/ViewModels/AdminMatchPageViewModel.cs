namespace SerieA.WebUI.ViewModels
{
    public class AdminMatchPageViewModel
    {
        public List<AdminMatchListItemViewModel> Matches { get; set; } = new();
        public List<TeamSummaryViewModel> Teams { get; set; } = new();
    }

    public class AdminMatchListItemViewModel
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string Status { get; set; } = "NotStarted";
        public string Stadium { get; set; } = "";
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public TeamSummaryViewModel? HomeTeam { get; set; }
        public TeamSummaryViewModel? AwayTeam { get; set; }
    }

    public class AdminMatchApiViewModel
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string Status { get; set; } = "NotStarted";
        public string Stadium { get; set; } = "";
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }

    public class AdminMatchFormViewModel
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public string Status { get; set; } = "NotStarted";
        public string Stadium { get; set; } = "";
    }
}
