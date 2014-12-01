using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.IEqualPlayerWinRules m_winRule;

        private IObserver iobserver;
        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.EqualPlayerWin();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                NewCard(this);

                return true;
            }
            return false;
        }

        public bool IsDealerWinner(Player a_player)
        {
            if (!m_winRule.EqualPlayerWinRules(this, a_player))
                return true;
            return false;
        }

        internal bool Stand() {
            
            if (m_deck != null) {
                ShowHand();

                while (m_hitRule.DoHit(this)) {
                    NewCard(this);
                }
            }
            return false;
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
        public void NewCard(Player a_player, bool show = true) {
            Card c = m_deck.GetCard();
            c.Show(show);
            a_player.DealCard(c);

            //iobserver.update();
        }

        public void RegisterIOBserver(IObserver iObserver) {
            this.iobserver = iObserver;
        }
    }
}
