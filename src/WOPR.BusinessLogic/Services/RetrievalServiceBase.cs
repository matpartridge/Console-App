using System;
using WOPR.DataLayer;
using WOPR.Infrastructure.Interfaces;

namespace WOPR.BusinessLogic.Classes
{
    public abstract class RetrievalServiceBase : IDisposable
    {
        protected readonly DataStore _ds;
        protected readonly IRepository _repo;
        public RetrievalServiceBase(IRepository repository)
        {
            _repo = repository;
            _ds = DataStore.Instance;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _repo.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
