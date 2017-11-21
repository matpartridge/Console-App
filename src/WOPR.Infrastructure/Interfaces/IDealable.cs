using System.Collections.Generic;

namespace WOPR.Infrastructure.Interfaces
{
    public interface IDealable
    {
        Stack<IAlbum> Albums { get; }
    }
}
