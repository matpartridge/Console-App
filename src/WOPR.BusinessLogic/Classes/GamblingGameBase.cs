using System;
using System.Collections.Generic;
using WOPR.DataLayer;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public abstract class GamblingGameBase : GameBase, IGamblingGame
    {
        protected readonly DataStore _ds = DataStore.Instance;
        public Stack<IAlbum> Albums { get; set; }
        public double Winnings { get; set; }
        public double Stake { get; set; }

        public override void Start()
        {
            Console.Clear();
            GameState = GameState.Start();
            Stake = SpinWheel();
        }
        public GamblingGameBase(Action<string> onGameStateChanged, int noOfPlays) : base(onGameStateChanged, noOfPlays)
        {
            Albums = new Stack<IAlbum>();
        }
    }
}
