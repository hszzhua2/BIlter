﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Interfaces
{
    public interface IDataContext
    {

        Document Document { get; set; }

        UIDocument GetUIDocument();

        UIApplication GetUIApplication();
        object GetDocument();
    }
}
