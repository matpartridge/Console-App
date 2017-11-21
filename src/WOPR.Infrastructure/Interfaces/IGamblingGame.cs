using System;
using System.Collections.Generic;

namespace WOPR.Infrastructure.Interfaces
{
    public interface IGamblingGame : IGame
    {
        Stack<IAlbum> Albums { get; set; }
        double Winnings { get; set; }
        double Stake { get; set; }        
    }
}
