using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Interfaces
{
    public interface IDataCreator
    {
        void AddCategory(ExtraCategory category);
    }
}
