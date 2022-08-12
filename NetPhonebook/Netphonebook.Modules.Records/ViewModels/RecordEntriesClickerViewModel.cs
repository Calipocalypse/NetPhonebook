using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class RecordEntriesClickerViewModel : BindableBase
    {
        public ObservableCollection<VirtualModelsData> VirtualModelsData { get; set; }
        private IDataProvider _dataProvider;

        private VirtualModelsData selectedItem;
        public VirtualModelsData SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                SetProperty(ref selectedItem, value);
                parentViewModel.OnEntryChange(selectedItem);
            }
        }

        private RecordEditorViewModel parentViewModel;
        public RecordEditorViewModel ParentViewModel
        {
            get { return parentViewModel; }
            set { SetProperty(ref parentViewModel, value); }
        }

        public RecordEntriesClickerViewModel(RecordEditorViewModel parentViewModel, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            ParentViewModel = parentViewModel;
        }

        public void GetEntriesListOfGivenModelId(Guid ModelId)
        {
            VirtualModelsData = _dataProvider.GetVirtualModelsDataWithCellDataForGivenModel(ModelId);
        }

        public bool IsSelectedItemNull()
        {
            if (selectedItem == null) return true;
            else return false;
        }
        
        public void AddEntry(VirtualModelsData newEntry)
        {
            VirtualModelsData.Add(newEntry);
        }

        internal void DeleteEntry()
        {
            VirtualModelsData.Remove(SelectedItem);
        }
    }
}
