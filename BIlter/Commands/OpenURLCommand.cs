using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class OpenURLCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string url = "https://www.bimobject.com/";
            try
            {
                Process.Start(url);
                return Result.Succeeded;
            }
            catch
            {
                return Result.Failed;
            }
        }
    }
}
