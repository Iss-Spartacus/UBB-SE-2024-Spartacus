using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Tournament
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ArenaId { get; set; } // Foreign key reference
        public Arena Arena { get; set; } // Navigation property
        public bool IsFinished { get; set; }

        // List of employees participating in the tournament
        public List<Employee> Fighters { get; set; } = new List<Employee>();

        // List of matches in the tournament
        public List<Match> Matches { get; set; } = new List<Match>();

        public Tournament(DateTime startDateTime, DateTime endDateTime, int arenaId, bool isFinished)
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ArenaId = arenaId;
            IsFinished = isFinished;
        }
    }
}
