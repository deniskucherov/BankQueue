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
using BankQueue.ViewModel;

namespace BankQueue.View
{
    /// <summary>
    /// Interaction logic for QueueView.xaml
    /// </summary>
    public partial class QueueView : UserControl
    {
        public QueueView(QueueViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
