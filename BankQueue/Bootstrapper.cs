using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Bank.Common;
using Bank.Common.Interface;
using BankQueue.Core.Model;
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
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
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
            regionManager.RegisterViewWithRegion(ApplicationRegion.Safe.ToString(), typeof (SafeView));
            regionManager.RegisterViewWithRegion(ApplicationRegion.CashDesk.ToString(), typeof (CashDeskView));
            return new MainWindow();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo("Statistics module", typeof(StatisticModule).AssemblyQualifiedName));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IEntranceDemon, EntranceDemon>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IQueueProcessor, BankQueueProcessor>(new ContainerControlledLifetimeManager());

        }
    }
}
