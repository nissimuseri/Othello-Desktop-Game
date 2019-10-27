using System;

namespace Ex05_Othelo
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            GameManager newGame = new GameManager();
            newGame.StartGame();
        }
    }
}
