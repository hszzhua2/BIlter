using BIlter.Entity;
using BIlter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.IServices
{
    public interface IMaterialService :IDataService<BOX_Material>, IExcelTransfer<BOX_Material>
    {
        
    }
}
