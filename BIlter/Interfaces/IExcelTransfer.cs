using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Interfaces
{
    public interface IExcelTransfer<TElement>
    {


        IExcelTransfer<TElement> Import();



        void Export(IEnumerable<TElement> elements);    
    }
}
