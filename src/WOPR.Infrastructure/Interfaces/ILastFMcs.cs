namespace WOPR.Infrastructure.Interfaces
{
    public interface ILastFM
    {
        string status { get; set; }
        ITopAlbums TopAlbums { get; set; }
    }
}
