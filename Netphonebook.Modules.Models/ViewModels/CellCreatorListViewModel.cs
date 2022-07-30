using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Netphonebook.Modules.Models.ViewModels
{
    public class CellCreatorListViewModel : BindableBase
    {
        private Visibility cellCreatorListVisibility;
        private IDataProvider _dataProvider;
        private ModelCreatorViewModel parentView;

        public sbyte RealSelectedCellNumber => (sbyte)(parentView.SelectedCellNumber - 1);

        public Visibility CellCreatorListVisibility
        {
            get { return cellCreatorListVisibility; }
            set { SetProperty(ref cellCreatorListVisibility, value); }
        }

        public CellCreatorListViewModel(IDataProvider dataProvider, ModelCreatorViewModel ParentView)
        {
            _dataProvider = dataProvider;
            parentView = ParentView;
            Categories = GetCategories();
            SelectedIndex = 1; //ToDO
        }

        public ObservableCollection<ExtraCategory> Categories { get; }

        private ObservableCollection<ExtraCategory> GetCategories()
        {
            var oc = new ObservableCollection<ExtraCategory>();
            foreach (var x in _dataProvider.GetCategoryList())
            {
                oc.Add(x);
            }
            return oc;
        }

        private ExtraCategory[] extraCategories = new ExtraCategory[5];
        public ExtraCategory[] ExtraCategories
        {
            get { return extraCategories; }
            set 
            { 
                SetProperty(ref extraCategories, value);
                RaisePropertyChanged(nameof(ExtraCategoryCell));
            }
        }
        public ExtraCategory ExtraCategoryCell
        {
            get { return extraCategories[RealSelectedCellNumber]; }
            set { SetProperty(ref ExtraCategories[RealSelectedCellNumber], value); }
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }
        
        /* 1. FontSize */
        private sbyte[] fontSize = new sbyte[6] { 1, 2, 3, 4, 5, 6 };
        public sbyte[] FontSize
        {
            get { return fontSize; }
            set { SetProperty(ref fontSize, value); }
        }
        public sbyte FontSizeCell //for XAML
        {
            get { return fontSize[RealSelectedCellNumber]; }
            set { SetProperty(ref fontSize[RealSelectedCellNumber], value); }
        }

        public void OnCellChange()
        {
            RaisePropertyChanged(nameof(ExtraCategories));
            RaisePropertyChanged(nameof(ExtraCategoryCell));
        }
    }
}
