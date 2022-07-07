using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core
{
    public enum DataProviderEnum : byte
    {
        CsvDataProvider = 0,
        MsSqlDataProvider = 1,
        FakeDataProvider = 2,
        SqLiteDataProvider = 3,
    }
}
