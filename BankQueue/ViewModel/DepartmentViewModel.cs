using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class DepartmentViewModel : CommonViewModel
    {
        private readonly IOperationProcessor _operationProcessor;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDepartmentManager _departmentManager;

        private ICommand _addWorkPlace, _deleteWorkPlace;
        private ICommand _startWork, _pauseWork, _stopWork;

        private int _workPlaceId;

        public DepartmentViewModel(Department department)
        {
            if (department == null) throw new ArgumentNullException("department");

            if (_operationProcessor == null)
            {
                // poor man's dependency injection
                _operationProcessor = ServiceLocator.Current.TryResolve<IOperationProcessor>();
                if (_operationProcessor == null)
                    throw new ApplicationException("_operationProcessor == null");
                _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                if (_eventAggregator == null)
                    throw new ApplicationException("_eventAggregator == null");
                _departmentManager = ServiceLocator.Current.TryResolve<IDepartmentManager>();
                if (_departmentManager == null)
                    throw new ApplicationException("_departmentManager == null");
            }
            
            Department = department;
            Workplaces = new ObservableCollection<IWorkPlace>();

            _operationProcessor.ProcessStarted += OnProcessStarted;
            _operationProcessor.ProcessCompleted += OnProcessCompleted;
        }

        public DepartmentViewModel(Department department, IOperationProcessor operationProcessor, IEventAggregator eventAggregator) 
            : this(department)
        {
            if (operationProcessor == null) throw new ArgumentNullException("operationProcessor");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            _operationProcessor = operationProcessor;
            _eventAggregator = eventAggregator;
        }

        public Department Department { get; private set; }
        public int WorkPlaceCount { get { return _departmentManager.WorkpPlacesCount; } }

        public ObservableCollection<IWorkPlace> Workplaces { get; private set; } 
        public IWorkPlace SelectedWorkPlace { get; set; } 

        public ICommand AddWorkPlaceCommand
        {
            get { return _addWorkPlace ?? (_addWorkPlace = new DelegateCommand(ExecuteAddWorkPlaceCommand)); }
        }

        public ICommand DeleteWorkPlaceCommand
        {
            get { return _deleteWorkPlace ?? (_deleteWorkPlace = new DelegateCommand(ExecuteDeleteWorkPlaceCommand)); }
        }

        public ICommand StartWorkCommand
        {
            get { return _startWork ?? (_startWork = new DelegateCommand(ExecuteStartWorkCommand)); }
        }

        public ICommand StopWorkCommand
        {
            get { return _stopWork ?? (_stopWork = new DelegateCommand(ExecuteStopWorkCommand)); }
        }

        public ICommand PauseWorkCommand
        {
            get { return _pauseWork ?? (_pauseWork = new DelegateCommand(ExecutePauseWorkCommand)); }
        }

        private void ExecutePauseWorkCommand()
        {
            try
            {

            }
            catch (Exception ex)
            {
                HandleException("ExecutePauseWorkCommand error.", ex);
            }
        }

        private void ExecuteStopWorkCommand()
        {
            try
            {
                if (SelectedWorkPlace == null) return;
                _operationProcessor.StopWorkplaceProccess(SelectedWorkPlace);
            }
            catch (Exception ex)
            {
                HandleException("ExecuteStopWorkCommand error.", ex);
            }
        }

        private void ExecuteStartWorkCommand()
        {
            try
            {
                if (SelectedWorkPlace == null) return;
                _operationProcessor.StartWorkplaceProccess(SelectedWorkPlace);
            }
            catch (Exception ex)
            {
                HandleException("ExecuteStartWorkCommand error.", ex);
            }
        }

        private void ExecuteAddWorkPlaceCommand()
        {
            try
            {
                var workPlace = _departmentManager.CreateWorkplace(Department.QueueType);
                var officer = _departmentManager.CreateOfficer();
                workPlace.AddOfficer(officer);
                Workplaces.Add(workPlace);
            }
            catch (Exception ex)
            {
                HandleException("ExecuteAddWorkPlaceCommand error.", ex);
            }
        }

        private void ExecuteDeleteWorkPlaceCommand()
        {
            if (SelectedWorkPlace == null) return;

            try
            {
             //   WorkPlaceCount--;
             //   Workplaces.Remove(SelectedWorkPlace);
             //   SelectedWorkPlace = Workplaces.FirstOrDefault();

                OnPropertyChanged(() => SelectedWorkPlace);
            }
            catch (Exception ex)
            {
                HandleException("ExecuteDeleteWorkPlaceCommand error.", ex);
            }
        }

        private void OnProcessCompleted(object sender, CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");
            _eventAggregator.GetEvent<CustomerServedEvent>().Publish(args);
        }

        private void OnProcessStarted(object sender, CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");
            _eventAggregator.GetEvent<CustomerServeStartsEvent>().Publish(args);
        }
    }
}
