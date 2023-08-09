using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BIlter.Toolkit.Mvvm.Interfaces
{
    public interface IUIProvider
    {
        UIControlledApplication GetUIApplication();

        ControlledApplication GetApplication();

        IntPtr GetWindowHandle();

        AddInId GetAddInId();
    }
}
