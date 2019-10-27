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
    public partial class FormSettings : Form
    {
        private static int m_BoardSize = 6;
        private static bool m_IsMultiplayer;

        internal FormSettings()
        {
            InitializeComponent();
        }

        internal static int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        internal static bool IsMultiplayer
        {
            get
            {
                return m_IsMultiplayer;
            }

            set
            {
                m_IsMultiplayer = value;
            }
        }
        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            BoardSize += 2;
            if(BoardSize > 12)
            {
                BoardSize = 6;
            }

            switch (BoardSize)
            {
                case 6:
                    buttonBoardSize.Text = "Board size: 6x6 (click to increase)";
                    break;
                case 8:
                    buttonBoardSize.Text = "Board size: 8x8 (click to increase)";
                    break;
                case 10:
                    buttonBoardSize.Text = "Board size: 10x10 (click to increase)";
                    break;
                case 12:
                    buttonBoardSize.Text = "Board size: 12x12 (click to increase)";
                    break;
            }
        }

        private void buttonAgainstComputer_Click(object sender, EventArgs e)
        {
            IsMultiplayer = false;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void buttonAgainstFriend_Click(object sender, EventArgs e)
        {
            IsMultiplayer = true;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
