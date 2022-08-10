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
            set { SetProperty(ref firstText, value); }
        }

        private string secondText;
        public string SecondText
        {
            get { return secondText; }
            set { SetProperty(ref secondText, value); }
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
    }
}
