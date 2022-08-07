using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Interfaces
{
    public interface IDataDestroyer
    {
        void DestroyCategory(ExtraCategory toDestroy);
        void DestroyInfo(ExtraInfo toDestroy);
        void DestroyModel(VirtualModel toDestroy);
        void DestroyFavouriteColor(SolidColorBrush toDestroy);
        void DestroyVirtualData(VirtualModelsData toDestroy);
    }
}
