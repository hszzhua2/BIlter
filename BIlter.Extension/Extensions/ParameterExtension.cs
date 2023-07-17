﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// revit parameter extension
    /// </summary>
    public static class ParameterExtension
    {
        /// <summary>
        /// Obtain parameter value based on StorageType
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Parameter value</returns>
        public static T GetParameterValue<T>(this Parameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "parameter can not be null");
            }

            try
            {
                switch (parameter.StorageType)
                {
                    case StorageType.Integer:
                        return (T)(object)parameter.AsInteger();
                    case StorageType.Double:
                        return (T)(object)parameter.AsDouble();
                    case StorageType.String:
                        return (T)(object)parameter.AsString();
                    case StorageType.ElementId:
                        return (T)(object)parameter.AsElementId();
                    default:
                        return (T)(object)parameter.AsValueString();
                }
            }
            catch (Exception)
            {
                throw new Exception($"Invalid value conversion , revit parameter storage type is {parameter.StorageType}");
            }
        }

        /// <summary>
        /// Set parameter value based on StorageType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static bool SetParameterValue<T>(this Parameter parameter, T value)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "parameter can not be null");
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "value can not be null");
            }

            if (parameter.IsReadOnly)
            {
                throw new ArgumentNullException(nameof(parameter), "parameter is read only");
            }

            bool result = false;
            var storageType = parameter.StorageType;
            switch (storageType)
            {
                case StorageType.Integer:
                    if (value is not int intValue)
                    {
                        throw new Exception($"Invalid value conversion , revit parameter storage type is {parameter.StorageType} ");
                    }
                    result = parameter.Set(intValue);
                    break;
                case StorageType.Double:
                    if (value is not double doubleValue)
                    {
                        throw new Exception($"Invalid value conversion , revit parameter storage type is {parameter.StorageType} ");
                    }
                    result = parameter.Set(doubleValue);
                    break;
                case StorageType.String:
                    if (value is not string stringValue)
                    {
                        throw new Exception($"Invalid value conversion , revit parameter storage type is {parameter.StorageType} ");
                    }
                    result = parameter.Set(stringValue);
                    break;
                case StorageType.ElementId:
                    if (value is not ElementId idValue)
                    {
                        throw new Exception($"Invalid value conversion , revit parameter storage type is {parameter.StorageType} ");
                    }
                    result = parameter.Set(idValue);
                    break;
            }
            return result;
        }
    }
}
