using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WOPR.Infrastructure.Classes;
using WOPR.Infrastructure.Interfaces;
using WOPR.Models.Classes;

namespace WOPR.Repositories.Classes
{
    public class OnlineRepository : IRepository
    {
        readonly HttpClient client = new HttpClient();

        public OnlineRepository()
        {
            client.BaseAddress = new Uri(ApplicationStrings.LastFMURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationStrings.HttpHeader));
        }
        public async Task<Maybe<ILastFM>> GetTopAlbumsAsync(string artist)
        {
            var response = await GetResultsAsync($"2.0/?method=artist.gettopalbums&artist={artist}&api_key={ApplicationStrings.LastFMApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var top50 = await response.Content.ReadAsStringAsync();

                XmlSerializer xs = new XmlSerializer(typeof(LastFM), "");
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(top50));

                var results = (LastFM)xs.Deserialize(ms);

                return Maybe<ILastFM>.Some(results);
            }
            return Maybe<ILastFM>.None();
        }
        //public async Task<Maybe<LastFM>> GetConcreteTopAlbumsAsync(string artist)
        //{
        //    var response = await GetResultsAsync($"2.0/?method=artist.gettopalbums&artist={artist}&api_key={ApplicationStrings.LastFMApiKey}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var top50 = await response.Content.ReadAsStringAsync();

        //        XmlSerializer xs = new XmlSerializer(typeof(LastFM), "");
        //        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(top50));

        //        var results = (LastFM)xs.Deserialize(ms);

        //        return Maybe<LastFM>.Some(results);
        //    }
        //    return Maybe<LastFM>.None();
        //}
        async Task<HttpResponseMessage> GetResultsAsync(string path)
        {
            return await client.GetAsync(path);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~OnlineRepository() {
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
