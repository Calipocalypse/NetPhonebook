using Netphonebook.Modules.Models.Views;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using NetPhonebook.Core;
using NetPhonebook.Core.Enums;
using Netphonebook.Modules.Common.ViewModels;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class ModelCreatorViewModel : BindableBase, INavigationAware
    {
        //ToDo Lets make cell pick as list of buttons with content as: "1", "2", "3"...

        private IRegionManager _regionManager;
        private IDataProvider _dataProvider;

        public EditorsMode Mode { get; set; }
        public VirtualModel toEdit;

        public ColorPickerViewModel colorPickerInstance;
        public ColorPickerViewModel ColorPickerInstance 
        {
            get { return colorPickerInstance; } 
            set { SetProperty(ref colorPickerInstance, value); }
        } 

        public DelegateCommand ClickBack { get; set; }
        public DelegateCommand ClickAdd { get; set; }
        public DelegateCommand<string> SetColor { get; set; }

        /* Edit or Add preparation */
        private string addEditButtonContent;
        public string AddEditButtonContent
        {
            get { return addEditButtonContent; }
            set { SetProperty(ref addEditButtonContent, value); }
        }

        private string modelName;
        public string ModelName
        {
            get { return modelName; }
            set { SetProperty(ref modelName, value); }
        }

        private sbyte numberOfCells = 1;
        public sbyte NumberOfCells
        {
            get { return numberOfCells; }
            set
            {
                if (selectedCellNumber > value) SelectedCellNumber = value; 
                SetProperty(ref numberOfCells, value); 
            }
        }

        private sbyte selectedCellNumber = 1;
        public sbyte SelectedCellNumber
        {
            get { return selectedCellNumber; }
            set
            {
                SetProperty(ref selectedCellNumber, value);
                OnCellChange();
            }
        }

        private void OnCellChange()
        {
            RaisePropertyChanged(nameof(FontSize));
            RaisePropertyChanged(nameof(BorderSize));
            RaisePropertyChanged(nameof(CornerRadius));
            RaisePropertyChanged(nameof(FontColor));
            RaisePropertyChanged(nameof(BackgroundColor));
            RaisePropertyChanged(nameof(BorderColor));
        }

        private sbyte[] fontSize = new sbyte[6];
        public sbyte FontSize
        {
            get { return fontSize[SelectedCellNumber-1]; }
            set { SetProperty(ref fontSize[SelectedCellNumber-1], value); }
        }

        private sbyte[] borderSize = new sbyte[6];
        public sbyte BorderSize
        {
            get { return borderSize[SelectedCellNumber-1]; }
            set { SetProperty(ref borderSize[SelectedCellNumber-1], value); }
        }

        private sbyte[] cornerRadius = new sbyte[6];
        public sbyte CornerRadius
        {
            get { return cornerRadius[SelectedCellNumber-1]; }
            set { SetProperty(ref cornerRadius[SelectedCellNumber-1], value); }
        }

        private string[] fontColor = new string[6];
        public SolidColorBrush FontColor
        {
            get { return HexColorConverter.ToSolidColor(fontColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref fontColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        private string[] backgroundColor = new string[6];
        public SolidColorBrush BackgroundColor
        {
            get { return HexColorConverter.ToSolidColor(backgroundColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref backgroundColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        private string[] borderColor = new string[6];
        public SolidColorBrush BorderColor
        {
            get { return HexColorConverter.ToSolidColor(borderColor[SelectedCellNumber - 1]); }
            set { SetProperty(ref borderColor[SelectedCellNumber-1], HexColorConverter.ToHex(value)); }
        }

        public ModelCreatorViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            ClickBack = new DelegateCommand(NavigateBack);
            ClickAdd = new DelegateCommand(ClickedAdd);
            SetColor = new DelegateCommand<string>(ClickedSetColor);
            ComposeUiElements();
        }

        private void ComposeUiElements()
        {
            ColorPickerInstance = new ColorPickerViewModel(_dataProvider);
        }

        private void ClickedSetColor(string parameter)
        {
            switch (parameter)
            {
                case "fontColor":
                    FontColor = ColorPickerInstance.OutcomingColor;
                    break;
                case "backgroundColor":
                    BackgroundColor = ColorPickerInstance.OutcomingColor;
                    break;
                case "borderColor":
                    BorderColor = ColorPickerInstance.OutcomingColor;
                    break;
                default: throw new NotImplementedException();
            }
        }

        private void ClickedAdd()
        {
            switch (Mode)
            {
                case EditorsMode.Add: AddNewModel(ComposeNewModel());
                    break;
                case EditorsMode.Edit: EditCurrentModel(ComposeNewModel(toEdit.Id));
                    break;
            }
        }

        private VirtualModel ComposeNewModel(Guid ModelId = new Guid())
        {
            if (ModelId == Guid.Empty) ModelId = Guid.NewGuid();
            //VirtualModelsCusotmization First
            List<VirtualModelsCustomization> toAddVMC = new List<VirtualModelsCustomization>();
            for (byte i = 0; i < numberOfCells; i++)
            {
                var newSingleVMC = new VirtualModelsCustomization
                {
                    Id = Guid.NewGuid(),
                    ModelId = ModelId,
                    CellId = i,
                    BorderColor = borderColor[i],
                    ForegroundColor = fontColor[i],
                    BackgroundColor = backgroundColor[i],
                    CornerRadius = cornerRadius[i].ToString(),
                    BorderSize = borderSize[i].ToString(),
                    FontSize = fontSize[i].ToString()
                };
                toAddVMC.Add(newSingleVMC);
            }

            VirtualModel toAddVM = new VirtualModel
            {
                Id = ModelId,
                Name = ModelName,
                CustomizationCells = toAddVMC
            };

            return toAddVM;
        }

        private void AddNewModel(VirtualModel toAdd)
        {
            _dataProvider.AddVirtualModel(toAdd);
        }

        private void EditCurrentModel(VirtualModel toAdd)
        {
            _dataProvider.UpdateVM(toEdit, toAdd);
        }

        private void NavigateBack()
        {
            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(new ModulesView(), "ModelsViewer");
            var view = _regionManager.Regions["ContentRegion"].GetView("ModelsViewer");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            toEdit = navigationContext.Parameters.GetValue<VirtualModel>("model");
            var mode = navigationContext.Parameters.GetValue<string>("mode");
            switch (mode)
            {
                case "add": LoadAddMode(); break;
                case "edit": LoadEditMode(toEdit); break;
            }
        }

        private void LoadAddMode()
        {
            AddEditButtonContent = "Add";
            Mode = EditorsMode.Add;
        }

        private void LoadEditMode(VirtualModel toEdit)
        {
            AddEditButtonContent = "Edit";
            Mode = EditorsMode.Edit;

            ModelName = toEdit.Name;
            List<VirtualModel> c = _dataProvider.GetVirtualModelsWithCustomization();
            var toEditFromCollection = c.FirstOrDefault(x => x.Id == toEdit.Id);
            NumberOfCells = (sbyte)toEditFromCollection.CustomizationCells.Count();
            for (int i = 0; i<NumberOfCells ; i++)
            {
                fontSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].FontSize);
                cornerRadius[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].CornerRadius);
                borderSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].BorderSize);
                borderColor[toEditFromCollection.CustomizationCells[i].CellId] = toEditFromCollection.CustomizationCells[i].BorderColor;
                fontColor[toEditFromCollection.CustomizationCells[i].CellId] = toEditFromCollection.CustomizationCells[i].ForegroundColor;
                backgroundColor[toEditFromCollection.CustomizationCells[i].CellId] = toEditFromCollection.CustomizationCells[i].BackgroundColor;
            }
            OnCellChange();
        }
    }
}