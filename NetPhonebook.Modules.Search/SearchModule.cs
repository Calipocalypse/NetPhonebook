using NetPhonebook.Core;
using NetPhonebook.Modules.Search.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace NetPhonebook.Modules.Search
{
    public class SearchModule : IModule
    {
        public readonly IRegionManager _regionManager;

        public SearchModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "SearchView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<SearchView>();
        }
    }
}