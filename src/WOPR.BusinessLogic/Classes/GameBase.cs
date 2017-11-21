using System;
using System.ComponentModel;
using WOPR.BusinessLogic.GameState;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public abstract class GameBase : IGame, IDisposable
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public IGameState GameState { get; set; }
        public abstract string Name { get; }
        public abstract void End();
        public abstract void Start();
        public abstract void Play(ConsoleKeyInfo keyPressed);
        public GameBase(Action<string> onGameStateChanged, int noOfPlays)
        {
            GameState = new Inactive(onGameStateChanged, noOfPlays);
        }
        protected double SpinWheel()
        {
            var rnd = new Random();
            return rnd.Next(10) * 100;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
