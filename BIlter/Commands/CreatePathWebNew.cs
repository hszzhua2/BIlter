using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CreatePathWebNew : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Get the active document and UI document
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                // Step 1: Prompt the user to select objects and store their exit points
                List<XYZ> exitPoints = new List<XYZ>();
                List<Reference> pickedReferences = uidoc.Selection.PickObjects(ObjectType.Element, "Select objects").ToList();

                foreach (Reference reference in pickedReferences)
                {
                    Element element = doc.GetElement(reference);

                    // Assuming the selected elements have LocationPoint as their location
                    if (element.Location is LocationPoint locationPoint)
                    {
                        XYZ exitPoint = locationPoint.Point;
                        exitPoints.Add(exitPoint);
                    }
                }

                // Step 2: Retrieve door locations
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ICollection<Element> doors = collector.OfCategory(BuiltInCategory.OST_Doors)
                    .OfClass(typeof(FamilyInstance))
                    .ToElements();

                List<XYZ> doorPoints = new List<XYZ>();

                foreach (Element door in doors)
                {
                    FamilyInstance familyInstance = door as FamilyInstance;
                    LocationPoint locationPoint = familyInstance.Location as LocationPoint;

                    if (locationPoint != null)
                    {
                        XYZ doorLocation = locationPoint.Point;
                        doorPoints.Add(doorLocation);
                    }
                }

                // Step 3: Create PathOfTravel instances and find shortest path for each exit point
                using (Transaction transaction = new Transaction(doc, "Create Path of Travel"))
                {
                    transaction.Start();

                    Dictionary<XYZ, List<ElementId>> pathStartGroup = new Dictionary<XYZ, List<ElementId>>();
                    Dictionary<XYZ, double> shortestPathLengths = new Dictionary<XYZ, double>();

                    foreach (XYZ exitPoint in exitPoints)
                    {
                        double shortestPathLength = double.MaxValue;
                        PathOfTravel shortestPath = null;

                        foreach (XYZ doorPoint in doorPoints)
                        {
                            if (exitPoint.IsAlmostEqualTo(doorPoint))
                                continue;

                            PathOfTravel path = PathOfTravel.Create(doc.ActiveView, doorPoint, exitPoint);

                            double pathLength = 0.0;
                            foreach (Curve curve in path.GetCurves())
                            {
                                pathLength += curve.Length;
                            }

                            if (!shortestPathLengths.TryGetValue(doorPoint, out double storedPathLength) || pathLength < storedPathLength)
                            {
                                shortestPathLengths[doorPoint] = pathLength;
                                shortestPathLength = pathLength;
                                shortestPath = path;
                            }
                        }

                        if (shortestPath != null)
                        {
                            XYZ pathStart = shortestPath.PathStart;

                            if (!pathStartGroup.ContainsKey(pathStart))
                            {
                                pathStartGroup[pathStart] = new List<ElementId>();
                            }

                            pathStartGroup[pathStart].Add(shortestPath.Id);

                            List<ElementId> nonShortestPaths = pathStartGroup[pathStart].Where(id => id != shortestPath.Id).ToList();

                            foreach (ElementId pathId in nonShortestPaths)
                            {
                                Element path = doc.GetElement(pathId);
                                doc.Delete(path.Id);
                            }
                        }
                    }

                    transaction.Commit();
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                // Handle OperationCanceledException here
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}

