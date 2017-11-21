using System;

namespace WOPR.Infrastructure.Interfaces
{
    public interface IGameState
    {
        bool Underway { get; }
        IGameState NotPlaying();
        IGameState Start();
        IGameState Play();
        IGameState Win();
        IGameState Lose();
        IGameState End();
    }
}
