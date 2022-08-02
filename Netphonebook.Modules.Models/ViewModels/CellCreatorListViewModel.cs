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

        private ObservableCollection<ExtraCategory> extraCategories = new ObservableCollection<ExtraCategory>(new ExtraCategory[6]);
        public ObservableCollection<ExtraCategory> ExtraCategories
        {
            get { return extraCategories; }
            set { SetProperty(ref extraCategories, value); }
        }

        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set 
            { 
                SetProperty(ref selectedIndex, value);
                SaveSelectionChangeToExtraCategories();
            }
        }

        private void SaveSelectionChangeToExtraCategories()
        {
            ExtraCategories[RealSelectedCellNumber] = Categories[selectedIndex];
            ExtraCategories = ExtraCategories;
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
            RaisePropertyChanged(nameof(FontSizeCell));
            SelectedIndex = GetSelectedIndex();
        }

        private int GetSelectedIndex()
        {
            for (int i = 0; i < Categories.Count; i++)
            {
                if (ExtraCategories[RealSelectedCellNumber] == null) continue;
                if (ExtraCategories[RealSelectedCellNumber].Id == Categories[i].Id) return i;
            }
            return 0;
        }

        public void AssignCorrectSelectedIndexToComboBox()
        {
            for (int i = 0; i < Categories.Count; i++)
            {
                for (int j = 0; j < ExtraCategories.Count; j++)
                {
                    if (ExtraCategories[j] == null) continue;
                    if (Categories[i].Id == ExtraCategories[j].Id) SelectedIndex = i;
                }
            }
        }
    }
}
