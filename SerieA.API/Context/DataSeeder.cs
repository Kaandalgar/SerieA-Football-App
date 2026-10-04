using SerieA.API.Entities;

namespace SerieA.API.Context
{
    public static class DataSeeder
    {
        public static void Seed(SerieAContext context)
        {
            // -------------------------------------------------
            // 1) TAKIMLAR
            // -------------------------------------------------
            if (!context.Teams.Any())
            {
                var teams = new List<Team>
                {
                    new Team { Name = "Inter", City = "Milano", Stadium = "San Siro", LogoUrl = "" },
                    new Team { Name = "Milan", City = "Milano", Stadium = "San Siro", LogoUrl = "" },
                    new Team { Name = "Juventus", City = "Torino", Stadium = "Allianz Stadium", LogoUrl = "" },
                    new Team { Name = "Roma", City = "Roma", Stadium = "Stadio Olimpico", LogoUrl = "" },
                    new Team { Name = "Lazio", City = "Roma", Stadium = "Stadio Olimpico", LogoUrl = "" },
                    new Team { Name = "Napoli", City = "Napoli", Stadium = "Diego Armando Maradona", LogoUrl = "" },
                    new Team { Name = "Atalanta", City = "Bergamo", Stadium = "Gewiss Stadium", LogoUrl = "" },
                    new Team { Name = "Fiorentina", City = "Firenze", Stadium = "Artemio Franchi", LogoUrl = "" },
                    new Team { Name = "Bologna", City = "Bologna", Stadium = "Renato Dall'Ara", LogoUrl = "" },
                    new Team { Name = "Torino", City = "Torino", Stadium = "Olimpico Grande Torino", LogoUrl = "" },
                    new Team { Name = "Genoa", City = "Genova", Stadium = "Luigi Ferraris", LogoUrl = "" },
                    new Team { Name = "Cagliari", City = "Cagliari", Stadium = "Unipol Domus", LogoUrl = "" },
                    new Team { Name = "Lecce", City = "Lecce", Stadium = "Via del Mare", LogoUrl = "" },
                    new Team { Name = "Udinese", City = "Udine", Stadium = "Bluenergy Stadium", LogoUrl = "" },
                    new Team { Name = "Parma", City = "Parma", Stadium = "Ennio Tardini", LogoUrl = "" },
                    new Team { Name = "Como", City = "Como", Stadium = "Giuseppe Sinigaglia", LogoUrl = "" },
                    new Team { Name = "Verona", City = "Verona", Stadium = "Marcantonio Bentegodi", LogoUrl = "" },
                    new Team { Name = "Monza", City = "Monza", Stadium = "U-Power Stadium", LogoUrl = "" },
                    new Team { Name = "Sassuolo", City = "Sassuolo", Stadium = "Mapei Stadium", LogoUrl = "" },
                    new Team { Name = "Empoli", City = "Empoli", Stadium = "Carlo Castellani", LogoUrl = "" }
                };

                context.Teams.AddRange(teams);
                context.SaveChanges();
            }

            // -------------------------------------------------
            // 2) MAÇLAR
            // -------------------------------------------------
            if (!context.Matches.Any())
            {
                var teamIds = context.Teams
                    .OrderBy(x => x.Id)
                    .Select(x => x.Id)
                    .ToList();

                var matches = new List<Match>
                {
                    // 1. HAFTA
                    new Match { HomeTeamId = teamIds[0], AwayTeamId = teamIds[1], Week = 1, MatchDate = new DateTime(2026, 10, 10, 20, 45, 0), HomeScore = 2, AwayScore = 1, Status = "Finished", Stadium = "San Siro" },
                    new Match { HomeTeamId = teamIds[2], AwayTeamId = teamIds[3], Week = 1, MatchDate = new DateTime(2026, 10, 11, 18, 0, 0), HomeScore = 1, AwayScore = 1, Status = "Finished", Stadium = "Allianz Stadium" },
                    new Match { HomeTeamId = teamIds[4], AwayTeamId = teamIds[5], Week = 1, MatchDate = new DateTime(2026, 10, 11, 20, 45, 0), HomeScore = 0, AwayScore = 2, Status = "Finished", Stadium = "Stadio Olimpico" },
                    new Match { HomeTeamId = teamIds[6], AwayTeamId = teamIds[7], Week = 1, MatchDate = new DateTime(2026, 10, 12, 15, 0, 0), HomeScore = 3, AwayScore = 1, Status = "Finished", Stadium = "Gewiss Stadium" },
                    new Match { HomeTeamId = teamIds[8], AwayTeamId = teamIds[9], Week = 1, MatchDate = new DateTime(2026, 10, 12, 18, 0, 0), HomeScore = 1, AwayScore = 0, Status = "Finished", Stadium = "Renato Dall'Ara" },
                    new Match { HomeTeamId = teamIds[10], AwayTeamId = teamIds[11], Week = 1, MatchDate = new DateTime(2026, 10, 12, 20, 45, 0), HomeScore = 2, AwayScore = 2, Status = "Finished", Stadium = "Luigi Ferraris" },
                    new Match { HomeTeamId = teamIds[12], AwayTeamId = teamIds[13], Week = 1, MatchDate = new DateTime(2026, 10, 13, 15, 0, 0), HomeScore = 1, AwayScore = 0, Status = "Finished", Stadium = "Via del Mare" },
                    new Match { HomeTeamId = teamIds[14], AwayTeamId = teamIds[15], Week = 1, MatchDate = new DateTime(2026, 10, 13, 18, 0, 0), HomeScore = 0, AwayScore = 0, Status = "Finished", Stadium = "Ennio Tardini" },
                    new Match { HomeTeamId = teamIds[16], AwayTeamId = teamIds[17], Week = 1, MatchDate = new DateTime(2026, 10, 13, 20, 45, 0), HomeScore = 2, AwayScore = 1, Status = "Finished", Stadium = "Marcantonio Bentegodi" },
                    new Match { HomeTeamId = teamIds[18], AwayTeamId = teamIds[19], Week = 1, MatchDate = new DateTime(2026, 10, 14, 20, 45, 0), HomeScore = 1, AwayScore = 3, Status = "Finished", Stadium = "Mapei Stadium" },

                    // 2. HAFTA
                    new Match { HomeTeamId = teamIds[1], AwayTeamId = teamIds[2], Week = 2, MatchDate = new DateTime(2026, 10, 17, 20, 45, 0), HomeScore = 1, AwayScore = 0, Status = "Finished", Stadium = "San Siro" },
                    new Match { HomeTeamId = teamIds[3], AwayTeamId = teamIds[4], Week = 2, MatchDate = new DateTime(2026, 10, 18, 18, 0, 0), HomeScore = 2, AwayScore = 2, Status = "Finished", Stadium = "Stadio Olimpico" },
                    new Match { HomeTeamId = teamIds[5], AwayTeamId = teamIds[6], Week = 2, MatchDate = new DateTime(2026, 10, 18, 20, 45, 0), HomeScore = 3, AwayScore = 1, Status = "Finished", Stadium = "Diego Armando Maradona" },
                    new Match { HomeTeamId = teamIds[7], AwayTeamId = teamIds[8], Week = 2, MatchDate = new DateTime(2026, 10, 19, 15, 0, 0), HomeScore = 0, AwayScore = 1, Status = "Finished", Stadium = "Artemio Franchi" },
                    new Match { HomeTeamId = teamIds[9], AwayTeamId = teamIds[10], Week = 2, MatchDate = new DateTime(2026, 10, 19, 18, 0, 0), HomeScore = 2, AwayScore = 0, Status = "Finished", Stadium = "Olimpico Grande Torino" },
                    new Match { HomeTeamId = teamIds[11], AwayTeamId = teamIds[12], Week = 2, MatchDate = new DateTime(2026, 10, 19, 20, 45, 0), HomeScore = 1, AwayScore = 1, Status = "Finished", Stadium = "Unipol Domus" },
                    new Match { HomeTeamId = teamIds[13], AwayTeamId = teamIds[14], Week = 2, MatchDate = new DateTime(2026, 10, 20, 15, 0, 0), HomeScore = 0, AwayScore = 2, Status = "Finished", Stadium = "Bluenergy Stadium" },
                    new Match { HomeTeamId = teamIds[15], AwayTeamId = teamIds[16], Week = 2, MatchDate = new DateTime(2026, 10, 20, 18, 0, 0), HomeScore = 2, AwayScore = 1, Status = "Finished", Stadium = "Giuseppe Sinigaglia" },
                    new Match { HomeTeamId = teamIds[17], AwayTeamId = teamIds[18], Week = 2, MatchDate = new DateTime(2026, 10, 20, 20, 45, 0), HomeScore = 1, AwayScore = 3, Status = "Finished", Stadium = "U-Power Stadium" },
                    new Match { HomeTeamId = teamIds[19], AwayTeamId = teamIds[0], Week = 2, MatchDate = new DateTime(2026, 10, 21, 20, 45, 0), HomeScore = 0, AwayScore = 2, Status = "Finished", Stadium = "Carlo Castellani" },

                    // 3. HAFTA
                    new Match { HomeTeamId = teamIds[2], AwayTeamId = teamIds[4], Week = 3, MatchDate = new DateTime(2026, 10, 24, 20, 45, 0), HomeScore = 2, AwayScore = 1, Status = "Finished", Stadium = "Allianz Stadium" },
                    new Match { HomeTeamId = teamIds[6], AwayTeamId = teamIds[8], Week = 3, MatchDate = new DateTime(2026, 10, 25, 18, 0, 0), HomeScore = 1, AwayScore = 1, Status = "Finished", Stadium = "Gewiss Stadium" },
                    new Match { HomeTeamId = teamIds[10], AwayTeamId = teamIds[12], Week = 3, MatchDate = new DateTime(2026, 10, 25, 20, 45, 0), HomeScore = 3, AwayScore = 0, Status = "Finished", Stadium = "Luigi Ferraris" },
                    new Match { HomeTeamId = teamIds[14], AwayTeamId = teamIds[16], Week = 3, MatchDate = new DateTime(2026, 10, 26, 15, 0, 0), HomeScore = 1, AwayScore = 2, Status = "Finished", Stadium = "Ennio Tardini" },
                    new Match { HomeTeamId = teamIds[18], AwayTeamId = teamIds[0], Week = 3, MatchDate = new DateTime(2026, 10, 26, 18, 0, 0), HomeScore = 0, AwayScore = 1, Status = "Finished", Stadium = "Mapei Stadium" },
                    new Match { HomeTeamId = teamIds[1], AwayTeamId = teamIds[3], Week = 3, MatchDate = new DateTime(2026, 10, 26, 20, 45, 0), HomeScore = 2, AwayScore = 2, Status = "Finished", Stadium = "San Siro" },
                    new Match { HomeTeamId = teamIds[5], AwayTeamId = teamIds[7], Week = 3, MatchDate = new DateTime(2026, 10, 27, 15, 0, 0), HomeScore = 3, AwayScore = 2, Status = "Finished", Stadium = "Diego Armando Maradona" },
                    new Match { HomeTeamId = teamIds[9], AwayTeamId = teamIds[11], Week = 3, MatchDate = new DateTime(2026, 10, 27, 18, 0, 0), HomeScore = 1, AwayScore = 0, Status = "Finished", Stadium = "Olimpico Grande Torino" },
                    new Match { HomeTeamId = teamIds[13], AwayTeamId = teamIds[15], Week = 3, MatchDate = new DateTime(2026, 10, 27, 20, 45, 0), HomeScore = 1, AwayScore = 1, Status = "Finished", Stadium = "Bluenergy Stadium" },
                    new Match { HomeTeamId = teamIds[17], AwayTeamId = teamIds[19], Week = 3, MatchDate = new DateTime(2026, 10, 28, 20, 45, 0), HomeScore = 2, AwayScore = 0, Status = "Finished", Stadium = "U-Power Stadium" }
                };

                context.Matches.AddRange(matches);
                context.SaveChanges();
            }

            // -------------------------------------------------
            // 3) 3 EK DETAYLI MAÇ
            // -------------------------------------------------

            var detailedMatches = context.Matches
                .OrderBy(x => x.Id)
                .Skip(3)
                .Take(3)
                .ToList();

            if (detailedMatches.Count == 3)
            {
                var match4 = detailedMatches[0];
                var match5 = detailedMatches[1];
                var match6 = detailedMatches[2];

                var detailedMatchIds = detailedMatches
                    .Select(x => x.Id)
                    .ToList();

                var detailAlreadyExists =
                    context.MatchGoals.Any(x => detailedMatchIds.Contains(x.MatchId)) ||
                    context.MatchCards.Any(x => detailedMatchIds.Contains(x.MatchId)) ||
                    context.Substitutions.Any(x => detailedMatchIds.Contains(x.MatchId));

                if (!detailAlreadyExists)
                {
                    // 4. maç: Atalanta - Fiorentina
                    // 5. maç: Bologna - Torino
                    // 6. maç: Genoa - Cagliari

                    var goals = new List<MatchGoal>
                    {
                        new MatchGoal
                        {
                            MatchId = match4.Id,
                            TeamId = match4.HomeTeamId,
                            PlayerName = "Ademola Lookman",
                            Minute = 18
                        },
                        new MatchGoal
                        {
                            MatchId = match4.Id,
                            TeamId = match4.HomeTeamId,
                            PlayerName = "Mateo Retegui",
                            Minute = 66
                        },
                        new MatchGoal
                        {
                            MatchId = match4.Id,
                            TeamId = match4.AwayTeamId,
                            PlayerName = "Moise Kean",
                            Minute = 74
                        },

                        new MatchGoal
                        {
                            MatchId = match5.Id,
                            TeamId = match5.HomeTeamId,
                            PlayerName = "Riccardo Orsolini",
                            Minute = 39
                        },

                        new MatchGoal
                        {
                            MatchId = match6.Id,
                            TeamId = match6.HomeTeamId,
                            PlayerName = "Andrea Pinamonti",
                            Minute = 54
                        },
                        new MatchGoal
                        {
                            MatchId = match6.Id,
                            TeamId = match6.AwayTeamId,
                            PlayerName = "Roberto Piccoli",
                            Minute = 81
                        }
                    };

                    var cards = new List<MatchCard>
                    {
                        new MatchCard
                        {
                            MatchId = match4.Id,
                            TeamId = match4.AwayTeamId,
                            PlayerName = "Rolando Mandragora",
                            Minute = 43,
                            CardType = "Yellow"
                        },

                        new MatchCard
                        {
                            MatchId = match5.Id,
                            TeamId = match5.AwayTeamId,
                            PlayerName = "Samuele Ricci",
                            Minute = 57,
                            CardType = "Yellow"
                        },

                        new MatchCard
                        {
                            MatchId = match6.Id,
                            TeamId = match6.AwayTeamId,
                            PlayerName = "Yerry Mina",
                            Minute = 72,
                            CardType = "Yellow"
                        }
                    };

                    var substitutions = new List<Substitution>
                    {
                        new Substitution
                        {
                            MatchId = match4.Id,
                            TeamId = match4.HomeTeamId,
                            PlayerIn = "Mario Pasalic",
                            PlayerOut = "Charles De Ketelaere",
                            Minute = 64
                        },

                        new Substitution
                        {
                            MatchId = match5.Id,
                            TeamId = match5.HomeTeamId,
                            PlayerIn = "Giovanni Fabbian",
                            PlayerOut = "Lewis Ferguson",
                            Minute = 70
                        },

                        new Substitution
                        {
                            MatchId = match6.Id,
                            TeamId = match6.AwayTeamId,
                            PlayerIn = "Gianluca Gaetano",
                            PlayerOut = "Zito Luvumbo",
                            Minute = 75
                        }
                    };

                    context.MatchGoals.AddRange(goals);
                    context.MatchCards.AddRange(cards);
                    context.Substitutions.AddRange(substitutions);

                    context.SaveChanges();
                }
            }
        }
    }
}