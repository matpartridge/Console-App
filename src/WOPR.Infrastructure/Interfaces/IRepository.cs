using System;
using System.Threading.Tasks;
using WOPR.Infrastructure.Classes;

namespace WOPR.Infrastructure.Interfaces
{
    public interface IRepository : IDisposable
    {

        Task<Maybe<ILastFM>> GetTopAlbumsAsync(string artist);

    }
}
