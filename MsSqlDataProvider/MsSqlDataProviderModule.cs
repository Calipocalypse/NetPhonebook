using NetPhonebook.Core.Interfaces;
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
            return new List<ExtraInfo>
            {
             
            };
        }
    }
}