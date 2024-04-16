using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public static class MatchState
    {
        private static Dictionary<int, (int Player1HP, int Player2HP)> matchHP = new Dictionary<int, (int, int)>();

        public static void SetHP(int matchId, int player1HP, int player2HP)
        {
            matchHP[matchId] = (player1HP, player2HP);
        }

        public static (int Player1HP, int Player2HP) GetHP(int matchId)
        {
            if (matchHP.ContainsKey(matchId))
            {
                return matchHP[matchId];
            }
            return (100, 100); // default HP values if not set
        }
    }

}
