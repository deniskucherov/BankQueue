using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using BankQueue.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace BankQueue.Control
{
    /// <summary>
    /// Interaction logic for DepartmentControl.xaml
    /// </summary>
    public partial class DepartmentControl : UserControl
    {
        public DepartmentControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DepartmentNameProperty = DependencyProperty.Register(
            "DepartmentName", typeof(string), typeof(DepartmentControl), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty QueueTypeProperty = DependencyProperty.Register(
            "QueueType", typeof (QueueType), typeof (DepartmentControl), new PropertyMetadata(default(QueueType), QueueTypeChangedCallback));

        private static void QueueTypeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var control = ((DepartmentControl) dependencyObject);
            if (control.DataContext != null) return;

            var queueType = (QueueType) args.NewValue;
            var deptment = new Department(string.Format("{0} Department", args.NewValue), queueType);
            control.DataContext = new DepartmentViewModel(deptment);
            control.DepartmentName = deptment.Name;
        }

        public QueueType QueueType
        {
            get { return (QueueType) GetValue(QueueTypeProperty); }
            set { SetValue(QueueTypeProperty, value); }
        }

        public string DepartmentName
        {
            get { return (string)GetValue(DepartmentNameProperty); }
            set { SetValue(DepartmentNameProperty, value); }
        }
    }
}
