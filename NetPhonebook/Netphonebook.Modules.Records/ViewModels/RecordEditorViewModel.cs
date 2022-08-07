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
        
        /* Buttons */

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            givenModel = navigationContext.Parameters.GetValue<VirtualModel>("model");
            ComposeRegionManager();
            EntriesClickerContext = new RecordEntriesClickerViewModel();
            EntriesClickerContext.
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

            //Add new record to list
            //Add new record to database
        }

        private VirtualModelsData ComposeNewEntry()
        {
            GetCellDatas();
        }

        private void GetCellDatas()
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
                    default: throw new Exception();
                        break;
                }
            }
        }

        private void CreateCellListType(int cellId)
        {
            var newVM = new ListEntryEditorViewModel();
            InjectCellViewToRegion(new ListEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }

        private void CreateCellTextType(int cellId)
        {
            var newVM = new TextEntryEditorViewModel();
            InjectCellViewToRegion(new TextEntryEditor(newVM), cellId);
            Cell[cellId] = newVM;
        }

        private void InjectCellViewToRegion(object view, int cellId)
        {
            CellRegions[cellId].Add(view);
            CellRegions[cellId].Activate(view);
        }
    }
}
