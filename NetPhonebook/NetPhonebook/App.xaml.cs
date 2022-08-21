using CsvDataProvider;
using Netphonebook.Modules.Models;
using Netphonebook.Modules.Records;
using NetPhonebook.Core.Interfaces;
using NetPhonebook.Modules.Lists;
using NetPhonebook.Modules.Menu;
using NetPhonebook.Modules.Search;
using NetPhonebook.Services;
using NetPhonebook.Services.Interfaces;
using NetPhonebook.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using SqLiteDataProvider;
using SqLiteWithLinqDataProvider;
using System.Windows;

namespace NetPhonebook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDataProvider, SqLiteWithLinqDataProviderModule>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SearchModule>();
            moduleCatalog.AddModule<MenuModule>();
            moduleCatalog.AddModule<ListsModule>();
            moduleCatalog.AddModule<ModelsModule>();
            moduleCatalog.AddModule<RecordsModule>();
        }
    }
}
