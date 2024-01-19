using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIlter.Extension.Extensions;

namespace BIlter.Entity
{
    public class BOX_Path : ObservableObject
    {

        public BOX_Path(PathOfTravel pathOfTravel)
        {
            PathOfTravel = pathOfTravel;

            PathEnd = pathOfTravel.PathEnd;

            PathStart = pathOfTravel.PathStart;

            List<Curve> pathCurve = new List<Curve>(pathOfTravel.GetCurves());

            CurveLength = 0.0;

            foreach (Curve curve in pathCurve)
            {
                CurveLength += curve.Length;
            }

            CurveLength = CurveLength.ConvertToMeters();


        }
        public string FormattedCurveLength
        {
            get
            {
                return CurveLength.ToString("00.00");
            }
        }

        public ElementId Id { get => PathOfTravel.Id; }


        private string _pathName;
        public string PathName
        {
            get
            {
                return $"From(({PathStart.X.ConvertToMeters():0.00}, {PathStart.Y.ConvertToMeters():0.00}, {PathStart.Z.ConvertToMeters():0.00}) to ({PathEnd.X.ConvertToMeters():0.00}, {PathEnd.Y.ConvertToMeters():0.00}, {PathEnd.Z.ConvertToMeters():0.00}))";
            }
            set { Set(ref _pathName, value); }
        }

        private double _curveLength;

        public double CurveLength
        {
            get { return _curveLength; }
            set { Set(ref _curveLength, value); }
        }


        private XYZ _pathEnd;

        public XYZ PathEnd
        {
            get { return _pathEnd; }
            set { Set(ref _pathEnd, value); }
        }

        public string PathStartString
        {
            get { return $"({PathStart.X:0.00} {PathStart.Y:0.00} {PathStart.Z:0.00})"; }
            set { Set(ref _pathStartString, value); }
        }
        private string _pathStartString;

        private XYZ _pathStart;


        public XYZ PathStart
        {
            get { return _pathStart; }
            set { Set(ref _pathStart, value); }
        }


        public PathOfTravel PathOfTravel { get; set; }

        public Document Document { get => PathOfTravel.Document; }

    }
}
