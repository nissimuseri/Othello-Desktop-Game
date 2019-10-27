using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public class FormGameBoard : Form
    {
        private const int k_ButtonSize = 40;
        private const int k_ButtonSpacing = 5;
        private static GameButton[,] s_ButtonMatrix;
        private readonly int r_BoardDimension;
        private GameManager m_GameManager;

        internal static GameButton[,] ButtonMatrix
        {
            get
            {
                return s_ButtonMatrix;
            }

            set
            {
                s_ButtonMatrix = value;
            }
        }

        internal FormGameBoard(int i_BoardDimension, GameManager i_GameManager)
        {
            m_GameManager = i_GameManager;
            r_BoardDimension = i_BoardDimension;
            initializeButtonMatrix();
            int formSize = ((k_ButtonSize + k_ButtonSpacing) * FormSettings.BoardSize) + k_ButtonSpacing + 10;
            this.ClientSize = new Size(formSize, formSize);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Text = "Othello - Red's Turn";
        }

        private void initializeButtonMatrix()
        {
            this.Height = ((FormSettings.BoardSize + 1) * k_ButtonSize) + ((FormSettings.BoardSize - 1) * k_ButtonSpacing);
            this.Width = ((FormSettings.BoardSize + 1) * k_ButtonSize) + ((FormSettings.BoardSize - 1) * k_ButtonSpacing);
            ButtonMatrix = new GameButton[FormSettings.BoardSize, FormSettings.BoardSize];

            for (int x = 0; x < r_BoardDimension; x++)
            {
                for (int y = 0; y < r_BoardDimension; y++)
                {
                    s_ButtonMatrix[x, y] = new GameButton();
                    s_ButtonMatrix[x, y].X = x;
                    s_ButtonMatrix[x, y].Y = y;
                    s_ButtonMatrix[x, y].Width = s_ButtonMatrix[x, y].Height = k_ButtonSize;
                    s_ButtonMatrix[x, y].Location = new System.Drawing.Point((x * (k_ButtonSize + k_ButtonSpacing)) + k_ButtonSpacing + 5, (y * (k_ButtonSpacing + k_ButtonSize)) + k_ButtonSpacing + 5);
                    s_ButtonMatrix[x, y].Click += m_ButtonMatrix_Click;
                    s_ButtonMatrix[x, y].TabIndex = ((x + 1) * r_BoardDimension) + (y + 1);
                    s_ButtonMatrix[x, y].Enabled = false;
                    s_ButtonMatrix[x, y].BackColor = Color.Empty;
                    this.Controls.Add(s_ButtonMatrix[x, y]);
                }
            }
        }

        private void updateButtonMatrix()
        {
            for (int x = 0; x < r_BoardDimension; x++)
            {
                for (int y = 0; y < r_BoardDimension; y++)
                {
                    if (s_ButtonMatrix[x, y].BackColor == Color.MediumSeaGreen)
                    {
                        s_ButtonMatrix[x, y].Enabled = false;
                        s_ButtonMatrix[x, y].BackColor = Color.Empty;
                    }
                }
            }
        }

        private void showValidMoves()
        {
            List<Cell> greenButtoms = m_GameManager.CurrentGame.PotentialMoves(m_GameManager.CurrentGame.IsPlayer1Turn ? 1 : 2);
            foreach (Cell cell in greenButtoms) 
            {
                s_ButtonMatrix[cell.X, cell.Y].Enabled = true;
                s_ButtonMatrix[cell.X, cell.Y].BackColor = Color.MediumSeaGreen;
            }
        }

        private void updateTitle()
        {
            if(m_GameManager.CurrentGame.IsPlayer1Turn == true)
            {
                this.Text = "Othello - Red's Turn";
            }
            else
            {
                this.Text = "Othello - Yellow's Turn";
            }
        }

        internal void ResetGameBoard()
        {
            updateTitle();
            for (int x = 0; x < r_BoardDimension; x++)
            {
                for (int y = 0; y < r_BoardDimension; y++)
                {
                    s_ButtonMatrix[x, y].Enabled = false;
                    s_ButtonMatrix[x, y].BackColor = Color.Empty;
                    s_ButtonMatrix[x, y].BackgroundImage = null;
                }
            }
        }

        private void m_ButtonMatrix_Click(object sender, EventArgs e)
        {
            GameButton button = sender as GameButton;
            m_GameManager.CurrentGame.PlayTurn(button.Y, button.X, m_GameManager.CurrentGame.IsPlayer1Turn);
            updateButtonMatrix();
            if(m_GameManager.CurrentGame.GameMode == Game.eGameMode.SinglePlayer)
            {
                    m_GameManager.CurrentGame.PlayComputerTurn();
            }

            showValidMoves();
            updateTitle();
            m_GameManager.GameOverMessage();
        }
    }
}
