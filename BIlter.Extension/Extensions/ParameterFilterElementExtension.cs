using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// Revit parameter filter extensions
    /// </summary>
    public static class ParameterFilterElementExtension
    {
        /// <summary>
        /// Get parameter filter element's filter
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ElementFilter GetElementFilter(this ParameterFilterElement element)
        {
#if Rvt_16 || Rvt_17 || Rvt_18
        var rules = element.GetRules();
        if (rules.Count > 0)
        {
            return new ElementParameterFilter(rules);
        }
        else
        {
            return null;
        }
#endif

#if Rvt_24 || Rvt_23 || Rvt_22 || Rvt_21 || Rvt_20 || Rvt_19
        return element.GetElementFilter();
#endif

            // Add a default return statement if none of the conditions are met
            return null;
        }

    }
}