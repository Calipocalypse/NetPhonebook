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
        #region Definitions
        private readonly IDataProvider _dataProvider;
        private readonly IRegionManager _regionManager;

        /* Delegate Commands */
        //Categories
        public DelegateCommand ClickAddCategory { get; set; }
        public DelegateCommand ClickEditCategory { get; set; }
        public DelegateCommand ClickDeleteCategory { get; set; }
        //Infos
        public DelegateCommand ClickAddExtraInfo { get; set; }
        public DelegateCommand ClickRemoveExtraInfo { get; set; }

        /* Bindings */
        //Categories
        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { SetProperty(ref categoryName, value); }
        }

        private ExtraCategory chosenCategory;
        public ExtraCategory ChosenCategory
        {
            get { return chosenCategory; }
            set
            {
                SetProperty(ref chosenCategory, value);
                GetExtraInfoData(ChosenCategory);
            }
        }

        public ObservableCollection<ExtraCategory> CategoryList { get; }

        //Infos
        public ObservableCollection<ExtraInfo> ExtraInfoList { get; set; }
        private ExtraInfo chosenRecord;
        public ExtraInfo ChosenRecord
        {
            get { return chosenRecord; }
            set 
            {
                SetProperty(ref chosenRecord, value);
                if (chosenRecord != null) InfoName = chosenRecord.Name;
            }
        }
        private string infoName;
        public string InfoName
        {
            get { return infoName; }
            set { SetProperty(ref infoName, value); }
        }
        #endregion

        public ListsViewModel(IRegionManager regionManager, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _regionManager = regionManager;
            CategoryList = _dataProvider.GetCategoryList();
            ExtraInfoList = new ObservableCollection<ExtraInfo>();
            ComposeDelegateCommands();
            //MessageBox.Show("Hello from " + this.ToString());
        }

        private void ComposeDelegateCommands()
        {
            ClickAddCategory = new DelegateCommand(ClickedAddCategory, CanClickAddCategory)
                .ObservesProperty(() => CategoryName);
            ClickEditCategory = new DelegateCommand(ClickedEditCategory, CanClickDeleteCategory)
                .ObservesProperty(() => ChosenCategory);
            ClickDeleteCategory = new DelegateCommand(ClickedDeleteCategory, CanClickDeleteCategory)
                .ObservesProperty(() => ChosenCategory);
            ClickAddExtraInfo = new DelegateCommand(ClickedAddExtraInfo, CanClickAddExtraInfo)
                .ObservesProperty(() => InfoName).ObservesProperty(() => ChosenCategory);
            ClickRemoveExtraInfo = new DelegateCommand(ClickedDeleteExtraInfo, CanClickRemoveExtraInfo)
                .ObservesProperty(() => ChosenRecord);
        }

        #region CanClicks

        private bool CanClickAddCategory()
        {
            if (CategoryName != null && CategoryName != "") return true;
            else return false;
        }

        private bool CanClickDeleteCategory()
        {
            if (ChosenCategory != null) return true;
            else return false;
        }

        private bool CanClickAddExtraInfo()
        {
            if (InfoName != null && InfoName != "" && ChosenCategory != null) return true;
            else return false;
        }

        private bool CanClickRemoveExtraInfo()
        {
            if (ChosenRecord != null) return true;
            else return false;
        }
        #endregion
        #region Clicked
        /* Category commands */
        private void ClickedAddCategory()
        {
            if (!IsCategoryExists())
            {
                ExtraCategory newCategoryFromForm = new ExtraCategory { Id = Guid.NewGuid(), Name = CategoryName };
                CategoryList.Add(newCategoryFromForm);
                _dataProvider.AddCategory(newCategoryFromForm);
            }
            else MessageBox.Show("Category already exists");
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
            try
            {
                var justEditedCategory = ChosenCategory;
                var toReplaceCategory = CategoryList.FirstOrDefault(justEditedCategory);
                justEditedCategory.Name = CategoryName;

                _dataProvider.UpdateCategory(justEditedCategory, ChosenCategory);

                CategoryList.Remove(ChosenCategory);
                CategoryList.Add(justEditedCategory);
            }
            catch { MessageBox.Show("Error: Try to perform another action"); }
        }

        /* ExtraInfo commands */
        private void ClickedAddExtraInfo()
        {
            if (!IsExtraInfoExists())
            {
                ExtraInfo newInfoFromForm = new ExtraInfo { Id = Guid.NewGuid(), Name = InfoName, ExtraCategoryId = ChosenCategory.Id }; //ToDo Add inactive field when Category not choosen
                ExtraInfoList.Add(newInfoFromForm);
                _dataProvider.AddInfo(newInfoFromForm);
            }
            else MessageBox.Show("This record already exists");
        }

        private void ClickedDeleteExtraInfo()
        {
            try
            {
                _dataProvider.DestroyInfo(ChosenRecord);
                ExtraInfoList.Remove(ChosenRecord);
            }
            catch { MessageBox.Show("Error: Try to perform another action"); }
        }
        #endregion

        private void GetExtraInfoData(ExtraCategory category)
        {
            ExtraInfoList.Clear();
            if (category != null) ExtraInfoList.AddRange(_dataProvider.GetExtraInfo().Where(x=>x.ExtraCategory.Name==category.Name));
        }

        private bool IsCategoryExists()
        {
            if (_dataProvider.GetCategoryList().Any(x => x.Name == CategoryName)) return true;
            else return false;
        }

        private bool IsExtraInfoExists()
        {
            if (_dataProvider.GetExtraInfo().Any(x => x.Name == InfoName)) return true;
            else return false;
        }
    }
}
