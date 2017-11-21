namespace WOPR.Infrastructure.Interfaces
{
    public interface IAlbum
    {
        IArtist Artist { get; set; }
        string Name { get; set; }
        int PlayCount { get; set; }
    }
}