using MsSqlDataProvider;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetPhonebook.Modules.Lists.ViewModels
{
    public class ListsViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;
        private readonly IRegionManager _regionManager;
        public DelegateCommand ClickSeed { get; set; }

        public ListsViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _regionManager = regionManager;
            ClickSeed = new DelegateCommand(ClickedSeed);
            //MessageBox.Show("Hello from" + this.ToString());
        }

        private void ClickedSeed()
        {
            var extraInfos = _dataProvider.GetExtraInfo();
            MessageBox.Show(extraInfos.Name);
        }
    }
}
