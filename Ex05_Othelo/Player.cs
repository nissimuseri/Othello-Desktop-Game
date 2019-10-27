using System.Collections.Generic;
using System.Linq;

namespace Ex05_Othelo
{
    public class Player
    {
        private readonly string r_Name;
        private readonly bool r_IsComputer;
        private List<Cell> m_ConqueredCells;

        internal List<Cell> ConqueredCells
        {
            get
            {
                return m_ConqueredCells;
            }

            set
            {
                m_ConqueredCells = value;
            }
        }

        internal Player(string i_Name, bool i_IsComputer)
        {
            m_ConqueredCells = new List<Cell>();
            r_Name = i_Name;
            r_IsComputer = i_IsComputer;
        }

        internal int GetScore()
        {
            return m_ConqueredCells.Count();
        }
    }
}
