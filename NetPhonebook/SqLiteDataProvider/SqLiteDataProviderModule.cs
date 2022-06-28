using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NetPhonebook.Core.Collections;
using System.Windows;

namespace SqLiteDataProvider
{
    public class SqLiteDataProviderModule : IDataProvider
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
        public void AddVirtualModel(VirtualModel toCreate)
        {
            using (var context = new NetphonebookContext())
            {
                context.Add(toCreate);
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
    }
}