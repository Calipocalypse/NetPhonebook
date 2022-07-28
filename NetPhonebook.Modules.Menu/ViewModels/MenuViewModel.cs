using Netphonebook.Modules.Models.Views;
using NetPhonebook.Modules.Lists.Views;
using NetPhonebook.Modules.Search.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Netphonebook.Modules.Records.Views;
using NetPhonebook.Core.Interfaces;

namespace NetPhonebook.Modules.Menu.ViewModels
{
    internal class MenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDataProvider _dataProvider; 

        public DelegateCommand<string> NavigateCommand { get; set; }
        public MenuViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _regionManager = regionManager;
            _dataProvider = dataProvider;
            DataProviderType = _dataProvider.ToString();
            NavigateCommand = new DelegateCommand<string>(Navigate);
            //MessageBox.Show("Hello from MenuViewModel");
        }

        private string dataProviderType;
        public string DataProviderType
        {
            get { return dataProviderType; }
            set { SetProperty(ref dataProviderType, value); }
        }

        private void Navigate(string uri)
        {
            //_regionManager.RequestNavigate("ContentRegion", uri);
            switch (uri)
            {
                case "ListsView":
                    SwitchView(new ListsView(), uri);
                    break;
                case "SearchView":
                    SwitchView(new SearchView(), uri);
                    break;
                case "ModulesView":
                    SwitchView(new ModulesView(), uri);
                    break;
                case "RecordsView":
                    SwitchView(new RecordModelPicker(), uri);
                    break;
                default:
                    break;
            }
        }

        private void SwitchView(object obj, string uri)
        {
            _regionManager.Regions["ContentRegion"].RemoveAll();
            _regionManager.Regions["ContentRegion"].Add(obj, uri);
            var view = _regionManager.Regions["ContentRegion"].GetView("uri");
        }
    }
}
