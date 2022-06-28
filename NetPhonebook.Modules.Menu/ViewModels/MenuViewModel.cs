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

namespace NetPhonebook.Modules.Menu.ViewModels
{
    internal class MenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; set; }
        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            //MessageBox.Show("Hello from MenuViewModel");
        }

        private void Navigate(string uri)
        {
            //_regionManager.RequestNavigate("ContentRegion", uri);
            switch (uri)
            {
                case "ListsView": SwitchView(new ListsView(), uri);
                    break;
                case "SearchView": SwitchView(new SearchView(), uri);
                    break;
                case "ModulesView": SwitchView(new ModulesView(), uri);
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
