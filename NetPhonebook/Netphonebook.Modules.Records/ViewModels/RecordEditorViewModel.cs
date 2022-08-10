using Netphonebook.Modules.Records.Views;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class RecordEditorViewModel : BindableBase, INavigationAware
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

        private IRegion[] CellRegions = new IRegion[6];
        private IVirtualCellDataProvider[] Cell = new IVirtualCellDataProvider[6];

        public DelegateCommand AddSaveButton { get; set; }
        private DelegateCommand DeleteButton;

        public RecordEditorViewModel(IDataProvider dataProvider, IRegionManager regionManager) 
        {
            _dataProvider = dataProvider;
            _regionManager = regionManager;
            AddSaveButton = new DelegateCommand(ClickedAddSaveButton);
        }
        
        /* Trigger */

        public void OnEntryChange(VirtualModelsData selectedItem)
        {
            //Iteration for every Cell Instance
            for (int i = 0; i < Cell.Length; i++)
            {
                //Iteration to find right cell data to Cell
                for (int j = 0; i < Cell.Length; j++)
                {
                    var singleCells = selectedItem.CellDatas;
                    var wantedCell = singleCells.FirstOrDefault(x => x.CellId == i);
                    Cell[i].FillCellData(wantedCell);
                }
            }
        }

        private VirtualModelsCustomization GetCellCustomization(int index)
        {
            return givenModel.CustomizationCells[index];
        }

        /* Buttons */

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            givenModel = navigationContext.Parameters.GetValue<VirtualModel>("model");
            ComposeRegionManager();
            CreateInstanceOfEntriesClicker();
        }

        private void CreateInstanceOfEntriesClicker()
        {
            EntriesClickerContext = new RecordEntriesClickerViewModel(this, _dataProvider, givenModel.Id);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        /* Entry Picker Load */

        /* Loading model */


        /* Creating Data */
        private void ClickedAddSaveButton()
        {
            var newEntry = ComposeNewEntry();
            _dataProvider.AddVirtualData(newEntry);
            EntriesClickerContext.AddEntry(newEntry);
        }

        private VirtualModelsData ComposeNewEntry()
        {
            var newEntry = new VirtualModelsData
            {
                Id = Guid.NewGuid(),
                DisplayedNumber = "5344",
                ModelBaseId = givenModel.Id,
                CellDatas = GetCellDatas()
            };
            return newEntry;
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

        private VirtualModelsCellData ComposeCellDataOfList()
        {
            throw new NotImplementedException();
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
                object newView;
                switch (givenModel.CustomizationCells[i].CellType)
                {
                    case CellRecordType.List: CreateCellListType(i);
                        break;
                    case CellRecordType.Text: CreateCellTextType(i);
                        break;
                    default: throw new NotImplementedException();
                        break;
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
            var newVM = new ListEntryEditorViewModel(_dataProvider, categoryId);
            InjectCellViewToRegion(new ListEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }

        private void CreateCellTextType(int cellId)
        {
            var newVM = new TextEntryEditorViewModel();
            InjectCellViewToRegion(new TextEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }
    }
}
