using Autodesk.Revit.DB;
using BIlter.Entity;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIlter.Extension.Extensions;
using BIlter.IServices;

namespace BIlter.ViewModels
{
    public class MaterialDialogViewModel : ViewModelBase
    {
        private readonly IMaterialService _service;

        public MaterialDialogViewModel(IMaterialService service)
        {
            this._service = service;
        }
        
        public void Initial(object Sender)
        {
            if (Sender != null)
            {
                Material = (BOX_Material)Sender;
                Name = Material.Name;
                Color = Material.Color;
                AppearanceColor = Material.AppearanceColor;
            }
        }
        
        private string _name;
        private Color _color;
        private Color _appearanceColor;
        public string Name
        {
            get => _name;
            set { Set(ref _name, value); }
        }
        public Color Color
        {
            get => _color;
            set { Set(ref _color, value); }
        }
        public Color AppearanceColor
        {
            get => _appearanceColor;
            set { Set(ref _appearanceColor, value); }
        }

        public BOX_Material Material { get; set; }

        public RelayCommand SetColorCommand
        {
            get => new RelayCommand(() =>
            {
                System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Color = dialog.Color.ConvertToRevitColor();
                }
            });
        }

        public RelayCommand SetAppearanceColorCommand
        {
            get => new RelayCommand(() =>
            {
                System.Windows.Forms.ColorDialog dialog = new System.Windows.Forms.ColorDialog();
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AppearanceColor = dialog.Color.ConvertToRevitColor();
                }
            });
        }


        public RelayCommand SubmitCommand
        {
            get => new RelayCommand(() =>
            {
                if (Material == null)
                {
                    Material = this._service.CreateELement(Name);
                }
                if (Material.Name != Name)
                    Material.Name = Name;
                if (Material.Color != Color)
                    Material.Color = Color;
                if (Material.AppearanceColor != AppearanceColor)
                    Material.AppearanceColor = AppearanceColor;
                MessengerInstance.Send(true, Contacts.Tokens.MaterialDialog);
            });
        }
    }
}
