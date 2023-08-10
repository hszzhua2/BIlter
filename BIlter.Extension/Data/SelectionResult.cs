using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Data
{
    /// <summary>
    /// Revit selection result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectionResult<T>
    {
        /// <summary>
        /// succeeded
        /// </summary>
        public SelectionResult()
        {
            Succeeded = true;
        }

        /// <summary>
        /// message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// result
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// selction state
        /// </summary>
        public bool Succeeded { get; set; }
    }
}
