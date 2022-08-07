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
        public ObservableCollection<VirtualModelsData> VirtualModelsData;
        private IDataProvider _dataProvider;

        private VirtualModelsData selectedItem;
        public VirtualModelsData SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public RecordEntriesClickerViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            VirtualModelsData = _dataProvider.GetVirtualModelsDataWithCellData().Where(x => x.);
        }
    }
}
