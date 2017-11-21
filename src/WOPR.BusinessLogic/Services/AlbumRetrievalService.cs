using System.Linq;
using System.Threading.Tasks;
using WOPR.Infrastructure.Classes;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public class AlbumRetrievalService : RetrievalServiceBase
    {
        public AlbumRetrievalService(IRepository repository) : base(repository)
        {
        }

        public async Task<OperationResult> GetTopAlbumsByArtistAsync(string artist)
        {
            var results = await _repo.GetTopAlbumsAsync(artist);
            foreach (var lastFM in results)
            {
                _ds.AlbumList = lastFM.TopAlbums.Albums.ToList();
                //SetupOffline(lastFM.TopAlbums.Artist);
                return new OperationResult(true, $"Top {lastFM.TopAlbums.PerPage} albums for {lastFM.TopAlbums.Artist} have been retrieved.");
            }
            return new OperationResult(false, $"No results found for {artist}.");
        }
        //async void SetupOffline(string artist)
        //{
        //    if (File.Exists($"{ApplicationStrings.DatasetFolderPath}{artist}.xml"))
        //        return;

        //    var repo = new OnlineRepository();

        //    var results = await repo.GetConcreteTopAlbumsAsync(artist);

        //    foreach (var lastFm in results)
        //    {
        //        using (FileStream fs = File.Create($"{ApplicationStrings.DatasetFolderPath}{artist}.xml"))
        //        {
        //            var serialiser = new XmlSerializer(typeof(LastFM));
        //            serialiser.Serialize(fs, lastFm);
        //            fs.Close();
        //        }
        //    }
        //}

    }
}
