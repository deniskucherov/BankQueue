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
    /// Interaction logic for OperatingRoomView.xaml
    /// </summary>
    public partial class OperatingRoomView : UserControl
    {
        public OperatingRoomView(OperationalViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
