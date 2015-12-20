using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankQueue.Core.Annotations;
using BankQueue.ViewModel;

namespace BankQueue.View
{
    /// <summary>
    /// Interaction logic for CashDeskView.xaml
    /// </summary>
    public partial class CashDeskView : UserControl
    {
        public CashDeskView([NotNull] CashDeskViewModel viewModlel)
        {
            if (viewModlel == null) throw new ArgumentNullException(nameof(viewModlel));

            InitializeComponent();
            this.DataContext = viewModlel;
        }
    }
}
