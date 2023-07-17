﻿using Autodesk.Revit.Attributes;
using BIlter.ViewModels;
using BIlter.Views;
using Nice3point.Revit.Toolkit.External;

namespace BIlter.Commands
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class Command : ExternalCommand
    {
        public override void Execute()
        {
            //新建了一个viewModel连接到
            var viewModel = new BIlterViewModel();
            var view = new BIlterView(viewModel);
            view.ShowDialog();
        }
    }
}