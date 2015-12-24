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

namespace BankQueue.Control
{
    /// <summary>
    /// Interaction logic for QueueItem.xaml
    /// </summary>
    public partial class QueueItem : UserControl
    {
        public QueueItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemColorProperty = DependencyProperty.Register(
            "ItemColor", typeof (Brush), typeof (QueueItem), new PropertyMetadata(new SolidColorBrush(Colors.Aqua)));

        public Brush ItemColor
        {
            get { return (Brush) GetValue(ItemColorProperty); }
            set { SetValue(ItemColorProperty, value); }
        }

        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            "ItemHeight", typeof (int), typeof (QueueItem), new PropertyMetadata(default(int)));

        public int ItemHeight
        {
            get { return (int) GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
    }
}
