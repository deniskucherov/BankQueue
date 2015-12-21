﻿using System;
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

        private ICommand _addWorkPlace, _deleteWorkPlace;
        private ICommand _startWork, _pauseWork, _stopWork;

        private int _workPlaceId = 1;

        public DepartmentViewModel(Department department)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));

            if (_operationProcessor == null)
            {
                // poor man's dependency injection
                _operationProcessor = ServiceLocator.Current.TryResolve<IOperationProcessor>();
                if (_operationProcessor == null)
                    throw new ApplicationException("_operationProcessor == null");
                _eventAggregator = ServiceLocator.Current.TryResolve<IEventAggregator>();
                if (_eventAggregator == null)
                    throw new ApplicationException("_eventAggregator == null");
            }
            
            Department = department;
            Workplaces = new ObservableCollection<Workplace>();
            _operationProcessor.ProcessCompleted += OnProcessCompleted;
        }
       
        public DepartmentViewModel(Department department, IOperationProcessor operationProcessor, IEventAggregator eventAggregator) 
            : this(department)
        {
            if (operationProcessor == null) throw new ArgumentNullException(nameof(operationProcessor));
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));

            _operationProcessor = operationProcessor;
            _eventAggregator = eventAggregator;
        }

        public Department Department { get; private set; }
        public int WorkPlaceCount { get; private set; }

        public ObservableCollection<Workplace> Workplaces { get; private set; } 
        public Workplace SelectedWorkPlace { get; set; } 

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
            
        }

        private void ExecuteStopWorkCommand()
        {
            
        }

        private void ExecuteStartWorkCommand()
        {
            _operationProcessor.StartWorkplaceProccess(SelectedWorkPlace);
        }

        private void ExecuteAddWorkPlaceCommand()
        {
            WorkPlaceCount++;
            var workPlace = new Workplace(string.Format("WorkPlace #{0}", _workPlaceId++), Department.QueueType);

            var person = new Person("Ivan S.A", 35, Gender.M);
            var officer = new Officer(person);
            workPlace.AddOfficer(officer);
            Workplaces.Add(workPlace);
        }

        private void ExecuteDeleteWorkPlaceCommand()
        {
            if (SelectedWorkPlace == null) return;

            WorkPlaceCount--;
            Workplaces.Remove(SelectedWorkPlace);
            SelectedWorkPlace = Workplaces.FirstOrDefault();
            OnPropertyChanged(()=>SelectedWorkPlace);
        }

        private void OnProcessCompleted(object sender, CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            _eventAggregator.GetEvent<CustomerServedEvent>().Publish(args);
        }
    }
}
