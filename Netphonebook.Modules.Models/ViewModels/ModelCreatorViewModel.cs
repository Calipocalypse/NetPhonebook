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

        private RecordPresenterViewModel presenterInstance;
        public RecordPresenterViewModel PresenterInstance
        {
            get { return presenterInstance; }
            set { SetProperty(ref presenterInstance, value); }
        }
        #endregion


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
            set 
            {
                SetProperty(ref modelName, value);
                UpdatePresenter();
            }
        }

        #region Bindings for XxxxXNumber customization

        /* 1. FontSize */
        private int fontSize = 5;
        public int FontSize
        {
            get { return fontSize; }
            set 
            {
                SetProperty(ref fontSize, value);
                UpdatePresenter();
            }
        }

        /* 2. CornerRadius */
        private int cornerRadius = 5;
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                SetProperty(ref cornerRadius, value);
                UpdatePresenter();
            }
        }
        /* 3. BorderSize */
        private int borderSize = 5;
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                SetProperty(ref borderSize, value);
                UpdatePresenter();
            }
        }
        /* 4. BackgroundColor */
        private SolidColorBrush backgroundColor;
        public SolidColorBrush BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                SetProperty(ref backgroundColor, value);
                UpdatePresenter();
            }
        }
        /* 5. BorderColor */
        private SolidColorBrush borderColor;
        public SolidColorBrush BorderColor
        {
            get { return borderColor; }
            set 
            { 
                SetProperty(ref borderColor, value);
                UpdatePresenter();
            }
        }
        /* 6. ForegroundColor */
        private SolidColorBrush foregroundColor;
        public SolidColorBrush ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                SetProperty(ref foregroundColor, value);
                UpdatePresenter();
            }
        }
        #endregion

        private sbyte numberOfCells = 1;
        public sbyte NumberOfCells
        {
            get { return numberOfCells; }
            set
            {
                if (selectedCellNumber > value) SelectedCellNumber = value; 
                SetProperty(ref numberOfCells, value);
                UpdatePresenter();
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
            SetColor = new DelegateCommand<string>(SetColorClicked);
        }

        private void SetColorClicked(string parameter)
        {
            switch(parameter)
            {
                case "backgroundColor": BackgroundColor = colorPickerInstance.OutcomingColor;
                    break;
                case "foregroundColor": ForegroundColor = colorPickerInstance.OutcomingColor;
                    break;
                case "borderColor": BorderColor = colorPickerInstance.OutcomingColor;
                    break;
            }
        }

        private void ComposeUiElements()
        {
            ColorPickerInstance = new ColorPickerViewModel(_dataProvider);
            TextCellViewModelInstance = new CellCreatorTextViewModel(_dataProvider, this, ColorPickerInstance);
            ListCellViewModelInstance = new CellCreatorListViewModel(_dataProvider, this, ColorPickerInstance);
            PresenterInstance = new RecordPresenterViewModel();
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
                        CategoryId = ListCellViewModelInstance.ExtraCategories[i].Id,
                        BorderColor = HexColorConverter.ToHex((ListCellViewModelInstance.BorderColor[i])),
                        ForegroundColor = HexColorConverter.ToHex(ListCellViewModelInstance.FontColor[i]),
                        BackgroundColor = HexColorConverter.ToHex(ListCellViewModelInstance.BackgroundColor[i]),
                        CornerRadius = ListCellViewModelInstance.CornerRadius[i].ToString(),
                        BorderSize = ListCellViewModelInstance.BorderSize[i].ToString(),
                    };
                    toAddVMC.Add(newSingleVMC);
                }
            }

            //Virtual Model Second
            VirtualModel toAddVM = new VirtualModel
            {
                Id = ModelId,
                Name = ModelName,

                FontSize = FontSize.ToString(),
                CornerRadius = CornerRadius.ToString(),
                BorderSize = BorderSize.ToString(),
                BackgroundColor = HexColorConverter.ToHex(BackgroundColor),
                BorderColor = HexColorConverter.ToHex(BorderColor),
                ForegroundColor = HexColorConverter.ToHex(ForegroundColor),

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
            UpdatePresenter();
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
                var customizedCell = toEditFromCollection.CustomizationCells[i];
                int cellId = customizedCell.CellId;

                //MainModelCustomization below
                FontSize = Convert.ToInt32(customizedCell.FontSize);
                CornerRadius = Convert.ToInt32(customizedCell.CornerRadius);
                BorderSize = Convert.ToInt32(customizedCell.BorderSize);
                BackgroundColor = HexColorConverter.ToSolidColor(customizedCell.BackgroundColor);
                BorderColor = HexColorConverter.ToSolidColor(customizedCell.BorderColor);
                ForegroundColor = HexColorConverter.ToSolidColor(customizedCell.ForegroundColor);

                CellRecordTypeArray[cellId] = customizedCell.CellType;

                if (CellRecordTypeArray[cellId] == CellRecordType.Text)
                {
                    TextCellViewModelInstance.FontSize[cellId] = Convert.ToSByte(customizedCell.FontSize);
                    TextCellViewModelInstance.CornerRadius[cellId] = Convert.ToSByte(customizedCell.CornerRadius);
                    TextCellViewModelInstance.BorderSize[cellId] = Convert.ToSByte(customizedCell.BorderSize);
                    TextCellViewModelInstance.BorderColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.BorderColor);
                    TextCellViewModelInstance.FontColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.ForegroundColor);
                    TextCellViewModelInstance.BackgroundColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.BackgroundColor);
                }
                else if (CellRecordTypeArray[cellId] == CellRecordType.List)
                {
                    ListCellViewModelInstance.FontSize[cellId] = Convert.ToSByte(customizedCell.FontSize);
                    ListCellViewModelInstance.ExtraCategories[cellId] = customizedCell.Category;
                    ListCellViewModelInstance.CornerRadius[cellId] = Convert.ToSByte(customizedCell.CornerRadius);
                    ListCellViewModelInstance.BorderSize[cellId] = Convert.ToSByte(customizedCell.BorderSize);
                    ListCellViewModelInstance.BorderColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.BorderColor);
                    ListCellViewModelInstance.FontColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.ForegroundColor);
                    ListCellViewModelInstance.BackgroundColor[cellId] = HexColorConverter.ToSolidColor(customizedCell.BackgroundColor);
                }
                else throw new NotImplementedException();
            }
            ListCellViewModelInstance.AssignCorrectSelectedIndexToComboBox();
            OnCellChange();
            UpdatePresenter();
        }

        /* Presenter handling below */
        #region Presenter

        public void UpdatePresenter()
        {
            PresenterInstance.CleanCollection();
            PresenterInstance.AddToCollection("6000", "555/19", GetTextes(), GetBackgroundColors(), GetForegroundColors(), GetBorderColors(),
                GetBorderSizes(), GetCornerRadiuses(), GetFontSizes());
        }

        private string[] GetTextes()
        {
            string[] textes = new string[6];
            for (int i = 0; i < textes.Length; i++) textes[i] = "Lorem Ipsum";
            return textes;
        }

        private SolidColorBrush[] GetBackgroundColors()
        {
            SolidColorBrush[] colors = new SolidColorBrush[6];
            for (int i = 0; i < colors.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        colors[i] = TextCellViewModelInstance.BackgroundColor[i];
                        break;
                    case CellRecordType.List:
                        colors[i] = ListCellViewModelInstance.BackgroundColor[i];
                        break;
                }
            }
            return colors;
        }

        private SolidColorBrush[] GetForegroundColors()
        {
            SolidColorBrush[] colors = new SolidColorBrush[6];
            for (int i = 0; i < colors.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        colors[i] = TextCellViewModelInstance.FontColor[i];
                        break;
                    case CellRecordType.List:
                        colors[i] = ListCellViewModelInstance.FontColor[i];
                        break;
                }
            }
            return colors;
        }

        private SolidColorBrush[] GetBorderColors()
        {
            SolidColorBrush[] colors = new SolidColorBrush[6];
            for (int i = 0; i < colors.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        colors[i] = TextCellViewModelInstance.BorderColor[i];
                        break;
                    case CellRecordType.List:
                        colors[i] = ListCellViewModelInstance.BorderColor[i];
                        break;
                }
            }
            return colors;
        }

        private int[] GetBorderSizes()
        {
            var borderSizes = new int[6];
            for (int i = 0; i<borderSizes.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        borderSizes[i] = TextCellViewModelInstance.BorderSize[i];
                        break;
                    case CellRecordType.List:
                        borderSizes[i] = ListCellViewModelInstance.BorderSize[i];
                        break;
                }
            }
            return borderSizes;
        }

        private int[] GetCornerRadiuses()
        {
            var cornerRadiuses = new int[6];
            for (int i = 0; i < cornerRadiuses.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        cornerRadiuses[i] = TextCellViewModelInstance.CornerRadius[i];
                        break;
                    case CellRecordType.List:
                        cornerRadiuses[i] = ListCellViewModelInstance.CornerRadius[i];
                        break;
                }
            }
            return cornerRadiuses;
        }

        private int[] GetFontSizes()
        {
            var fontSizes = new int[6];
            for (int i = 0; i < fontSizes.Count(); i++)
            {
                switch (CellRecordTypeArray[i])
                {
                    case CellRecordType.Text:
                        fontSizes[i] = TextCellViewModelInstance.FontSize[i];
                        break;
                    case CellRecordType.List:
                        fontSizes[i] = ListCellViewModelInstance.FontSize[i];
                        break;
                }
            }
            return fontSizes;
        }
        #endregion
    }
}