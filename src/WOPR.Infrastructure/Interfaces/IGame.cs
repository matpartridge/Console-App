using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Infrastructure.Interfaces
{
    public interface IGame : INotifyPropertyChanged
    {
        string Name { get; }
        void Start();
        void End();
        void Play(ConsoleKeyInfo key);
        IGameState GameState { get; set; }
    }
}
