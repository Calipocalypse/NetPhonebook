using NetPhonebook.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records.ViewModels
{
    public class ListEntryEditorViewModel : BindableBase, IVirtualCellDataProvider
    {
        public VirtualModelsCellData GetCellData()
        {
            throw new NotImplementedException();
        }
    }
}
