using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Prism.Modularity;
using Prism.Regions;

namespace BankStatistic.Module
{
    public sealed class StatisticModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StatisticModule(IRegionManager regionManager)
        {
            if (regionManager == null) throw new ArgumentNullException(nameof(regionManager));
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ApplicationRegion.Statistic.ToString(), typeof (StatisticView));
        }
    }
}
