using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIlter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Services
{
    public class DataContext : IDataContext
    {
        public DataContext(Document document) { this.Document = document; }

        public Document Document { get; set; }

        public UIApplication GetUIApplication()
        {
            return GetUIDocument().Application;
        }

        public UIDocument GetUIDocument()
        {
            return new UIDocument(Document);
        }
    }
}
