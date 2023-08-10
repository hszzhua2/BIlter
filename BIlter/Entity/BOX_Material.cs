using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using GalaSoft.MvvmLight;
using BIlter.Extension.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Autodesk.Revit.DB.Color;
using Material = Autodesk.Revit.DB.Material;
using System.Runtime.CompilerServices;

namespace BIlter.Entity
{
    public class BOX_Material : ObservableObject
    {
        
        public BOX_Material(Material material)
        {
            Material = material;
        }
        public ElementId Id { get => Material.Id; }
        public Material Material { get; set; }
        public Document Document { get => Material.Document; }

        //材质名称 =>可修改
        public string Name
        {
            get => Material.Name;
            set
            {
                Document.NewTransaction("Modify Name", () => Material.Name = value);
                RaisePropertyChanged();
            }
        }
        //材质颜色 =>可修改
        public Color Color
        {
            get => Material.Color;
            set
            {
                Document.NewTransaction("Modify Color", () => Material.Color = value);
                RaisePropertyChanged();
            }
        }

        public Color AppearanceColor
        {
            get => GetAppearanceColor();
            set
            {
                Set(value, (x) => { Document.NewTransaction("Modify Appearance Color", () => SetAppearanceColor(x)); });
            }
        }

        protected void Set<T>(T value, Action<T> callback, [CallerMemberName] string name = null)
        {
            callback.Invoke(value);
            RaisePropertyChanged(name);
        }

        private AssetPropertyDoubleArray4d GetColorProperty(Asset asset)
        {
            return (AssetPropertyDoubleArray4d)asset?.FindByName("generic_diffuse");
        }


        private Color GetAppearanceColor()
        {
            ElementId id = Material.AppearanceAssetId;
            if (id == null || id.IntegerValue == -1)
            {
                return null;
            }
            AppearanceAssetElement appearanceAssetElement = Document.GetElement(id) as AppearanceAssetElement;
            Asset asset = appearanceAssetElement.GetRenderingAsset();
            return GetColorProperty(asset)?.GetValueAsColor();
        }

        private void SetAppearanceColor(Color color)
        {
            ElementId id = Material.AppearanceAssetId;
            if (id != null && id.IntegerValue != -1)
            {
                using (AppearanceAssetEditScope scope = new AppearanceAssetEditScope(Document))
                {
                    Asset asset = scope.Start(id);
                    GetColorProperty(asset)?.SetValueAsColor(color);
                    scope.Commit(true);
                }
            }
        }
    }
}