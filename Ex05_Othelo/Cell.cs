using System.Drawing;

namespace Ex05_Othelo
{
    internal class Cell
    {
        internal enum eStatus
        {
            Player1,
            Player2,
            Empty
        }

        internal int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        internal int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        internal eStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        internal Cell(int i_X, int i_Y, eStatus i_Status)
        {
            m_X = i_X;
            m_Y = i_Y;
            m_Status = i_Status;
        }

        private int m_X;
        private int m_Y;
        private eStatus m_Status;
    }
}