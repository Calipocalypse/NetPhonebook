using Netphonebook.Modules.Common.ViewModels;
using Netphonebook.Modules.Records.Enums;
using Netphonebook.Modules.Records.Interfaces;
using Netphonebook.Modules.Records.Views;
using NetPhonebook.Core;
using NetPhonebook.Core.Enums;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class RecordEditorViewModel : BindableBase, INavigationAware, ICellStateWatcher
    {
        public VirtualModel givenModel;
        private IDataProvider _dataProvider;
        private IRegionManager _regionManager;

        private RecordEntriesClickerViewModel entriesClickerContext;
        public RecordEntriesClickerViewModel EntriesClickerContext
        {
            get { return entriesClickerContext; }
            set { SetProperty(ref entriesClickerContext, value); }
        }

        private RecordPresenterViewModel presenterInstance;
        public RecordPresenterViewModel PresenterInstance
        {
            get { return presenterInstance; }
            set { SetProperty(ref presenterInstance, value); }
        }

        private string displayedNumber;
        public string DisplayedNumber
        {
            get { return displayedNumber; }
            set
            {
                SetProperty(ref displayedNumber, value);
                CheckIfDataModelIsValid();
            }
        }

        private string serialNumber;
        public string SerialNumber
        {
            get { return serialNumber; }
            set
            {
                SetProperty(ref serialNumber, value);
                CheckIfDataModelIsValid();
            }
        }

        private IRegion[] CellRegions = new IRegion[6];
        private IVirtualCellDataProvider[] Cell = new IVirtualCellDataProvider[6];

        private ICellStateWatcher CellStateWatcher;

        public DelegateCommand AddButton { get; set; }
        public DelegateCommand EditButton { get; set; }
        public DelegateCommand DeleteButton { get; set; }

        private bool canComposeEntry;
        public bool CanComposeEntry
        {
            get { return canComposeEntry; }
            set { SetProperty(ref canComposeEntry, value); }
        }

        public RecordEditorViewModel(IDataProvider dataProvider, IRegionManager regionManager)
        {
            _dataProvider = dataProvider;
            _regionManager = regionManager;
            CellStateWatcher = this;
            CreateInstanceOfEntriesClicker();
            ComposeButtons();
        }

        private void ComposeButtons()
        {
            AddButton = new DelegateCommand(ClickedAddButton, AreDatasValidForAdd).ObservesProperty(() => CanComposeEntry);
            EditButton = new DelegateCommand(ClickedEditButton, AreDatasValidForEdit).ObservesProperty(() => CanComposeEntry);
            DeleteButton = new DelegateCommand(ClickedDeleteButton);
        }

        /* Trigger */

        public void OnEntryChange(VirtualModelsData selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }
            //Change for displayed and serial number
            DisplayedNumber = selectedItem.DisplayedNumber;
            SerialNumber = selectedItem.SerialNumber;

            //Iteration for every USED Cell Instance
            for (int i = 0; i < givenModel.CustomizationCells.Count; i++)
            {
                //Iteration to find right cell data to Cell
                for (int j = 0; j < givenModel.CustomizationCells.Count; j++)
                {
                    var singleCells = selectedItem.CellDatas;
                    var wantedCell = singleCells.FirstOrDefault(x => x.CellId == i);
                    Cell[wantedCell.CellId].FillCellData(wantedCell);
                }
            }
        }

        private VirtualModelsCustomization GetCellCustomization(int cellId)
        {
            return givenModel.CustomizationCells.FirstOrDefault(x => x.CellId == cellId);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            givenModel = navigationContext.Parameters.GetValue<VirtualModel>("model");
            EntriesClickerContext.GetEntriesListOfGivenModelId(givenModel.Id);
            ComposeRegionManager();
        }

        private void CreateInstanceOfEntriesClicker()
        {
            EntriesClickerContext = new RecordEntriesClickerViewModel(this, _dataProvider);
            PresenterInstance = new RecordPresenterViewModel();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        /* ButtonAdd - Creating Data */
        private void ClickedAddButton()
        {
            var newEntry = ComposeEntry();
            _dataProvider.AddVirtualData(newEntry);
            EntriesClickerContext.AddEntry(newEntry);
        }

        private VirtualModelsData ComposeEntry(Guid? id = null)
        {
            if (id == null)
            {
                id = Guid.NewGuid();
            }

            var composedEntry = new VirtualModelsData
            {
                Id = (Guid)id,
                DisplayedNumber = DisplayedNumber,
                SerialNumber = SerialNumber,
                ModelBaseId = givenModel.Id,
                CellDatas = GetCellDatas()
            };
            return composedEntry;
        }

        private ICollection<VirtualModelsCellData> GetCellDatas()
        {
            ICollection<VirtualModelsCellData> cellDatas = new List<VirtualModelsCellData>();
            for (int i = 0; i < givenModel.CustomizationCells.Count; i++)
            {
                VirtualModelsCellData cellData = Cell[i].GetVirtualCellData(givenModel.Id, i);
                cellDatas.Add(cellData);
            }
            return cellDatas;
        }

        /* ButtonEdit - Editing Data */

        private void ClickedEditButton()
        {
            var editEntry = ComposeEntry(EntriesClickerContext.SelectedItem.Id);
            _dataProvider.UpdateVirtualData(EntriesClickerContext.SelectedItem, editEntry);
            EntriesClickerContext.GetEntriesListOfGivenModelId(givenModel.Id);
            CheckIfDataModelIsValid();
        }

        /* Data Validation */

        public void CheckIfDataModelIsValid()
        {
            if (CanComposeEntry == true) CanComposeEntry = false;
            else CanComposeEntry = true; //Buttons react for this and then checks by AreDatasValid validation of data
        }

        private bool AreDatasValidForAdd()
        {
            return AreDatasValid();
        }

        private bool AreDatasValidForEdit()
        {
            if (EntriesClickerContext.IsSelectedItemNull() == true) return false;
            return AreDatasValid();
        }

        private bool AreDatasValid()
        {
            if (DisplayedNumber == null || DisplayedNumber == "") return false;
            if (SerialNumber == null || SerialNumber == "") return false;
            for (int i = 0; i < givenModel.CustomizationCells.Count; i++)
            {
                if (Cell[i] == null) return false;
                else if (Cell[i].IsCellReadyToCompose() == false) return false;
            }
            return true;
        }


        /* ButtonDelete - Deleting Data */

        private void ClickedDeleteButton()
        {
            _dataProvider.DestroyVirtualData(EntriesClickerContext.SelectedItem);
            EntriesClickerContext.DeleteEntry();
            ClearDisplayedData();
        }

        private void ClearDisplayedData()
        {
            for (int i = 0; i < givenModel.CustomizationCells.Count; i++) Cell[i].ClearCellData();
            DisplayedNumber = null;
            SerialNumber = null;
        }

        /* Cells */
        private void ComposeRegionManager()
        {
            ComposeRegions();
            InjectCellViewsToRegions();
        }

        //Compose
        private void ComposeRegions()
        {
            CellRegions[0] = _regionManager.Regions["FirstCellRegion"];
            CellRegions[1] = _regionManager.Regions["SecondCellRegion"];
            CellRegions[2] = _regionManager.Regions["ThirdCellRegion"];
            CellRegions[3] = _regionManager.Regions["FourthCellRegion"];
            CellRegions[4] = _regionManager.Regions["FifthCellRegion"];
            CellRegions[5] = _regionManager.Regions["SixthCellRegion"];
        }

        //Inject
        private void InjectCellViewsToRegions()
        {
            for (int i = 0; i < givenModel.CustomizationCells.Count; i++)
            {
                var givenCustomizationCell = givenModel.CustomizationCells[i];
                var cellId = givenCustomizationCell.CellId;

                switch (givenCustomizationCell.CellType)
                {
                    case CellRecordType.List: CreateCellListType(cellId);
                        break;
                    case CellRecordType.Text: CreateCellTextType(cellId);
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        private void InjectCellViewToRegion(object view, int cellId)
        {
            CellRegions[cellId].Add(view);
            CellRegions[cellId].Activate(view);
        }

        private void CreateCellListType(int cellId)
        {
            Guid? categoryId = GetCellCustomization(cellId).CategoryId;
            var newVM = new ListEntryEditorViewModel(_dataProvider, CellStateWatcher, cellId, categoryId);
            InjectCellViewToRegion(new ListEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }

        private void CreateCellTextType(int cellId)
        {
            var newVM = new TextEntryEditorViewModel(CellStateWatcher, cellId);
            InjectCellViewToRegion(new TextEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }

        //Add adding to presenter here down there/* Presenter handling below */
        #region Presenter

        public void UpdatePresenter()
        {
            PresenterInstance.CleanCollection();
            PresenterInstance.AddToCollection(DisplayedNumber, SerialNumber, HexColorConverter.ToSolidColor(givenModel.BackgroundColor), HexColorConverter.ToSolidColor(givenModel.BorderColor), HexColorConverter.ToSolidColor(givenModel.ForegroundColor),
                Convert.ToInt32(givenModel.FontSize), Convert.ToInt32(givenModel.CornerRadius), Convert.ToInt32(givenModel.BorderSize),
                GetCellDataTextType(PresenterDataParts.Text), GetCellDataColorType(PresenterDataParts.BackgroundColor), GetCellDataColorType(PresenterDataParts.ForegroundColor), GetCellDataColorType(PresenterDataParts.BorderColor),
                GetCellDataValueType(PresenterDataParts.BorderSize), GetCellDataValueType(PresenterDataParts.CornerRadius), GetCellDataValueType(PresenterDataParts.FontSize));
        }

        private string[] GetCellDataTextType(PresenterDataParts type)
        {
            int cellCount = givenModel.CustomizationCells.Count;

            string[] toReturn = new string[cellCount];

            for (int i = 0; i < cellCount; i++)
            {
                switch (type)
                {
                    case PresenterDataParts.Text:
                        toReturn[i] = Cell[i].GetText();
                        break;
                    default: throw new NotImplementedException();

                }
            }
            return toReturn;
        }

        private SolidColorBrush[] GetCellDataColorType(PresenterDataParts type)
        {
            int cellCount = givenModel.CustomizationCells.Count;
            var toReturn = new SolidColorBrush[cellCount];
            for (int i = 0; i < cellCount; i++)
            {
                var correctCellId = givenModel.CustomizationCells[i].CellId;
                switch (type)
                {
                case PresenterDataParts.BackgroundColor:
                        toReturn[correctCellId] = HexColorConverter.ToSolidColor(givenModel.CustomizationCells[i].BackgroundColor);
                    break;
                case PresenterDataParts.ForegroundColor:
                        toReturn[correctCellId] = HexColorConverter.ToSolidColor(givenModel.CustomizationCells[i].ForegroundColor);
                        break;
                case PresenterDataParts.BorderColor:
                        toReturn[correctCellId] = HexColorConverter.ToSolidColor(givenModel.CustomizationCells[i].BorderColor);
                        break;
                default: throw new NotImplementedException();
                }
            }
            return toReturn;
        }

        private int[] GetCellDataValueType(PresenterDataParts type)
        {
            int cellCount = givenModel.CustomizationCells.Count;
            var toReturn = new int[cellCount];

            for (int i = 0; i < cellCount; i++)
            {
                var correctCellId = givenModel.CustomizationCells[i].CellId;
                switch (type)
                {
                    case PresenterDataParts.BorderSize:
                        toReturn[correctCellId] = Convert.ToInt32(givenModel.CustomizationCells[i].BorderSize);
                        break;
                    case PresenterDataParts.CornerRadius:
                        toReturn[correctCellId] = Convert.ToInt32(givenModel.CustomizationCells[i].CornerRadius);
                        break;
                    case PresenterDataParts.FontSize:
                        toReturn[correctCellId] = Convert.ToInt32(givenModel.CustomizationCells[i].FontSize);
                        break;
                }
            }
            return toReturn;

        }
        #endregion
    }
}
