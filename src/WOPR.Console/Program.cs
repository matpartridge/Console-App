using System;
using WOPR.BusinessLogic.Classes;

namespace WOPR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "WOPR";
            GameController.Instance.SelectDataSource();

            while (GameController.Instance.PlayAgain(Console.ReadKey()) != false)
            {
                ConsoleKeyInfo? selectedGame = null;    
                while (selectedGame == null)
                {
                    selectedGame = GameController.Instance.SelectGame();
                }

                GameController.Instance.StartGame((ConsoleKeyInfo)selectedGame);        

                var inPlay = true;
                while (inPlay)
                {
                    inPlay = GameController.Instance.Play(Console.ReadKey());
                }
            }
            
            Console.ReadKey();
        }
    }
}
