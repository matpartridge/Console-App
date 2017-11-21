using System;
using System.Collections.Generic;
using System.ComponentModel;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public class PlayYourPlayCountRight : GamblingGameBase
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        readonly int _albumsToSelect;
        Action<string> UpdateConsole { get; }
        Stack<IAlbum> _revealedAlbums;
        public PlayYourPlayCountRight(Action<string> onGameStateChanged, int noOfPlays) : base(onGameStateChanged, noOfPlays)
        {
            UpdateConsole = onGameStateChanged;
            _revealedAlbums = new Stack<IAlbum>();
            _albumsToSelect = noOfPlays + 1;
        }
        public override string Name => ToString();
        public override void Start() 
        {
            base.Start();
            SetUpGame();
            UpdateConsole($"You are playing {ToString()}");
            UpdateConsole(Environment.NewLine);
            UpdateConsole($"You need to decide if the play count of the current album is higher or lower than the previous album");
            UpdateConsole(Environment.NewLine);
            GetNextAlbum();
        }
     
        public override void Play(ConsoleKeyInfo key)
        {
            if (key.KeyChar.ToString().ToLower() != "h" && key.KeyChar.ToString().ToLower() != "l")
                return;
            UpdateConsole(Environment.NewLine);
            UpdateGame(key, SpinWheel());
            if (Albums.Count <= 1)
                End();
            else
                GameState = GameState.Play();
        }
        public override void End()
        {
            GameState = GameState.End();

            UpdateConsole($"{Environment.NewLine}Your winnings are {Winnings.ToString("c")}.");
            Albums.Clear();
            _revealedAlbums.Clear();
            UpdateConsole("Do you want to play again?");
        }

        void SetUpGame()
        {
            var i = 0;
            var rnd = new Random();
            while (i++ < _albumsToSelect)
            {
                Albums.Push(_ds.AlbumList[rnd.Next(_ds.AlbumList.Count - 1)]);
            }
        }
        void UpdateGame(ConsoleKeyInfo key, double nextStake)
        {
            var result = WinningGuess(key);
            if (result == true)
            {
                Winnings += Stake;
                UpdateConsole($"Correct, {Winnings.ToString("c")} in bank");
            }
            else if (result == false)
            {
                Winnings = 0;
                UpdateConsole("Incorrect, all winnings lost");
            }
            else
            {
                UpdateConsole("Nothing in this game for two in a bed");
            }
            Stake = nextStake;

            GetNextAlbum();
        }
        bool WinningGuess(ConsoleKeyInfo key)
        {
            if (key.KeyChar.ToString().ToLower() == "h")
                return _revealedAlbums.Peek().PlayCount < Albums.Peek().PlayCount;
            else 
                return _revealedAlbums.Peek().PlayCount > Albums.Peek().PlayCount;
        }
        void GetNextAlbum()
        {
            if (Albums.Count <= 1)
                return;
            _revealedAlbums.Push(Albums.Pop());
            UpdateConsole($"{_revealedAlbums.Peek().Name} - play count: {_revealedAlbums.Peek().PlayCount}");
            UpdateConsole($"Is the play count of {Albums.Peek().Name} higher [H] or lower[L]");
            UpdateConsole($"A correct answer adds {Stake.ToString("c")} to the bank");            
        }
        void EndGame()
        {
            _revealedAlbums.Clear();
        }

        public override string ToString()
        {
            return "Play Your PlayCount's Right";
        }
    }
}
