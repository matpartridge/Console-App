using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.GameState
{
    public class Lost : IGameState
    {
        readonly int _openingNoOfPlays;
        int _noOfPlays;
        public bool Underway => false;
        Action<string> OnGameStateChanged { get; }
        public Lost(Action<string> onGameStateChanged, int openingNoOfPlays, int noOfPlays)
        {
            _openingNoOfPlays = openingNoOfPlays;
            _noOfPlays = noOfPlays;
            OnGameStateChanged = onGameStateChanged;
            OnGameStateChanged("You have lost");
        }
        public IGameState Start() => new InProgress(OnGameStateChanged, _openingNoOfPlays, _noOfPlays);
        public IGameState End() => new Inactive(OnGameStateChanged, _openingNoOfPlays);
        public IGameState NotPlaying() => new Inactive(OnGameStateChanged, _openingNoOfPlays);
        public IGameState Win() => this;
        public IGameState Lose() => this;
        public IGameState Play() => this;
    }
}
