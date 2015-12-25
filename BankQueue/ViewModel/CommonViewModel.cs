using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;

namespace BankQueue.ViewModel
{
    public abstract class CommonViewModel : BindableBase
    {
        protected void HandleException(string message, Exception ex)
        {
            MessageBox.Show(ex.Message, message, MessageBoxButton.OK);
        }
    }
}
