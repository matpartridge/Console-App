using WOPR.Infrastructure.Interfaces;
using WOPR.Repositories.Classes;

namespace WOPR.BusinessLogic.RepositoryState
{
    public class WebService : IRepositoryState
    {
        readonly IRepository _repo;
        public IRepository Repo => _repo;

        public IRepositoryState Offline() => new XmlFile();

        public IRepositoryState Online() => this;

        public IRepositoryState NotSet() => new Unknown();
        public WebService()
        {
            _repo = new OnlineRepository();
        }
        public override string ToString()
        {
            return "You are playing online";
        }
    }
}
