﻿using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace MsSqlDataProvider
{
    public class MsSqlDataProviderModule //IDataProvider
    {
        public ExtraInfo GetExtraInfo()
        {
            throw new NotImplementedException();
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