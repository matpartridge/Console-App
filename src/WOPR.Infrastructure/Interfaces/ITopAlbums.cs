using System.Collections.Generic;

namespace WOPR.Infrastructure.Interfaces
{
    public interface ITopAlbums
    {
        IEnumerable<IAlbum> Albums { get; }
        string Artist { get; set; }
        string Page { get; set; }
        string PerPage { get; set; }
        string Total { get; set; }
        string TotalPages { get; set; }
    }
}