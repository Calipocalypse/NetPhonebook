using NetPhonebook.Core.Enums;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records
{
    internal interface IVirtualCellDataProvider
    {
        CellRecordType CellRecordType { get; set; }
        VirtualModelsCellData GetVirtualCellData(Guid? mainModelData, int cellId);
        void FillCellData(VirtualModelsCellData selectedItemCell);
        void ClearCellData();
        bool IsCellReadyToCompose();

    }
}
