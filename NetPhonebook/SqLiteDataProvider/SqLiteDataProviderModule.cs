using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SqLiteDataProvider
{
    public class SqLiteDataProviderModule : IDataProvider
    {
        public SqLiteDataProviderModule()
        {
            new NetbookContext();
        }

        public void AddCategory(string categoryName)
        {
            using (var context = new NetbookContext())
            {
                context.Add(new ExtraCategory(categoryName));
                context.SaveChanges();
            }
        }

        public ExtraInfo GetExtraInfo()
        {
            return GetExtraInfoList().First();
        }
        public List<ExtraInfo> GetExtraInfoList()
        {
            return new List<ExtraInfo>
            {
                
            };
        }
    }
}