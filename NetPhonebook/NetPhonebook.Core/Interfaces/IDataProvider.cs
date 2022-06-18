using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPhonebook.Core.Interfaces.DataProviders;
using NetPhonebook.Core.Models;

namespace NetPhonebook.Core.Interfaces
{
    public interface IDataProvider : IDataCreator, IDataReader, IDataUpdater, IDataDestroyer { }
}
