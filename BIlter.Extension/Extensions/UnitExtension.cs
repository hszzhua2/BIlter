using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// Revit unit Extensions
    /// </summary>
    public static class UnitExtension
    {
        /// <summary>
        /// Convert value to millimeters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToMillimeters(this int value)
        {
            return ((double)value).ConvertToMillimeters();
        }


        /// <summary>
        /// Convert value to millimeters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToMillimeters(this double value)
        {
#if Rvt_21 || Rvt_22 || Rvt_23
            return UnitUtils.Convert(value, UnitTypeId.Feet, UnitTypeId.Millimeters);
#else 
            return UnitUtils.Convert(value, UnitTypeId.Feet, UnitTypeId.Millimeters);
        }
#endif

        /// <summary>
        /// Convert value to meters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToMeters(this int value)
        {
            return ((double)value).ConvertToMeters();
        }
        /// <summary>
        /// Convert value to meters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToMeters(this double value)
        {
#if Rvt_21 || Rvt_22 || Rvt_23
            return UnitUtils.Convert(value, UnitTypeId.Feet, UnitTypeId.Meters);
#else 
            return UnitUtils.Convert(value, UnitTypeId.Feet, UnitTypeId.Meters);
        }
#endif

        public static double ConvertToSquareMeters(this int value)
        {
            return ((double)value).ConvertToSquareMeters();
        }

        /// <summary>
        /// Convert value to Squaremeters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertToSquareMeters(this double value)
        {
#if Rvt_21 || Rvt_22 || Rvt_23
            return UnitUtils.Convert(value, UnitTypeId.SquareFeet, UnitTypeId.SquareMeters);
#else 
            return UnitUtils.Convert(value, UnitTypeId.SquareFeet, UnitTypeId.SquareMeters);
        }
#endif
    }
}

