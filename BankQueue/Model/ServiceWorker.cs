using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankQueue.Model
{
    public abstract class ServiceWorker
    {
        public enum ServiceConnectionStatus
        {
            NaN,
            Up,
            Down
        }

        private readonly object _syncRoot = new object();
        private readonly Timer _timer;

        public int Count { get; private set; }
        public List<Exception> Exceptions { get; private set; }
        public string AddressUri { get; private set; }
        private ServiceConnectionStatus _status;

        public event EventHandler<ServiceConnectionStatus> StatusChanged = delegate { };

        public ServiceWorker(string addressUri)
        {
            _status = ServiceConnectionStatus.NaN;
            AddressUri = addressUri;
            _timer = new Timer(PingTimerCallback, null, 0, 500);
        }

        private void PingTimerCallback(object state)
        {
            if (!Monitor.TryEnter(_syncRoot)) return;

            try
            {
                Monitor.Enter(_syncRoot);
                Ping();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ServiceDemon timer error.", ex);
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        public virtual void DoWork()
        {
            Count++;
        }

        protected abstract void Ping();

        protected void HandleErrors(Exception ex)
        {
            if (Exceptions == null)
            {
                Exceptions = new List<Exception>();
            }

            Exceptions.Add(ex);
        }

        protected void ChangeStatus(ServiceConnectionStatus status)
        {
            if (status != _status)
            {
                _status = status;
                StatusChanged(this, status);
            }
        }
    }
}
