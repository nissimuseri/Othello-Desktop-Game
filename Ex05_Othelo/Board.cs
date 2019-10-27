namespace Ex05_Othelo
{
    public class Board
    {
        private Cell[,] m_GameBoard;
        private int m_BoardSize;

        internal Cell[,] PlayBoard
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

        internal int BoardSize
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

        internal Board(int i_BoardSize)
        {
            PlayBoard = new Cell[i_BoardSize, i_BoardSize];
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    PlayBoard[i, j] = new Cell(i, j, Cell.eStatus.Empty);
                }
            }

            BoardSize = i_BoardSize;
        }
    }
}
