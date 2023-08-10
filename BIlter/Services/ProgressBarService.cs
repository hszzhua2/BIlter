using BIlter.IServices;
using BIlter.Toolkit.Mvvm.Extensions;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIlter.Services
{
    //实现
    public class ProgressBarService : IProgressBarService
    {
        Views.ProgressBarDialog _dialog;
        public void Start(int maximum)
        {

            _dialog = SimpleIoc.Default.Resolve<Views.ProgressBarDialog, ViewModels.ProgressBarDialogViewModel>();
            _dialog.Show();

            Messenger.Default.Send<int>(maximum, Contacts.Tokens.ProgressBarMaximum);
        }

        public void Stop()
        {
            Messenger.Default.Unregister(_dialog.DataContext);
            _dialog.Close();
        }
    }
}
