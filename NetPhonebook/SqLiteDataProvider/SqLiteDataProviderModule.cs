using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace SqLiteDataProvider
{
    public class SqLiteDataProviderModule : IDataProvider
    {
        public SqLiteDataProviderModule()
        {
            new NetbookContext("Changethis");
        }

        public ExtraInfo GetExtraInfo()
        {
            return GetExtraInfoList().First();
        }
        public List<ExtraInfo> GetExtraInfoList()
        {
            var newCategory = new ExtraCategory { Id = Guid.NewGuid(), Name = "Typ" };
            return new List<ExtraInfo>
            {
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Podłużnia",
                    Category = newCategory
                },
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pochylnia",
                    Category = newCategory
                }
            };
        }
    }
}