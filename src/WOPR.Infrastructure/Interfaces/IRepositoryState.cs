namespace WOPR.Infrastructure.Interfaces
{
    public interface IRepositoryState
    {
        IRepository Repo { get; }
        IRepositoryState Online();
        IRepositoryState Offline();
        IRepositoryState NotSet();
    }
}
