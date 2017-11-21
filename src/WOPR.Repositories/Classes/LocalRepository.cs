using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WOPR.Infrastructure.Classes;
using WOPR.Infrastructure.Interfaces;
using WOPR.Models.Classes;

namespace WOPR.Repositories.Classes
{
    public class LocalRepository : IRepository
    {
        public async Task<Maybe<ILastFM>> GetTopAlbumsAsync(string artist)
        {
            var albums = await Task.Run(() =>
            {
                var fileName = $"{ApplicationStrings.DatasetFolderPath}{artist}.xml";
                LastFM results;
                XmlSerializer xs = new XmlSerializer(typeof(LastFM), "");

                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    var reader = XmlReader.Create(fs);
                    results = (LastFM)xs.Deserialize(reader);
                    fs.Close();
                }

                return Maybe<ILastFM>.Some(results);
            });
            return albums;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LocalRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
