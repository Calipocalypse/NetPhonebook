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

        #region Another Instances
        private CellCreatorTextViewModel textCellViewModelInstance;
        public CellCreatorTextViewModel TextCellViewModelInstance
        {
            get { return textCellViewModelInstance; }
            set { SetProperty(ref textCellViewModelInstance, value); }
        }

        private CellCreatorListViewModel listCellViewModelInstance;
        public CellCreatorListViewModel ListCellViewModelInstance
        {
            get { return listCellViewModelInstance; }
            set { SetProperty(ref listCellViewModelInstance, value); }
        }

        private ColorPickerViewModel colorPickerInstance;
        public ColorPickerViewModel ColorPickerInstance 
        {
            get { return colorPickerInstance; } 
            set { SetProperty(ref colorPickerInstance, value); }
        }
        #endregion

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
                OnCellChange();
            }
        }

        public ObservableCollection<CellRecordType> CellDataTypes =>  new ObservableCollection<CellRecordType>(Enum.GetValues<CellRecordType>());

        private CellRecordType[] cellRecordTypeArray = new CellRecordType[6];
        public CellRecordType[] CellRecordTypeArray
        {
            get { return cellRecordTypeArray; }
            set { SetProperty(ref cellRecordTypeArray, value); }
        }
        public CellRecordType CellRecordTypeCell
        { 
            get { return cellRecordTypeArray[SelectedCellNumber - 1]; }
            set 
            { 
                SetProperty(ref cellRecordTypeArray[SelectedCellNumber - 1], value);
                OnCellTypeChange();
            }
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
                .ObservesProperty(() => CellRecordTypeCell)
                .ObservesProperty(() => NumberOfCells);
        }

        private void ComposeUiElements()
        {
            ColorPickerInstance = new ColorPickerViewModel(_dataProvider);
            TextCellViewModelInstance = new CellCreatorTextViewModel(_dataProvider, this, ColorPickerInstance);
            ListCellViewModelInstance = new CellCreatorListViewModel(_dataProvider, this);
            OnCellTypeChange();
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

        private void OnCellChange()
        {
            RaisePropertyChanged(nameof(CellRecordTypeCell));
            if (CellRecordTypeArray[SelectedCellNumber - 1] == CellRecordType.Text) TextCellViewModelInstance.OnCellChange();
            if (CellRecordTypeArray[SelectedCellNumber - 1] == CellRecordType.List) ListCellViewModelInstance.OnCellChange();
            OnCellTypeChange();
        }

        private void OnCellTypeChange()
        {
            switch (CellRecordTypeCell)
            {
                case CellRecordType.Text:
                    {
                        TextCellViewModelInstance.CellCreatorTextVisibility = Visibility.Visible;
                        ListCellViewModelInstance.CellCreatorListVisibility = Visibility.Collapsed;
                    }
                    break;
                case CellRecordType.List:
                    {
                        TextCellViewModelInstance.CellCreatorTextVisibility = Visibility.Collapsed;
                        ListCellViewModelInstance.CellCreatorListVisibility = Visibility.Visible;
                        ListCellViewModelInstance.OnCellChange();
                    }
                    break;
            }
        }

        //Creation below

        private void ClickedAdd()
        {
            switch (Mode)
            {
                case EditorsMode.Add: AddNewModel(ComposeNewModel());
                    break;
                case EditorsMode.Edit: EditCurrentModel(ComposeNewModel(toEdit.Id));
                    break;
            }
            NavigateBack();
        }

        private VirtualModel ComposeNewModel(Guid ModelId = new Guid())
        {
            if (ModelId == Guid.Empty) ModelId = Guid.NewGuid();
            //Virtual Models Customization First
            List<VirtualModelsCustomization> toAddVMC = new List<VirtualModelsCustomization>();
            for (byte i = 0; i < numberOfCells; i++)
            {
                if (CellRecordTypeArray[i] == CellRecordType.Text)
                {
                    var newSingleVMC = new VirtualModelsCustomization
                    {
                        Id = Guid.NewGuid(),
                        ModelId = ModelId,
                        CellId = i,
                        CellType = CellRecordTypeArray[i],
                        BorderColor = HexColorConverter.ToHex((TextCellViewModelInstance.BorderColor[i])),
                        ForegroundColor = HexColorConverter.ToHex(TextCellViewModelInstance.FontColor[i]),
                        BackgroundColor = HexColorConverter.ToHex(TextCellViewModelInstance.BackgroundColor[i]),
                        CategoryId = null,
                        CornerRadius = TextCellViewModelInstance.CornerRadius[i].ToString(),
                        BorderSize = TextCellViewModelInstance.BorderSize[i].ToString(),
                        FontSize = TextCellViewModelInstance.FontSize[i].ToString()
                    };
                    toAddVMC.Add(newSingleVMC);
                }
                else if (CellRecordTypeArray[i] == CellRecordType.List)
                {
                    var newSingleVMC = new VirtualModelsCustomization
                    {
                        Id = Guid.NewGuid(),
                        ModelId = ModelId,
                        CellId = i,
                        CellType = CellRecordTypeArray[i],
                        FontSize = ListCellViewModelInstance.FontSize[i].ToString(),
                        CategoryId = ListCellViewModelInstance.ExtraCategories[i].Id
                    };
                    toAddVMC.Add(newSingleVMC);
                }
            }

            //Virtual Model Second
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
                CellRecordTypeArray[i] = toEditFromCollection.CustomizationCells[i].CellType;
                if (CellRecordTypeArray[i] == CellRecordType.Text)
                {
                    TextCellViewModelInstance.FontSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].FontSize);
                    TextCellViewModelInstance.CornerRadius[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].CornerRadius);
                    TextCellViewModelInstance.BorderSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].BorderSize);
                    TextCellViewModelInstance.BorderColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].BorderColor);
                    TextCellViewModelInstance.FontColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].ForegroundColor);
                    TextCellViewModelInstance.BackgroundColor[toEditFromCollection.CustomizationCells[i].CellId] = HexColorConverter.ToSolidColor(toEditFromCollection.CustomizationCells[i].BackgroundColor);
                }
                else if (CellRecordTypeArray[i] == CellRecordType.List)
                {
                    ListCellViewModelInstance.FontSize[toEditFromCollection.CustomizationCells[i].CellId] = Convert.ToSByte(toEditFromCollection.CustomizationCells[i].FontSize);
                    ListCellViewModelInstance.ExtraCategories[toEditFromCollection.CustomizationCells[i].CellId] = toEditFromCollection.CustomizationCells[i].Category;
                }
                else throw new NotImplementedException();
            }
            ListCellViewModelInstance.AssignCorrectSelectedIndexToComboBox();
            OnCellChange();
        }
    }
}