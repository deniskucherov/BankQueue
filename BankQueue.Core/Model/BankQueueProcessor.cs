using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace BankQueue.Core.Model
{
    public sealed class BankQueueProcessor : IQueueProcessor
    {
        private readonly object _syncRoot = new object();
        private readonly Dictionary<QueueType, BankQueueCommon> _workingQueues;
        private int _totalCustomersCount;
        private int _currentCustomersCount;

        public BankQueueProcessor(QueueFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            _workingQueues = new Dictionary<QueueType, BankQueueCommon>(3);
            _workingQueues.Add(QueueType.Cashire, factory.CreateQueue(QueueType.Cashire));
            _workingQueues.Add(QueueType.Credit, factory.CreateQueue(QueueType.Credit));
            _workingQueues.Add(QueueType.Operational, factory.CreateQueue(QueueType.Operational));
        }

        public int QueueCount
        {
            get { return _workingQueues.Count; }
        }

        public int TotalCustomersCount { get { return _totalCustomersCount; } }
        public int CurrentCustomersCount { get { return _currentCustomersCount; } }

        public void AddCustomer(CustomerArgs args)
        {
            try
            {
                if (args == null) throw new ArgumentNullException(nameof(args));

                var queue = _workingQueues[args.QueueType];
                if (queue == null)
                    throw new ApplicationException("queue == null");

                lock (_syncRoot)
                {
                    queue.AddCustomer(args);
                    _totalCustomersCount++;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("AddCustomer error.", ex);
            }
        }

        public void CloseAndClearQueue(IEnumerable<QueueType> types)
        {
            try
            {
                if (types == null)
                    throw new ArgumentNullException(nameof(types));

                lock (_syncRoot)
                {
                    foreach (var t in types)
                    {
                        var queue = _workingQueues[t];
                        if (queue == null)
                            throw new ApplicationException("queue == null");
                        queue.CloseAndClear();
                        _totalCustomersCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CloseQueue", ex);
            }
        }

        public void CloseQueue(IEnumerable<QueueType> types)
        {
            try
            {
                if (types == null)
                    throw new ArgumentNullException(nameof(types));

                lock (_syncRoot)
                {
                    foreach (var t in types)
                    {
                        var queue = _workingQueues[t];
                        if (queue == null)
                            throw new ApplicationException("queue == null");
                        queue.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("CloseQueue", ex);
            }
        }

        public void OpenQueue(IEnumerable<QueueType> types)
        {
            try
            {
                if (types == null)
                    throw new ArgumentNullException(nameof(types));

                lock (_syncRoot)
                {
                    foreach (var t in types)
                    {
                        var queue = _workingQueues[t];
                        if (queue == null)
                            throw new ApplicationException("queue == null");
                        queue.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("OpenQueue", ex);
            }
        }

        public CustomerArgs GetNextCustomer(QueueType type)
        {
            var queue = QueueByType(type);
            try
            {
                Monitor.Enter(_syncRoot);
                Interlocked.Decrement(ref _totalCustomersCount);
                return queue.GetCustomer();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("GetNextCustomer error.", ex);
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        private BankQueueCommon QueueByType(QueueType type)
        {
            try
            {
                var queue = _workingQueues[type];
                if (queue == null)
                    throw new ApplicationException("queue == null");

                return queue;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("QueueByType error.", ex);
            }
        }
    }
}
