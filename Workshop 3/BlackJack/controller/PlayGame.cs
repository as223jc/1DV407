using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack.controller
{
    class PlayGame : model.IObserver {
        private model.Game m_game;
        private view.IView m_view;
        public bool Play(model.Game a_game, view.IView a_view)
        {
            m_game = a_game;
            m_view = a_view;
            m_game.RegisterNewObserver(this);

            update();
            
            if (a_game.IsGameOver())
            {
                m_view.DisplayGameOver(a_game.IsDealerWinner());
            }

            int input = m_view.GetInput();

            if (input == view.SwedishView.NewGame_c) 
                a_game.NewGame();
             else if (input == view.SwedishView.Hit_c) 
                a_game.Hit();
             else if (input == view.SwedishView.Stand_c) 
                a_game.Stand();
            
            return input != view.SwedishView.Quit_c;
        }

        public void update() {
            Thread.Sleep(1200);
            m_view.DisplayWelcomeMessage();

            m_view.DisplayDealerHand(this.m_game.GetDealerHand(), this.m_game.GetDealerScore());
            m_view.DisplayPlayerHand(this.m_game.GetPlayerHand(), this.m_game.GetPlayerScore());
           
        }
    }
}
