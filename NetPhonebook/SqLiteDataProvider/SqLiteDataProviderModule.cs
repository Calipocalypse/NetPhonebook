using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using NetPhonebook.Core;
using System.Windows.Media;

namespace SqLiteDataProvider
{
    public class SqLiteDataProviderModule// : IDataProvider
    {
        public SqLiteDataProviderModule() { }

        /* Extra Category */
        public void AddCategory(ExtraCategory category)
        {
            using (var context = new NetphonebookContext())
            {
                context.Add(category);
                context.SaveChanges();
                //SeedInfos();
            }
        }

        public void DestroyCategory(ExtraCategory toDestroy)
        {
            using (var context = new NetphonebookContext())
            {
                context.Remove(toDestroy);
                context.SaveChanges();
            }
        }

        public ObservableCollection<ExtraCategory> GetCategoryList()
        {
            using (var context = new NetphonebookContext())
            {
                var oc = new ObservableCollection<ExtraCategory>();
                context.extraCategories.ToList().ForEach(category => oc.Add(category));
                return oc;
            }
        }

        public void UpdateCategory(ExtraCategory editedExtraCategory, ExtraCategory toReplaceExtraCategory)
        {
            using (var context = new NetphonebookContext())
            {
                var toReplaceExtraCategoryDB = context.extraCategories.FirstOrDefault(x => x.Id == toReplaceExtraCategory.Id);
                toReplaceExtraCategoryDB.Name = editedExtraCategory.Name;
                context.Update(toReplaceExtraCategoryDB);
                context.SaveChanges();
            }
        }

        /* Extra Infos */
        public ObservableCollection<ExtraInfo> GetExtraInfo()
        {
            using (var context = new NetphonebookContext())
            {
                var oc = new ObservableCollection<ExtraInfo>();
                context.extraInfos.Include(x => x.ExtraCategory).ToList().ForEach(info => oc.Add(info));
                return oc;
            }
        }

        public void AddInfo(ExtraInfo toCreate)
        {
            using (var context = new NetphonebookContext())
            {
                context.Add(toCreate);
                context.SaveChanges();
            }
        }

        public void DestroyInfo(ExtraInfo toDestroy)
        {
            using (var context = new NetphonebookContext())
            {
                context.Remove(toDestroy);
                context.SaveChanges();
            }
        }

        /* Virtual Models */
        public ObservableCollection<VirtualModel> GetVirtualModels()
        {
            using (var context = new NetphonebookContext())
            {
                var oc = new ObservableCollection<VirtualModel>();
                context.virtualModels.ForEachAsync(x => oc.Add(x));
                return oc;
            }
        }

        public List<VirtualModel> GetVirtualModelsWithCustomization()
        {
            using (var context = new NetphonebookContext())
            {
                var oc = new List<VirtualModel>();
                context.virtualModels.Include("CustomizationCells").ForEachAsync(x => oc.Add(x));
                //context.virtualModels.Include("VirtualModelsCustomization").ForEachAsync(x => oc.Add(x));
                return oc;
            }
        }
            public void AddVirtualModel(VirtualModel toCreate)
        {
            using (var context = new NetphonebookContext())
            {
                context.Add(toCreate);
                context.SaveChanges();
            }
        }

        public void UpdateVM(VirtualModel edited, VirtualModel freshModel)
        {
            using (var context = new NetphonebookContext())
            {
                var c = context.virtualModelsCustomizations.Where(x => x.ModelId == edited.Id);
                context.RemoveRange(c);
                context.Remove(edited);
                context.Add(freshModel);
                context.SaveChanges();
            }
        }

        public void DestroyModel(VirtualModel toDestroy)
        {
            using (var context = new NetphonebookContext())
            {
                context.Remove(toDestroy);
                context.SaveChanges();
            }
        }

        /* Favourite Colors */
        public void AddFavouriteColor(FavouriteColor toCreate)
        {
            using (var context = new NetphonebookContext())
            {
                context.Add(toCreate);
                context.SaveChanges();
            }
        }

        public ObservableCollection<SolidColorBrush> GetFavouriteColors()
        {
            var oc = new ObservableCollection<SolidColorBrush>();
            using (var context = new NetphonebookContext())
            {
                context.favouriteColors.ForEachAsync(x => oc.Add(x.SolidColorBrush));
            }
            return oc;
        }

        public void DestroyFavouriteColor(SolidColorBrush toDestroy)
        {
            using (var context = new NetphonebookContext())
            {
                var favouriteColorToRemove = context.favouriteColors.FirstOrDefault(x => x.HexColor == HexColorConverter.ToHex(toDestroy));
                context.Remove(favouriteColorToRemove);
                context.SaveChanges();
            }
        }

        public void AddVirtualData(VirtualModelsData toCreate)
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<VirtualModelsData> GetVirtualModelsDataWithCellData()
        {
            throw new System.NotImplementedException();
        }

        public void DestroyVirtualData(VirtualModelsData toDestroy)
        {
            throw new System.NotImplementedException();
        }
    }
}