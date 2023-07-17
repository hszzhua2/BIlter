﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// revit geometry extension
    /// </summary>
    public static class GeometryExtension
    {
        private static readonly string _displayMethod = "SetForTransientDisplay";

        /// <summary>
        /// Creates geometry of transient (temporary) element for application display which will not be saved with the model.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="objects"></param>
        /// <param name="graphicsStyleId"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static ElementId TransientDisplay(this Document document, IList<GeometryObject> objects, ElementId graphicsStyleId = null)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            MethodInfo method = GetTransientDisplayMethod();
            if (method == null)
            {
                throw new Exception($"No target method");
            }

            return (ElementId)method.Invoke(null, parameters: new object[4]
            {
               document,
               ElementId.InvalidElementId,
               objects,
               graphicsStyleId ?? ElementId.InvalidElementId
            });
        }

        /// <summary>
        /// Creates geometry of transient (temporary) element for application display which will not be saved with the model.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="obj"></param>
        /// <param name="graphicsStyleId"></param>
        /// <returns>The element id of the created element</returns>
        public static ElementId TransientDisplay(this Document document, GeometryObject obj, ElementId graphicsStyleId = null)
        {
            return document.TransientDisplay(new List<GeometryObject>() { obj }, graphicsStyleId);
        }

        /// <summary>
        /// get revit internal method where is from geometry element
        /// </summary>
        /// <returns></returns>
        private static MethodInfo GetTransientDisplayMethod()
        {
            return typeof(GeometryElement)
                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.Name == _displayMethod);
        }
    }
}
