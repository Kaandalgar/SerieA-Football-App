namespace SerieA.WebUI.ViewModels
{
    public class AdminTeamPageViewModel
    {
        public List<TeamSummaryViewModel> Teams { get; set; } = new();
    }

    public class AdminTeamFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LogoUrl { get; set; } = "";
        public string City { get; set; } = "";
        public string Stadium { get; set; } = "";
    }
}
