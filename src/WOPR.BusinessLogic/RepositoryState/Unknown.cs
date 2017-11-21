using WOPR.Infrastructure.Interfaces;
using WOPR.Repositories.Classes;

namespace WOPR.BusinessLogic.RepositoryState
{
    public class Unknown : IRepositoryState
    {
        readonly IRepository _repo;
        public IRepository Repo => _repo;

        public IRepositoryState Offline() => new XmlFile();

        public IRepositoryState Online() => new WebService();

        public IRepositoryState NotSet() => this;
        public Unknown()
        {
            _repo = new UnknownRepository();
        }
        public override string ToString()
        {
            return "You have not set a playing environment";
        }
    }
}
