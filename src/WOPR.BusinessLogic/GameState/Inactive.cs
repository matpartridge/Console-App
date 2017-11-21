using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.GameState
{
    public class Inactive : IGameState
    {
        readonly int _openingNoOfPlays;
        int _noOfPlays;
        public bool Underway => false;

        Action<string> OnGameStateChanged { get; }
        public Inactive(Action<string> onGameStateChanged, int openingNoOfPlays)
        {
            _openingNoOfPlays = openingNoOfPlays;
            _noOfPlays = openingNoOfPlays;
            OnGameStateChanged = onGameStateChanged;
        }
        public IGameState Start() =>  new InProgress(OnGameStateChanged, _openingNoOfPlays, _noOfPlays);

        public IGameState End() => this;
        
        public IGameState Play() => this;

        public IGameState NotPlaying() => this;

        public IGameState Win() => this;

        public IGameState Lose() => this;
    }
}
