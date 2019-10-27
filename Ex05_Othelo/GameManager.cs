using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public class GameManager
    {
        private Game m_CurrentGame = null;
        private FormGameBoard m_GameUI = null;
        private Game.eGameMode m_GameMode = Game.eGameMode.SinglePlayer;
        private int m_RedCountWins = 0;
        private int m_YellowCountWins = 0;
        private int m_TieGames = 0;

        internal Game CurrentGame
        {
            get
            {
                return m_CurrentGame;
            }

            set
            {
                m_CurrentGame = value;
            }
        }

        internal void StartGame()
        {
            FormSettings startFormSettings = new FormSettings();
            startFormSettings.ShowDialog();
            if (startFormSettings.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (FormSettings.IsMultiplayer == true)
                {
                    m_GameMode = Game.eGameMode.MultiPlayer;
                }

                m_GameUI = new FormGameBoard(FormSettings.BoardSize, this);
                CurrentGame = new Game("Red", "Yellow", FormSettings.BoardSize, m_GameMode);
                generateFirstGreenButtons();
                CurrentGame.PlaceFirstCells();
                m_GameUI.ShowDialog();
            }
        }

        private void generateFirstGreenButtons()
        {
            List<Cell> greenButtoms = CurrentGame.PotentialMoves(1);
            foreach (Cell cell in greenButtoms)
            {
                FormGameBoard.ButtonMatrix[cell.Y, cell.X].Enabled = true;
                FormGameBoard.ButtonMatrix[cell.Y, cell.X].BackColor = Color.MediumSeaGreen;
            }
        }

        internal void GameOverMessage()
        {
            if (m_CurrentGame.GameOver == true)
            {
                int winner = CurrentGame.GetWinner(CurrentGame.Player1.GetScore(), CurrentGame.Player2.GetScore());
                string winnerMessage;
                if (winner == 1)
                {
                    m_RedCountWins++;
                    winnerMessage = string.Format(
                                            "Red Won!! ({0}/{1}) ({2}/{3})", 
                                            CurrentGame.Player1.GetScore(), 
                                            CurrentGame.Player2.GetScore(), 
                                            m_RedCountWins, 
                                            m_RedCountWins + m_YellowCountWins + m_TieGames);
                }
                else if (winner == 2)
                {
                    m_YellowCountWins++;
                    winnerMessage = string.Format(
                                            "Yellow Won!! ({0}/{1}) ({2}/{3})", 
                                            CurrentGame.Player2.GetScore(), 
                                            CurrentGame.Player1.GetScore(), 
                                            m_YellowCountWins, 
                                            m_RedCountWins + m_YellowCountWins + m_TieGames);
                }
                else
                {
                    winnerMessage = "It's a Tie!";
                    m_TieGames++;
                }

                string endOfGameAnnounce = string.Format(
                    @"{0}
Whould you like another round?", 
winnerMessage, 
CurrentGame.Player1.GetScore(), 
CurrentGame.Player2.GetScore());
                DialogResult dialogResult = MessageBox.Show(endOfGameAnnounce, "Othello", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    restartGame();
                }
                else
                {
                    m_GameUI.Close();
                }
            }
        }

        private void restartGame()
        {
            CurrentGame = new Game("Red", "Yellow", FormSettings.BoardSize, m_GameMode);
            m_GameUI.ResetGameBoard();
            CurrentGame.PlaceFirstCells();
            generateFirstGreenButtons();
        }
    }
}
