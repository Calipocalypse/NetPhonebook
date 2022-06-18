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
using NetPhonebook.Modules.Lists;
using NetPhonebook.Modules.Lists.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Prism.Events;
using System.Runtime.CompilerServices;
using NetPhonebook.Core.Collections;

namespace NetPhonebook.Modules.Lists.ViewModels
{
    public class ListsViewModel : BindableBase
    {
        private readonly IDataProvider _dataProvider;
        private readonly IRegionManager _regionManager;
        public DelegateCommand ClickAddCategory { get; set; }
        public DelegateCommand ClickEditCategory { get; set; }
        public DelegateCommand ClickDeleteCategory { get; set; }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { SetProperty(ref categoryName, value); }
        }

        public ExtraCategory ChosenCategory { get; set; }

        public ObservableCollection<ExtraCategory> CategoryList { get; }

        public ListsViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _regionManager = regionManager;
            CategoryList = _dataProvider.GetCategoryList();
            ClickAddCategory = new DelegateCommand(ClickedAddCategory);
            ClickEditCategory = new DelegateCommand(ClickedEditCategory);
            ClickDeleteCategory = new DelegateCommand(ClickedDeleteCategory);
            //MessageBox.Show("Hello from " + this.ToString());
        }

        private void ClickedAddCategory()
        {
            ExtraCategory newCategoryFromForm = new ExtraCategory(CategoryName);
            CategoryList.Add(newCategoryFromForm);
            _dataProvider.AddCategory(newCategoryFromForm);
        }

        private void ClickedDeleteCategory()
        {
            try {
                _dataProvider.DestroyCategory(ChosenCategory);
                CategoryList.Remove(ChosenCategory);
            }
            catch { MessageBox.Show("Error: Try to perform another action"); }
        }

        private void ClickedEditCategory()
        {
            var justEditedCategory = ChosenCategory;
            var toReplaceCategory = CategoryList.FirstOrDefault(justEditedCategory);
            justEditedCategory.Name = CategoryName;

            _dataProvider.UpdateCategory(justEditedCategory, ChosenCategory);

            CategoryList.Remove(ChosenCategory);
            CategoryList.Add(justEditedCategory);
        }
    }
}
