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
        #region <summary> 数据导入


        #endregion


        IExcelTransfer<TElement> Import();


        #region <summary> 数据导出


        #endregion



        void Export(IEnumerable<TElement> elements);    
    }
}
