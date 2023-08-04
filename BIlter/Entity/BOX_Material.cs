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

namespace BIlter.Entity
{
    public class BOX_Material : ObservableObject
    {
        //定义DB.Material => 为material
        public BOX_Material(Material material)
        {
            Material = material;

            _name = material.Name;
            _color = material.Color;
            _appearanceColor = GetAppearanceColor();
        }

        //上述的材质名称=>字段'_name'；上述材质颜色=>字段'_color'
        private string _name;
        private Color _color;
        private Color _appearanceColor;



        public Material Material { get; private set; }

        public Document Document { get => Material.Document; }

        //材质名称 =>可修改
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                Document.NewTransaction("Modify Name", () => Material.Name = _name);

            }
        }
        //材质颜色 =>可修改
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                Document.NewTransaction("Modify Color", () => Material.Color = _color);
                RaisePropertyChanged();
            }
        }

        public Color AppearanceColor
        {
            get => _appearanceColor;
            set
            {
                _appearanceColor = value;
                Document.NewTransaction("Modify Appearance Color", () => SetAppearanceColor(_appearanceColor));
                RaisePropertyChanged();
            }
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
            if (id != null || id.IntegerValue == -1)
            {
                using (AppearanceAssetEditScope scope = new AppearanceAssetEditScope(Document))
                {
                    Asset asset = scope.Start(id);
                    GetColorProperty(asset).SetValueAsColor(color);
                    scope.Commit(true);
                }
            }
        }
        public void Save()
        {
            Document.NewTransaction("修改名称", () =>
            {
                Material.Name = this._name;
                Material.Color = this._color;
                SetAppearanceColor(this._appearanceColor);
            });
        }
    }
}