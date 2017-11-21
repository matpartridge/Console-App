using System.Collections.Generic;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.DataLayer
{
    public sealed class DataStore
    {

        public List<IGame> GameList { get; set; }
        public List<IAlbum> AlbumList { get; set; }

        static DataStore _instance = new DataStore();
        public static DataStore Instance => _instance;

        DataStore()
        {
            AlbumList = new List<IAlbum>();
            GameList = new List<IGame>();
        }
    }
}
