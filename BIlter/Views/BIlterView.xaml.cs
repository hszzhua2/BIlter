using BIlter.ViewModels;

namespace BIlter.Views
{
    public partial class BIlterView
    {
        public BIlterView(BIlterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}