﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Constants
{
    /// <summary>
    /// Revit builtin parameters
    /// </summary>
    public class BuiltInParameters
    {
        /// <summary>
        /// INVALID
        /// </summary>
        public static ElementId Invaild => new ElementId(BuiltInParameter.INVALID);

        /// <summary>
        /// Type of view builtin parameters
        /// </summary>
        public class View
        {
            /// <summary>
            /// VIEW_NAME
            /// </summary>
            public static ElementId Name => new ElementId(BuiltInParameter.VIEW_NAME);

            /// <summary>
            /// VIEW_DESCRIPTION
            /// </summary>
            public static ElementId Description => new ElementId(BuiltInParameter.VIEW_DESCRIPTION);

            /// <summary>
            /// VIEW_TYPE
            /// </summary>
            public static ElementId Type => new ElementId(BuiltInParameter.VIEW_TYPE);

            /// <summary>
            /// VIEW_SCALE
            /// </summary>
            public static ElementId Scale => new ElementId(BuiltInParameter.VIEW_SCALE);

        }

        /// <summary>
        /// Sheet of view builtin parameters
        /// </summary>
        public class Sheet
        {
            /// <summary>
            /// SHEET_NAME
            /// </summary>
            public static ElementId Name => new ElementId(BuiltInParameter.SHEET_NAME);

            /// <summary>
            /// SHEET_NUMBER
            /// </summary>
            public static ElementId Number => new ElementId(BuiltInParameter.SHEET_NUMBER);

            /// <summary>
            /// SHEET_SCALE
            /// </summary>
            public static ElementId Scale => new ElementId(BuiltInParameter.SHEET_SCALE);

            /// <summary>
            /// SHEET_DATE
            /// </summary>
            public static ElementId Date => new ElementId(BuiltInParameter.SHEET_DATE);
        }

        /// <summary>
        /// Level of view builtin parameters
        /// </summary>
        public class Level
        {
            /// <summary>
            /// LEVEL_NAME
            /// </summary>
            public static ElementId Name => new ElementId(BuiltInParameter.LEVEL_NAME);
        }

        /// <summary>
        /// Symbol of view builtin parameters
        /// </summary>
        public class Symbol
        {
            /// <summary>
            /// SYMBOL_NAME_PARAM
            /// </summary>
            public static ElementId Name => new ElementId(BuiltInParameter.SYMBOL_NAME_PARAM);
        }

        /// <summary>
        /// Room of view builtin parameters
        /// </summary>
        public class Room
        {
            /// <summary>
            /// ROOM_NAME
            /// </summary>
            public static ElementId Name => new ElementId(BuiltInParameter.ROOM_NAME);

            /// <summary>
            /// ROOM_NUMBER
            /// </summary>
            public static ElementId Number => new ElementId(BuiltInParameter.ROOM_NUMBER);

            /// <summary>
            /// ROOM_AREA
            /// </summary>
            public static ElementId Area => new ElementId(BuiltInParameter.ROOM_AREA);
        }
    }
}


