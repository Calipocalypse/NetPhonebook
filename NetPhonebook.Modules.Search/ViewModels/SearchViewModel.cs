using NetPhonebook.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetPhonebook.Modules.Search.ViewModels
{
    public class SearchViewModel : BindableBase
    {
        public SearchViewModel(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
            var x = dataProvider.GetCategoryList;
        }

        public IDataProvider DataProvider { get; }
    }
}
