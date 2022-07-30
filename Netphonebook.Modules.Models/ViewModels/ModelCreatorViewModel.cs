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

        private CellCreatorTextViewModel textCellViewModelInstance;
        public CellCreatorTextViewModel TextCellViewModelInstance
        {
            get { return textCellViewModelInstance; }
            set { SetProperty(ref textCellViewModelInstance, value); }
        }

        private ColorPickerViewModel colorPickerInstance;
        public ColorPickerViewModel ColorPickerInstance 
        {
            get { return colorPickerInstance; } 
            set { SetProperty(ref colorPickerInstance, value); }
        } 

        public DelegateCommand ClickBack { get; set; }
        public DelegateCommand ClickAdd { get; set; }

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
                TextCellViewModelInstance.OnCellChange();
            }
        }

        public ObservableCollection<CellRecordType> CellDataTypes
        {
            get { return new ObservableCollection<CellRecordType>(Enum.GetValues<CellRecordType>()); }
        }

        public ModelCreatorViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            ComposeUiElements();
            ComposeDelegateCommands();
        }

        private void ComposeDelegateCommands()
        {
            ClickBack = new DelegateCommand(NavigateBack);
            ClickAdd = new DelegateCommand(ClickedAdd, IsModelValid)
                .ObservesProperty(() => TextCellViewModelInstance.BackgroundColorCell)
                .ObservesProperty(() => TextCellViewModelInstance.FontColorCell)
                .ObservesProperty(() => TextCellViewModelInstance.BorderColorCell)
                .ObservesProperty(() => NumberOfCells);
        }

        private void ComposeUiElements()
        {
            ColorPickerInstance = new ColorPickerViewModel(_dataProvider);
            TextCellViewModelInstance = new CellCreatorTextViewModel(_dataProvider,this,ColorPickerInstance);
        }

        private bool IsModelValid()
        {
            if (TextCellViewModelInstance.IsCellModelValid() && IsMainModelInfoValid()) return true;
            else return false;
        }

        private bool IsMainModelInfoValid()
        {
            return true;
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
                    BorderColor = HexColorConverter.ToHex((TextCellViewModelInstance.BorderColor[i])),
                    ForegroundColor = HexColorConverter.ToHex(TextCellViewModelInstance.FontColor[i]),
                    BackgroundColor = HexColorConverter.ToHex(TextCellViewModelInstance.BackgroundColor[i]),
                    CornerRadius = TextCellViewModelInstance.CornerRadius[i].ToString(),
                    BorderSize = TextCellViewModelInstance.BorderSize[i].ToString(),
                    FontSize = TextCellViewModelInstance.FontSize[i].ToString()
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
                TextCellViewModelInstance.FontSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].FontSize);
                TextCellViewModelInstance.CornerRadius[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].CornerRadius);
                TextCellViewModelInstance.BorderSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].BorderSize);
                TextCellViewModelInstance.BorderColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].BorderColor);
                TextCellViewModelInstance.FontColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].ForegroundColor);
                TextCellViewModelInstance.BackgroundColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].BackgroundColor);
            }
            TextCellViewModelInstance.OnCellChange();
        }
    }
}