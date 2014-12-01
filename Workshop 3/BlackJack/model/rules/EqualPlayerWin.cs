using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules {
    class EqualPlayerWin : IEqualPlayerWinRules {
        private const int g_maxScore = 21;
        public bool EqualPlayerWinRules(model.Player a_dealer, model.Player a_player) {
            if (a_dealer.CalcScore() > g_maxScore && a_player.CalcScore() <= g_maxScore)
                return true;
            if (a_dealer.CalcScore() < g_maxScore && a_player.CalcScore() >= g_maxScore)
                return false;
            if (a_dealer.CalcScore() == a_player.CalcScore())
                return true;
            if (a_dealer.CalcScore() > g_maxScore && a_player.CalcScore() > g_maxScore && a_player.CalcScore() < a_dealer.CalcScore())
                return true;
            if (a_dealer.CalcScore() <= g_maxScore && a_player.CalcScore() <= g_maxScore && a_player.CalcScore() > a_dealer.CalcScore())
                return true;            
            return false;
        }
    }
}
