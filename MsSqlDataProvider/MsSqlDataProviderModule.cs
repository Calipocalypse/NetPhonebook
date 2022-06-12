using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace MsSqlDataProvider
{
    public class MsSqlDataProviderModule : IDataProvider
    {
        public List<ExtraInfo> GetExtraInfoList()
        {
            return new List<ExtraInfo>
            {
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Podłużnia",
                    Category = "Prefix"
                },
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pochylnia",
                    Category = "Prefix"
                }
            };
        }
    }
}