using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetPhonebook.Core.Interfaces
{
    public interface IDataCreator
    {
        void AddCategory(ExtraCategory toCreate);
        void AddInfo(ExtraInfo toCreate);
        void AddVirtualModel(VirtualModel toCreate);
        void AddFavouriteColor(FavouriteColor toCreate);
        void AddVirtualData(VirtualModelsData toCreate);
    }
}
