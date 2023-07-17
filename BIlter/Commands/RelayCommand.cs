using Autodesk.Revit.Attributes;
using BIlter.ViewModels;
using BIlter.Views;
using Nice3point.Revit.Toolkit.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BIlter.Commands
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class RelayCommand : ExternalCommand
    {
        public override void Execute()
        {
            var viewModel = new BIlterViewModel();
            var view = new BIlterView(viewModel);
            view.ShowDialog();
        }
    }
}
