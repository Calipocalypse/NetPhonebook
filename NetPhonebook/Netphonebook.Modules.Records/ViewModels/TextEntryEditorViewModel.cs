using Netphonebook.Modules.Records.Interfaces;
using NetPhonebook.Core.Enums;
using NetPhonebook.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class TextEntryEditorViewModel : BindableBase, IVirtualCellDataProvider
    {
        private ICellStateWatcher _cellStateMainWatcher;
        private int _cellId;

        private CellRecordType cellRecordType;
        public CellRecordType CellRecordType
        {
            get { return cellRecordType; }
            set { SetProperty(ref cellRecordType, value); }
        }

        private string firstText;
        public string FirstText
        {
            get { return firstText; }
            set 
            {
                SetProperty(ref firstText, value);
                SendSignalToCheckIfValid();
            }
        }

        private string secondText;
        public string SecondText
        {
            get { return secondText; }
            set 
            { 
                SetProperty(ref secondText, value);
                SendSignalToCheckIfValid();
            }
        }

        public TextEntryEditorViewModel(ICellStateWatcher cellStateWatcher, int cellId)
        {
            _cellStateMainWatcher = cellStateWatcher;
            _cellId = cellId;
        }

        public VirtualModelsCellData GetVirtualCellData(Guid? mainModelData, int cellId)
        {
            return new VirtualModelsCellData
            {
                Id = Guid.NewGuid(),
                MainDataId = (Guid)mainModelData,
                CellId = (sbyte)cellId,
                CellType = cellRecordType,
                FirstText = firstText,
                SecondText = secondText
            };
        }

        public void FillCellData(VirtualModelsCellData selectedItemCell)
        {
            SecondText = selectedItemCell.SecondText;
            FirstText = selectedItemCell.FirstText;
        }

        public void ClearCellData()
        {
            SecondText = null;
            FirstText = null;
        }

        public bool IsCellReadyToCompose()
        {
            if (FirstText != null && FirstText !="" && SecondText != null && SecondText != "") return true;
            else return false;
        }

        private void SendSignalToCheckIfValid()
        {
            _cellStateMainWatcher.CheckIfDataModelIsValid();
        }
    }
}
