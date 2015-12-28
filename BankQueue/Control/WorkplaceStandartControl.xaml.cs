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
using Bank.Common;

namespace BankQueue.Control
{
    /// <summary>
    /// Interaction logic for WorkplaceStandartControl.xaml
    /// </summary>
    public partial class WorkplaceStandartControl : UserControl
    {
        public WorkplaceStandartControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WorkplaceNameProperty = DependencyProperty.Register(
            "WorkplaceName", typeof (string), typeof (WorkplaceStandartControl), new PropertyMetadata(default(string)));

        public string WorkplaceName
        {
            get { return (string) GetValue(WorkplaceNameProperty); }
            set { SetValue(WorkplaceNameProperty, value); }
        }

        public static readonly DependencyProperty OfficerProperty = DependencyProperty.Register(
            "Officer", typeof (Officer), typeof (WorkplaceStandartControl), new PropertyMetadata(default(Officer)));

        public Officer Officer
        {
            get { return (Officer) GetValue(OfficerProperty); }
            set { SetValue(OfficerProperty, value); }
        }

        public static readonly DependencyProperty WorkStateProperty = DependencyProperty.Register(
            "WorkState", typeof (WorkState), typeof (WorkplaceStandartControl), new PropertyMetadata(default(WorkState)));

        public WorkState WorkState
        {
            get { return (WorkState) GetValue(WorkStateProperty); }
            set { SetValue(WorkStateProperty, value); }
        }

        public static readonly DependencyProperty CustomerProperty = DependencyProperty.Register(
            "Customer", typeof (Customer), typeof (WorkplaceStandartControl), new PropertyMetadata(default(Customer)));

        public Customer Customer
        {
            get { return (Customer) GetValue(CustomerProperty); }
            set { SetValue(CustomerProperty, value); }
        }
   
    }
}
