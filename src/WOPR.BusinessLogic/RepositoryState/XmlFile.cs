using WOPR.Infrastructure.Interfaces;
using WOPR.Repositories.Classes;

namespace WOPR.BusinessLogic.RepositoryState
{
    public class XmlFile : IRepositoryState
    {
        readonly IRepository _repo;
        public IRepository Repo => _repo;

        public IRepositoryState Offline() => this;

        public IRepositoryState Online() => new WebService();
        public IRepositoryState NotSet() => new Unknown();
        public XmlFile()
        {
            _repo = new LocalRepository();
        }
        public override string ToString()
        {
            return "You are playing offline";
        }
    }
}
