using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netphonebook.Modules.Records.Interfaces
{
    public interface ICellStateWatcher
    {
        public void CheckIfDataModelIsValid();
    }
}
