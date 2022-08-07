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
        VirtualModelsCellData GetCellData();
    }
}
