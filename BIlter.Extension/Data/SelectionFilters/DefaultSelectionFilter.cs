using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BIlter.Extension.Data.SelectionFilters
{
    internal class DefaultSelectionFilter : Autodesk.Revit.UI.Selection.ISelectionFilter
    {
        private readonly Func<Element, bool> _predicate;

        public DefaultSelectionFilter(Func<Element, bool> predicate = null)
        {
            _predicate = predicate;
        }

        public bool AllowElement(Element elem)
        {
            if (_predicate != null)
            {
                return _predicate(elem);
            }
            return true;
        }

        public bool AllowReference(Autodesk.Revit.DB.Reference reference, XYZ position)
        {
            return true;
        }
    }
}

