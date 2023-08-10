using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Extension.Extensions;
using BIlter.Toolkit.Mvvm;
using BIlter.Toolkit.Mvvm.Extensions;
using BIlter.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System.Windows;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class MaterialsCommand : CommandBase
    {
        public override Window CreateMainWidow()
        {
            return SimpleIoc.Default.Resolve<Views.Materials, MaterialsViewModel>();
        }

        public override Result Execute(ref string message, ElementSet elements)
        {
            TransactionStatus status = DataContext.GetDocument().NewTransactionGroup("资源管理",
                () =>
                {
                    return MainWindow.ShowDialog().Value;
                });
            return status == TransactionStatus.Committed ? Result.Succeeded : Result.Cancelled;
        }
        
    }
}
