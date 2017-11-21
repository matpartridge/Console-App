using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WOPR.Infrastructure.Classes;

namespace WOPR.BusinessLogic.Classes
{
    public class WheelOfFortune : GamblingGameBase
    {
        public override event PropertyChangedEventHandler PropertyChanged;
        Action<string> UpdateConsole { get; }
        Dictionary<string, int> selectedCharacters;
        int playCount;
        public override string Name => ToString();

        public WheelOfFortune(Action<string> onGameStateChanged, int noOfPlays) : base(onGameStateChanged, noOfPlays)
        {
            UpdateConsole = onGameStateChanged;
            selectedCharacters = new Dictionary<string, int>();
        }
        public override void Start()
        {
            base.Start();
            UpdateConsole($"You are playing {ToString()}");
            UpdateConsole(Environment.NewLine);
            UpdateConsole($"You have 5 chances to guess the name of the selected {Albums.Peek().Artist.Name} album - when you think you know the answer enter [?]");
            UpdateConsole(Environment.NewLine);
            UpdateConsole($"Each letter you reveal is worth {Stake.ToString("c")} following this spin");
            UpdateConsole(GetRevealedLetters());
        }
        
        public override void Play(ConsoleKeyInfo key)
        {
            UpdateGame(key, SpinWheel());
            GameState = GameState.Play();
        }
        public override void End()
        {
            GameState = GameState.End();
           
            UpdateConsole($"{Environment.NewLine}The album you were looking for was {Albums.Peek().Name}.");
            selectedCharacters.Clear();
            playCount = 0;
            UpdateConsole("Do you want to play again?");
        }
        void SetUpGame()
        {
            var rnd = new Random();
            Albums.Push(_ds.AlbumList[rnd.Next(_ds.AlbumList.Count - 1)]);
        }
        void UpdateGame(ConsoleKeyInfo key, double nextStake)
        {
            if (key.KeyChar.ToString() == "?")
            {
                UpdateConsole("What is the Album name?");
                var selection = Console.ReadLine();
                if (selection.ToLower() == Albums.Peek().Name.ToLower())
                {
                    GameState = GameState.Win();
                }
                else
                {
                    GameState = GameState.Lose();
                }
            }
            else
            {
                var keyCount = Albums.Peek().Name.NumberOfCharactersInString(key, true);
                Winnings += Stake * keyCount;
                Stake = nextStake;
                playCount++;
                selectedCharacters[key.KeyChar.ToString().ToLower()] = keyCount;
                SetConsoleMessage();
            }
        }    

        void SetConsoleMessage()
        {
            UpdateConsole(Environment.NewLine);
            UpdateConsole(GetRevealedLetters());
            UpdateConsole($"Money in the bank {Winnings.ToString("c")}");
            UpdateConsole($"The value of each letter in the next round is {Stake.ToString("c")}");
        }

        string GetRevealedLetters()
        {
            var sb = new StringBuilder();
            var i = 0;
            while (++i < Albums.Peek().Name.Length + 1)
            {
                if (string.IsNullOrWhiteSpace(Albums.Peek().Name.Substring(i - 1, 1)))
                    sb.Append(" ");
                else if (selectedCharacters.ContainsKey(Albums.Peek().Name.Substring(i - 1, 1).ToLower()))
                    sb.Append(Albums.Peek().Name.Substring(i - 1, 1));
                else
                    sb.Append("_");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return "Wheel of Fortune";
        }
    }
}
