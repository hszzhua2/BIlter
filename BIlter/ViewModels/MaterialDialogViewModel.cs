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
using Tuna.Revit.Extension;

namespace BIlter.ViewModels
{
    public class MaterialDialogViewModel : ViewModelBase
    {
        private NotificationMessageAction<BOX_Material> _message;
        public MaterialDialogViewModel(NotificationMessageAction<BOX_Material> message)
        {
            if (message.Sender is BOX_Material material)
            {
                Material = material;
                Name = material.Name;
                Color = material.Color;
                AppearanceColor = material.AppearanceColor;
            }
        }
        public string Name
        {
            get => _name;
            set { Set(ref _name, value); }
        }
        public Autodesk.Revit.DB.Color Color
        {
            get => _color;
            set { Set(ref _color, value); }
        }
        public Autodesk.Revit.DB.Color AppearanceColor
        {
            get => _appearanceColor;
            set { Set(ref _appearanceColor, value); }
        }

        public BOX_Material Material { get; set; }

        private string _name;
        private Autodesk.Revit.DB.Color _color;
        private Autodesk.Revit.DB.Color _appearanceColor;

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
                if (_message.Notification == "Create")
                {
                    Document document = _message.Target as Document;
                    document.NewTransaction(() =>
                    {
                        ElementId id = Autodesk.Revit.DB.Material.Create(document, Name);
                        Material = new BOX_Material(document.GetElement(id) as Material);
                    });
                }

                if (Material.Name != Name)
                    Material.Name = Name;
                if (Material.Color != Color)
                    Material.Color = Color;
                if (Material.AppearanceColor != AppearanceColor)
                    Material.AppearanceColor = AppearanceColor;
                MessengerInstance.Send(Material, "InsertMaterial");
                _message.Execute(Material);

                Messenger.Default.Send(true, Contacts.Tokens.MaterialsDialog);
            });
        }

    }
}
