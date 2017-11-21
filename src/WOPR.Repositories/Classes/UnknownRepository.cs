using System.Threading.Tasks;
using WOPR.Infrastructure.Classes;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.Repositories.Classes
{
    public class UnknownRepository : IRepository
    {
        public void Dispose()
        {

        }

        public async Task<Maybe<ILastFM>> GetTopAlbumsAsync(string artist)
        {
            return Maybe<ILastFM>.None();
        }
    }
}
