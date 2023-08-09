using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Toolkit.Mvvm.Interfaces;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System.Windows;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BIlter.Toolkit.Mvvm
{
    public abstract class CommandBase : IExternalCommand
    {
        public virtual void RegisterTypes(SimpleIoc simpleIoc) { }

        public abstract Window CreateMainWidow();

        public abstract Result Execute(ref string message, ElementSet elements);

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //注册Document
            SimpleIoc.Default.Register<Document>(() => commandData.Application.ActiveUIDocument.Document);
            RegisterTypes(SimpleIoc.Default);

            var window = CreateMainWidow();
            if (window != null)
            {
                MainWindow = window;    
            }

            //执行命令
            Execute(ref message, elements);
            //取消注册Document
            SimpleIoc.Default.Unregister<Document>();
            return Result.Succeeded;
        }
        public Window MainWindow { get; set; }

        protected IDataContext DataContext { get => ServiceLocator.Current.GetInstance<IDataContext>(); }
    }
}
