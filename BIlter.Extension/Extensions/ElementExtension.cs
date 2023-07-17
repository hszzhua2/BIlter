﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// revit element extension
    /// </summary>
    public static class ElementExtension
    {
        /// <summary>
        /// Get element <see cref="Parameter"/> by <see cref="Autodesk.Revit.DB.ElementId"/>
        /// </summary>
        /// <param name="element">host element</param>
        /// <param name="parameterId">target parameter id</param>
        /// <returns>element <see cref="Parameter"/></returns>
        public static Parameter GetParameter(this Element element, ElementId parameterId)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "element can not be null");
            }

            if (parameterId != ElementId.InvalidElementId)
            {
                foreach (Parameter item in element.Parameters)
                {
                    if (item.Id == parameterId)
                    {
                        return item;
                    }
                }
            }
            return default;
        }

        /// <summary>
        /// Get element type instances count in the document
        /// </summary>
        /// <typeparam name="T"><see cref="Autodesk.Revit.DB.Element"/> what is <see cref="Autodesk.Revit.DB.ElementType"/> typical corresponding pair </typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IDictionary<ElementType, int> GetElementTypeInstancesCount<T>(this IEnumerable<ElementType> types) where T : Element
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types), "types can not be null");
            }

            Dictionary<ElementType, int> result = new Dictionary<ElementType, int>();
            if (types.Any())
            {
                Dictionary<ElementId, int> counts = types.ToDictionary((e) => e?.Id, _ => 0);
                Document document = types.First().Document;
                var elements = document.GetElements<T>();
                foreach (var element in elements)
                {
                    var id = element.GetTypeId();

                    if (counts.TryGetValue(id, out int count))
                    {
                        counts[id] = count++;
                        continue;
                    }
                    counts.Add(id, 0);
                }
                result = counts.ToDictionary(p => document.GetElement(p.Key) as ElementType, p => p.Value);
            }
            return result;
        }


        /// <summary>
        /// Get element types which has instances in the document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<ElementType> HasInstances<T>(this IEnumerable<ElementType> types) where T : Element
        {
            return types.GetElementTypeInstancesCount<T>().Where(p => p.Value > 0).ToDictionary(p => p.Key, p => p.Value).Keys.ToList();
        }
    }
}
