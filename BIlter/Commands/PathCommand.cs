using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Toolkit.Mvvm;
using BIlter.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BIlter.Extension.Extensions;
using BIlter.Toolkit.Mvvm.Extensions;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class PathCommand : CommandBase
    {
        public override Window CreateMainWidow()
        {
            return SimpleIoc.Default.Resolve<Views.PathWindow, PathViewModel>();
        }

        public override Result Execute(ref string message, ElementSet elements)
        {
            TransactionStatus status = DataContext.GetDocument().NewTransactionGroup("防火分区",
                () =>
                {
                    return MainWindow.ShowDialog().Value;

                });
            return status == TransactionStatus.Committed ? Result.Succeeded : Result.Failed;
        }
    }
}
