using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public static class EventAggregator
    {
        public static event Action<PlayerUpdateEventArgs> OnPlayerHPChanged;

        public static void RaisePlayerHPChanged(PlayerUpdateEventArgs args)
        {
            OnPlayerHPChanged?.Invoke(args);
        }
    }

    public class PlayerUpdateEventArgs : EventArgs
    {
        public int Player1HP { get; set; }
        public int Player2HP { get; set; }
        public int MatchId { get; set; }
    }

}
