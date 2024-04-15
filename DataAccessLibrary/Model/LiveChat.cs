using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class LiveChat
    {
        public int Id { get; set; }
        public int MatchId { get; set; } // Foreign key reference
        public Matches Match { get; set; } // Navigation property
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        public LiveChat(int id, int matchId, string content, DateTime timeStamp)
        {
            Id = id;
            MatchId = matchId;
            Content = content;
            TimeStamp = timeStamp;
        }
    }
}
