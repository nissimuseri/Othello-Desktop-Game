using System;
using System.Collections.Generic;
using System.Drawing;

namespace Ex05_Othelo
{
    public class Game
    {
        internal enum eGameMode
        {
            SinglePlayer = 1,
            MultiPlayer
        }

        private Player m_Player1, m_Player2;
        private Board m_GameBoard;
        private bool m_IsPlayer1;
        private bool m_GameOver;

        private readonly eGameMode m_GameMode;

        internal bool IsPlayer1Turn
        {
            get
            {
                return m_IsPlayer1;
            }

            set
            {
                m_IsPlayer1 = value;
            }
        }

        internal bool GameOver
        {
            get
            {
                return m_GameOver;
            }

            set
            {
                m_GameOver = value;
            }
        }

        internal Board GameBoard
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }

        internal Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        internal Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        internal eGameMode GameMode
        {
            get
            {
                return m_GameMode;
            }
        }

        internal Game(string i_Player1Name, string i_Player2Name, int i_BoardSize, eGameMode i_GameMode)
        {
            m_Player1 = new Player(i_Player1Name, false);
            m_Player2 = new Player(i_Player2Name, i_GameMode == eGameMode.SinglePlayer ? true : false);
            m_GameBoard = new Board(i_BoardSize);
            m_IsPlayer1 = true;
            m_GameOver = false;
            m_GameMode = i_GameMode;
            PlaceFirstCells();
        }

        internal void PlaceFirstCells()
        {
            changeCell((GameBoard.BoardSize / 2) - 1, (GameBoard.BoardSize / 2) - 1, Cell.eStatus.Player1);
            changeCell(GameBoard.BoardSize / 2, GameBoard.BoardSize / 2, Cell.eStatus.Player1);
            changeCell(GameBoard.BoardSize / 2, (GameBoard.BoardSize / 2) - 1, Cell.eStatus.Player2);
            changeCell((GameBoard.BoardSize / 2) - 1, GameBoard.BoardSize / 2, Cell.eStatus.Player2);
        }

        internal void PlayTurn(int i_Y, int i_X, bool isPlayer1Turn)
        {
            if (GameOver != true)
            {
                if (GameBoard.PlayBoard[i_X, i_Y].Status == Cell.eStatus.Empty)
                {
                    this.changeCell(i_Y, i_X, isPlayer1Turn ? Cell.eStatus.Player1 : Cell.eStatus.Player2);
                    flipCells(i_X, i_Y, isPlayer1Turn ? 1 : 2);
                }
            }

            changePlayer();
            checkGameOver();
        }

        private void flipCells(int i_X, int i_Y, int i_PlayerStatus)
        {
            bool overFlowFlag = false;
            int offsetX = 0;
            int offsetY = 0;
            for (int i = Math.Max(0, i_X - 1); i <= i_X + 1 && i < GameBoard.BoardSize; i++)
            {
                for (int j = Math.Max(0, i_Y - 1); j <= i_Y + 1 && j < GameBoard.BoardSize; j++)
                {
                    overFlowFlag = false;
                    if (GameBoard.PlayBoard[i, j].Status == (IsPlayer1Turn ? Cell.eStatus.Player2 : Cell.eStatus.Player1))
                    {
                        offsetX = i - i_X;
                        offsetY = j - i_Y;
                        int tempX = i_X + offsetX;
                        int tempY = i_Y + offsetY;
                        checkDirection(i_PlayerStatus, ref overFlowFlag, ref tempX, ref tempY, offsetX, offsetY);
                        if (overFlowFlag != true && GameBoard.PlayBoard[tempX, tempY].Status != Cell.eStatus.Empty)
                        {
                            offsetX = i - i_X;
                            offsetY = j - i_Y;
                            tempX = i_X + offsetX;
                            tempY = i_Y + offsetY;
                            flipDirection(i_PlayerStatus, ref tempX, ref tempY, offsetX, offsetY);
                        }
                    }
                }
            }
        }

        private void flipDirection(int i_PlayerStatus, ref int io_DirectionX, ref int io_DirectionY, int i_OffsetX, int i_OffsetY)
        {
            while (GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status != Cell.eStatus.Empty)
            {
                if (i_PlayerStatus == 1)
                {
                    if (GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status == Cell.eStatus.Player2)
                    {
                        changeCell(io_DirectionY, io_DirectionX, Cell.eStatus.Player1);
                    }
                    else
                    {
                        break;
                    }
                }
                else if (i_PlayerStatus == 2)
                {
                    if (GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status == Cell.eStatus.Player1)
                    {
                        changeCell(io_DirectionY, io_DirectionX, Cell.eStatus.Player2);
                    }
                    else
                    {
                        break;
                    }
                }

                io_DirectionX += i_OffsetX;
                io_DirectionY += i_OffsetY;
                if (io_DirectionX < 0 || io_DirectionX > GameBoard.BoardSize - 1 || io_DirectionY < 0 || io_DirectionY > GameBoard.BoardSize - 1)
                {
                    break;
                }
            }
        }

