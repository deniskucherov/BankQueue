using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;
using BankQueue.Core.Annotations;

namespace BankQueue.ViewModel
{
    public sealed class CashDeskViewModel : CommonViewModel
    {
        public CashDeskViewModel([NotNull] IStampProvider stampProvider)
        {
            if (stampProvider == null) throw new ArgumentNullException("stampProvider");
            StampProvider = stampProvider;
        }

        public IStampProvider StampProvider { get; private set; }

    }
}
