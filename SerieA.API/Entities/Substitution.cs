namespace SerieA.API.Entities
{
    public class Substitution
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public int TeamId { get; set; }

        public string PlayerIn { get; set; }

        public string PlayerOut { get; set; }

        public int Minute { get; set; }
    }
}