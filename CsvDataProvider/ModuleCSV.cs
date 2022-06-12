using NetPhonebook.Core.Interfaces;
using NetPhonebook.Core.Models;

namespace CsvDataProvider
{
    public class ModuleCSV : IDataProvider
    {
        public List<ExtraInfo> GetExtraInfoList()
        {
            return new List<ExtraInfo>
            {
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Poziom 1",
                    Category = "Poziomy"
                },
                new ExtraInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "Poziom 2",
                    Category = "Poziomy"
                }
            };
        }
    }
}