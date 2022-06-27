using NetPhonebook.Core.Collections;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Interfaces
{
    public interface IDataReader
    {
        ObservableCollection<ExtraCategory> GetCategoryList();
        ObservableCollection<ExtraInfo> GetExtraInfo();
    }
}
