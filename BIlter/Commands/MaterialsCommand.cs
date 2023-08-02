using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BIlter.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class MaterialsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;

            //do something
            Views.Materials materials = new Views.Materials(document);
            TransactionStatus status;
            using (TransactionGroup group = new TransactionGroup(document, "资源管理"))
            {
                group.Start();
                if (materials.ShowDialog().Value)
                {
                    status = group.Assimilate();
                }
                else
                {
                    status = group.RollBack();
                }
                if (status == TransactionStatus.Committed)
                {
                    return Result.Succeeded;
                }
            }
            return Result.Succeeded;
        }
    }
}
