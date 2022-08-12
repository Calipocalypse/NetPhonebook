using Netphonebook.Modules.Records.Interfaces;
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
        private int _cellId;

        private ICellStateWatcher _cellStateMainWatcher { get; }

        private IDataProvider _dataProvider;
        public ObservableCollection<ExtraInfo> ListOfCategory {get;set;}

        public ListEntryEditorViewModel(IDataProvider dataProvider, ICellStateWatcher cellStateMainWatcher, int cellId, Guid? extraCategoryId)
        {
            _cellId = cellId;
            _cellStateMainWatcher = cellStateMainWatcher;
            _dataProvider = dataProvider;
            ListOfCategory = GetListOfGivenCategory(extraCategoryId);
        }

        private CellRecordType cellRecordType;
        public CellRecordType CellRecordType
        {
            get { return cellRecordType; }
            set { SetProperty(ref cellRecordType, value); }
        }

        private int? selectedIndex;
        public int? SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                SetProperty(ref selectedIndex, value);
                SendSignalToCheckIfValid();
            }
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
            SelectedIndex = GetIndexOfGivenExtraInfo(selectedItemCell);
        }

        private int GetIndexOfGivenExtraInfo(VirtualModelsCellData selectedItemCell)
        {
            for (int i = 0; i < ListOfCategory.Count; i++)
            {
                if (selectedItemCell.extraInfoId == ListOfCategory[i].Id) return i;
            }
            throw new InvalidOperationException();
        }

        public VirtualModelsCellData GetVirtualCellData(Guid? mainModelData, int cellId)
        {
            return new VirtualModelsCellData
            {
                Id = Guid.NewGuid(),
                MainDataId = (Guid)mainModelData,
                CellId = (sbyte)cellId,
                CellType = cellRecordType,
                extraInfoId = ListOfCategory[(int)selectedIndex].Id
            };
        }

        public void ClearCellData()
        {
            SelectedIndex = null;
        }

        public bool IsCellReadyToCompose()
        {
            if (SelectedIndex != null) return true;
            else return false;
        }

        private void SendSignalToCheckIfValid()
        {
            _cellStateMainWatcher.CheckIfDataModelIsValid();
        }
    }
}
