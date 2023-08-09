
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Toolkit.Mvvm.Extensions;
using BIlter.Toolkit.Mvvm.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Toolkit.Mvvm
{
    public class DataContext : IDataContext
    {
        public Document GetDocument()
        {
            return SimpleIoc.Default.GetInstance<Document>();
        }

        public UIApplication GetUIApplication()
        {
            return GetUIDocument().Application;
        }

        public UIDocument GetUIDocument()
        {
            return new UIDocument(GetDocument());
        }
    }
}
