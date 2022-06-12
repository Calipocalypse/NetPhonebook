using NetPhonebook.Core;
using NetPhonebook.Modules.Lists.Views;
using NetPhonebook.Modules.Menu.Views;
using NetPhonebook.Modules.Search.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace NetPhonebook.Modules.Menu
{
    public class MenuModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MenuModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.MenuRegion, "MenuView");
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ListsView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuView>();
            containerRegistry.RegisterForNavigation<SearchView>();
            //containerRegistry.RegisterForNavigation<RecordsView>();
            //containerRegistry.RegisterForNavigation<ModelsView>();
            containerRegistry.RegisterForNavigation<ListsView>();
            //containerRegistry.RegisterForNavigation<PrintView>();
            //containerRegistry.RegisterForNavigation<ManageView>();
        }
    }
}