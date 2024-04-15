using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Match
    {
        public int Id { get; set; }
        public int TournamentId { get; set; } // Foreign key reference
        public Tournament Tournament { get; set; } // Navigation property
        public int Employee1Id { get; set; } // Foreign key reference
        public Employee Employee1 { get; set; } // Navigation property
        public int Employee2Id { get; set; } // Foreign key reference
        public Employee Employee2 { get; set; } // Navigation property
        public DateTime RegistrationDate { get; set; }
        public int WinnerId { get; set; } // Foreign key reference
        public Employee Winner { get; set; } // Navigation property
        public List<MatchObserver> Observers { get; set; } = new List<MatchObserver>();

        public Match(int id, int tournamentId, int employee1Id, int employee2Id, DateTime registrationDate, int winnerId)
        {
            Id = id;
            TournamentId = tournamentId;
            Employee1Id = employee1Id;
            Employee2Id = employee2Id;
            RegistrationDate = registrationDate;
            WinnerId = winnerId;
        }
    }
}
