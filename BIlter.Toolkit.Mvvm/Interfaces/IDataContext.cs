using Autodesk.Revit.Creation;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Document = Autodesk.Revit.DB.Document;

namespace BIlter.Toolkit.Mvvm.Interfaces
{
    public interface IDataContext
    {
        Document GetDocument();

        UIDocument GetUIDocument();

        UIApplication GetUIApplication();

    }
}
