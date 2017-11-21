using System;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.GameState
{
    public class InProgress : IGameState
    {
        readonly int _openingNoOfPlays;
        int _noOfPlays;
        public bool Underway => true;
        Action<string> OnGameStateChanged { get; }
        public InProgress(Action<string> onGameStateChanged, int openingNoOfPlays, int noOfPlays)
        {
            _openingNoOfPlays = openingNoOfPlays;
            _noOfPlays = noOfPlays;
            OnGameStateChanged = onGameStateChanged;
        }
        public IGameState Start() => this;
        public IGameState End() => new Inactive(OnGameStateChanged, _openingNoOfPlays);
        public IGameState NotPlaying() => new Inactive(OnGameStateChanged, _openingNoOfPlays);
        public IGameState Win() => new Won(OnGameStateChanged, _openingNoOfPlays, _noOfPlays);
        public IGameState Lose() => new Lost(OnGameStateChanged, _openingNoOfPlays, _noOfPlays);
        public IGameState Play()
        {            
            if (_noOfPlays <= 0)
                return new Lost(OnGameStateChanged, _openingNoOfPlays, _noOfPlays);
            _noOfPlays--;
            return this;
        }
    }
}
