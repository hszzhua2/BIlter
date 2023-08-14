using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class FirePathCommand : IExternalCommand
    {

        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            try
            {
                XYZ point = new XYZ(0.0, 0, 0); // Corrected to use 'new XYZ'

                // Find the room where the point is located
                Room room = doc.GetRoomAtPoint(point);

                if (room != null)
                {
                    TaskDialog.Show("Room Information",
                        $"Room Name: {room.Name}\n" +
                        $"Level: {doc.GetElement(room.LevelId).Name}\n" +
                        $"Number: {room.Number}");
                }
                else
                {
                    TaskDialog.Show("Room Information", "No room found at the specified point.");
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
