using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Interfaces.DataProviders
{
    public interface IDataUpdater
    {
        void UpdateCategory(ExtraCategory extraCategoryEdited, ExtraCategory extraCategory);
        void UpdateVM(VirtualModel edited, VirtualModel freshModel);
        void UpdateVirtualData(VirtualModelsData oldData, VirtualModelsData newData);
    }
}
