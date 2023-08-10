using Autodesk.Revit.DB;
using BIlter.Entity;
using BIlter.ViewModels;
using GalaSoft.MvvmLight.Messaging;
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
using System.Windows.Shapes;

namespace BIlter.Views
{
    /// <summary>
    /// Materials.xaml 的交互逻辑
    /// </summary>
    public partial class Materials : Window
    {

        public Materials()
        {
            InitializeComponent();

            Messenger.Default.Register<bool>(this, Contacts.Tokens.MaterialsDialog, CloseWindow);
            Messenger.Default.Register<NotificationMessageAction<BOX_Material>>(this, Contacts.Tokens.ShowMaterialDialog, ShowMaterialDialog);
            this.Unloaded += Materials_Unloaded;
        }

        private void ShowMaterialDialog(NotificationMessageAction<BOX_Material> message)
        {
            MaterialDialog dialog = new MaterialDialog();
            MaterialDialogViewModel dialogViewModel = (MaterialDialogViewModel)message.Target;
            dialog.DataContext = dialogViewModel;
            dialogViewModel.Initial(message.Sender);
            if (dialog.ShowDialog().Value)
            {
                message.Execute(dialogViewModel.Material);
            };

        }

        private void Materials_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
            Messenger.Default.Unregister(this.DataContext);
        }
        private void CloseWindow(bool result)
        {
            this.DialogResult = result;
            this.Close();
        }
    }
}