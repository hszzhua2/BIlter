using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace BIlter.Views
{
    /// <summary>
    /// PathWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PathWindow : Window
    {
        public PathWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<bool>(this, Contacts.Tokens.PathsDialog, CloseWindow);
            this.Unloaded += Materials_Unloaded;
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

