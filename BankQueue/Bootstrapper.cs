using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Bank.Common;
using Bank.Common.Interface;
using BankQueue.Core;
using BankQueue.Core.QueueModel;
using BankQueue.Model;
using BankQueue.View;
using BankStatistic.Module;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace BankQueue
{
    internal sealed class Bootstrapper : UnityBootstrapper
    {
        protected override void InitializeShell()
        {
            var wnd = (Window) Shell;
            wnd.Closing += WndOnClosing;
            Application.Current.MainWindow = wnd;
            Application.Current.MainWindow.Show();
        }

        private void WndOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            try
            {
                var admin = Container.Resolve<IAdministrator>();
                var generator = Container.Resolve<ICustomerGenerator>();
                generator.Stop();

                var queueInformation = Container.Resolve<IQueueInformation>();

                if (queueInformation.TotalCustomersCount > 0)
                {
                    if (
                        MessageBox.Show("Wait till there are still any customers in a bank?", "Bye...",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        cancelEventArgs.Cancel = true;
                        return;
                    }
                }

                admin.CloseOffice();
            }
            catch
            {

            }
        }

        protected override DependencyObject CreateShell()
        {
            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(ApplicationRegion.Admin.ToString(), typeof (AdminView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.DataService.ToString(), typeof (DataServerView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.ReportService.ToString(), typeof (ReportServerView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.Entrance.ToString(), typeof (EntranceView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.Queue.ToString(), typeof(QueueView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.OperationRoom.ToString(), typeof (OperatingRoomView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.Seif.ToString(), typeof (SeifView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.CashDesk.ToString(), typeof (CashDeskView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.Statistic.ToString(), typeof (StatisticView));

            var wnd = new MainWindow();
            SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
            return wnd;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo("Statistics module", typeof(StatisticModule).AssemblyQualifiedName));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            var queueProcessor = Container.Resolve<BankQueueProcessor>();
            Container.RegisterInstance(typeof (IQueueProcessor), queueProcessor);
            Container.RegisterInstance(typeof (IOperationQueue), queueProcessor);
            Container.RegisterInstance(typeof (IQueueInformation), queueProcessor);

            Container.RegisterType<IEntranceDemon, EntranceDemon>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICustomerGenerator, EntranceDemon>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IOperationProcessor, OperationRoomProcessor>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IStampProvider, CashierDesk>(new ContainerControlledLifetimeManager());
                 
            Container.RegisterType<IAdministrator, BankAdministrator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOfficeInformation, BankAdministrator>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IDepartmentManager, DepartmentManager>(new ContainerControlledLifetimeManager());
;        }
    }
}
