using NetPhonebook.Core.Enums;
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
    public class ListEntryEditorViewModel : BindableBase, IVirtualCellDataProvider
    {
        private IDataProvider _dataProvider;
        public ObservableCollection<ExtraInfo> ListOfCategory {get;set;}

        public ListEntryEditorViewModel(IDataProvider dataProvider, Guid? extraCategoryId)
        {
            _dataProvider = dataProvider;
            ListOfCategory = GetListOfGivenCategory(extraCategoryId);
        }

        private CellRecordType cellRecordType;
        public CellRecordType CellRecordType
        {
            get { return cellRecordType; }
            set { SetProperty(ref cellRecordType, value); }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        private ObservableCollection<ExtraInfo> GetListOfGivenCategory(Guid? extraCategoryId)
        {
            var oc = new ObservableCollection<ExtraInfo>();
            foreach (var data in _dataProvider.GetExtraInfo())
            {
                if (data.ExtraCategoryId == extraCategoryId) oc.Add(data);
            }
            return oc;
        }

        public void FillCellData(VirtualModelsCellData selectedItemCell)
        {
            
        }

        public VirtualModelsCellData GetVirtualCellData(Guid? mainModelData, int cellId)
        {
            return new VirtualModelsCellData
            {
                Id = Guid.NewGuid(),
                MainDataId = (Guid)mainModelData,
                CellId = (sbyte)cellId,
                CellType = cellRecordType,
                extraInfoId = ListOfCategory[selectedIndex].Id
            };
        }
    }
}
