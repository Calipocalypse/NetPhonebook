using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;
using System.Collections.Generic;

namespace CsvDataProvider
{
    public class ModuleCSV : IDataProvider
    {
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