namespace SerieA.WebUI.ViewModels
{
    public class MatchDetailViewModel
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

        public List<GoalViewModel> Goals { get; set; } = new();
        public List<CardViewModel> Cards { get; set; } = new();
        public List<SubstitutionViewModel> Substitutions { get; set; } = new();
    }

    public class GoalViewModel
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; }
        public int Minute { get; set; }
    }

    public class CardViewModel
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string PlayerName { get; set; }
        public int Minute { get; set; }
        public string CardType { get; set; }
    }

    public class SubstitutionViewModel
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public string PlayerIn { get; set; }
        public string PlayerOut { get; set; }
        public int Minute { get; set; }
    }
}