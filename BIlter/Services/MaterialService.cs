using Autodesk.Revit.DB;
using BIlter.Entity;
using BIlter.Extension.Extensions;
using BIlter.Interfaces;
using BIlter.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IDataContext _dataContext;

        public MaterialService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public BOX_Material CreateELement(string name)
        {
            Element element = null;
            _dataContext.Document.NewTransaction("创建材质", () =>
            {
                ElementId id = Material.Create(_dataContext.Document, name);
                element = _dataContext.Document.GetElement(id);
            });
            return new BOX_Material(element as Material);
        }

        public void DeleteElement(BOX_Material element)
        {
            if (element == null)
                return;

            _dataContext.Document.NewTransaction("删除材质", () =>
            {
                /*_dataContext.Document.Delete(element.Id);*/
            });
        }

        public void DeleteElements(IEnumerable<BOX_Material> elements)
        {
            _dataContext.Document.NewTransaction("删除材质", () =>
            {
                foreach (var material in elements)
                {
                    /*_dataContext.Document.Delete(elements.Id);*/
                }
            });
        }


        public IEnumerable<BOX_Material> GetElements(Func<BOX_Material, bool> predicate = null)
        {
            FilteredElementCollector elements = new FilteredElementCollector(_dataContext.Document).OfClass(typeof(Material));
            IEnumerable<BOX_Material> materials = elements.ToList()
                .ConvertAll(x => new BOX_Material(x as Material));

            if (predicate != null)
            {
                materials = materials.Where(predicate);
            }
            return materials;
        }

        public void Export(IEnumerable<BOX_Material> elements)
        {
            throw new NotImplementedException();
        }

        public IExcelTransfer<BOX_Material> Import()
        {
            throw new NotImplementedException();
        }
    }
}
