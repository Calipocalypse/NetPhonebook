using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System.Collections.Generic;

namespace CsvDataProvider
{
    public class ModuleCSV //: IDataProvider
    {
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