        private void checkDirection(int i_PlayerStatus, ref bool io_OverFlowFlag, ref int io_DirectionX, ref int io_DirectionY, int i_OffsetX, int i_OffsetY)
        {
            while (io_OverFlowFlag != true && GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status != Cell.eStatus.Empty)
            {
                if (i_PlayerStatus == 1)
                {
                    if (GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status == Cell.eStatus.Player1)
                    {
                        break;
                    }
                }
                else if (i_PlayerStatus == 2)
                {
                    if (GameBoard.PlayBoard[io_DirectionX, io_DirectionY].Status == Cell.eStatus.Player2)
                    {
                        break;
                    }
                }

                io_DirectionX += i_OffsetX;
                io_DirectionY += i_OffsetY;
                if (io_DirectionX < 0 || io_DirectionX > GameBoard.BoardSize - 1 || io_DirectionY < 0 || io_DirectionY > GameBoard.BoardSize - 1)
                {
                    io_OverFlowFlag = true;
                }
            }
        }

        internal void PlayComputerTurn()
        {
            List<Cell> possibleMoves = PotentialMoves(2);
            if (possibleMoves.Count != 0)
            {
                int randTurn = new Random().Next(possibleMoves.Count);
                Cell NextTurn = possibleMoves[randTurn];

                PlayTurn(NextTurn.Y, NextTurn.X, false);
            }
        }

        internal List<Cell> PotentialMoves(int i_PlayerToCheck)
        {
            List<Cell> PotentialMoves = new List<Cell>();
            for (int i = 0; i < GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < GameBoard.BoardSize; j++)
                {
                    if (isValidOption(GameBoard.PlayBoard[i, j], i_PlayerToCheck) == true)
                    {
                        PotentialMoves.Add(GameBoard.PlayBoard[i, j]);
                    }
                }
            }

            return PotentialMoves;
        }

        private bool isValidOption(Cell i_Cell, int i_PlayerToCheck)
        {
            int offsetX = 0;
            int offsetY = 0;
            bool isPotential = false;
            if (i_Cell.Status == Cell.eStatus.Empty)
            {
                for (int i = Math.Max(0, i_Cell.X - 1); i <= i_Cell.X + 1 && i < GameBoard.BoardSize; i++)
                {
                    for (int j = Math.Max(0, i_Cell.Y - 1); j <= i_Cell.Y + 1 && j < GameBoard.BoardSize; j++)
                    {
                        if (GameBoard.PlayBoard[i, j].Status == (IsPlayer1Turn ? Cell.eStatus.Player2 : Cell.eStatus.Player1))
                        {
                            offsetX = i - i_Cell.X;
                            offsetY = j - i_Cell.Y;
                            int tempX = i_Cell.X + offsetX;
                            int tempY = i_Cell.Y + offsetY;
                            while (GameBoard.PlayBoard[tempX, tempY].Status != Cell.eStatus.Empty)
                            {
                                if (i_PlayerToCheck == 1)
                                {
                                    if (GameBoard.PlayBoard[tempX, tempY].Status == Cell.eStatus.Player1)
                                    {
                                        isPotential = true;
                                        break;
                                    }
                                }
                                else if (i_PlayerToCheck == 2)
                                {
                                    if (GameBoard.PlayBoard[tempX, tempY].Status == Cell.eStatus.Player2)
                                    {
                                        isPotential = true;
                                        break;
                                    }
                                }

                                tempX += offsetX;
                                tempY += offsetY;
                                if (tempX < 0 || tempX > GameBoard.BoardSize - 1 || tempY < 0 || tempY > GameBoard.BoardSize - 1)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return isPotential;
        }

        private void changeCell(int i_Y, int i_X, Cell.eStatus i_NewStatus)
        {
            m_GameBoard.PlayBoard[i_X, i_Y].Status = i_NewStatus;
            Cell temp = new Cell(i_X, i_Y, i_NewStatus);
            if (i_NewStatus == Cell.eStatus.Player1)
            {
                Player1.ConqueredCells.Add(temp);
                Player2.ConqueredCells.RemoveAll(cell => cell.X == i_X && cell.Y == i_Y);
                FormGameBoard.ButtonMatrix[i_X, i_Y].BackgroundImage = (Image)new Bitmap(ResourceIcon.CoinRed, new Size(35, 35));
            }

            if (i_NewStatus == Cell.eStatus.Player2)
            {
                Player2.ConqueredCells.Add(temp);
                Player1.ConqueredCells.RemoveAll(cell => cell.X == i_X && cell.Y == i_Y);
                FormGameBoard.ButtonMatrix[i_X, i_Y].BackgroundImage = (Image)new Bitmap(ResourceIcon.CoinYellow, new Size(35, 35));
            }

            FormGameBoard.ButtonMatrix[i_X, i_Y].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            FormGameBoard.ButtonMatrix[i_X, i_Y].Enabled = false;
        }

        private void checkGameOver()
        {
            if (this.PotentialMoves(this.IsPlayer1Turn ? 1 : 2).Count == 0)
            {
                changePlayer();
                if (this.PotentialMoves(this.IsPlayer1Turn ? 1 : 2).Count == 0)
                {
                    this.GameOver = true;
                }
                else if (IsPlayer1Turn != true && m_GameMode == eGameMode.SinglePlayer)
                {
                    PlayComputerTurn();
                }
            }
        }

        internal int GetWinner(int i_ScorePlayer1, int i_ScorePlayer2)
        {
            int winner;
            if (i_ScorePlayer1 > i_ScorePlayer2)
            {
                winner = 1;
            }
            else if (i_ScorePlayer1 < i_ScorePlayer2)
            {
                winner = 2;
            }
            else
            {
                winner = 0;
            }

            return winner;
        }

        private void changePlayer()
        {
            IsPlayer1Turn = !IsPlayer1Turn;
        }
    }
}