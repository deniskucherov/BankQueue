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
using BankQueue.Core.Annotations;
using BankQueue.ViewModel;

namespace BankQueue.Control
{
    /// <summary>
    /// Interaction logic for QueueControl.xaml
    /// </summary>
    public partial class QueueStandartControl : UserControl
    {
        public QueueStandartControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty QueueTypeProperty = DependencyProperty.Register(
            "QueueType", typeof (QueueType), typeof (QueueStandartControl), new PropertyMetadata(default(QueueType), QueueTypeChangedCallback));

        public static readonly DependencyProperty QueueNameProperty = DependencyProperty.Register(
            "QueueName", typeof (string), typeof (QueueStandartControl), new PropertyMetadata(default(string)));

        public string QueueName
        {
            get { return (string) GetValue(QueueNameProperty); }
            set { SetValue(QueueNameProperty, value); }
        }

        public QueueType QueueType
        {
            get { return (QueueType) GetValue(QueueTypeProperty); }
            set { SetValue(QueueTypeProperty, value); }
        }

        private static void QueueTypeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var control = dependencyObject as QueueStandartControl;

            var vm = new QueueControlViewModel();
            vm.QueueType = (QueueType)args.NewValue;
            control.DataContext = vm;
            control.QueueName = args.NewValue.ToString();
        }
    }
}
