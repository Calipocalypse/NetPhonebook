using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Collections
{
    public class CloneableObservableCollection<T> : ObservableCollection<T>, ICloneable
    {
        public object Clone()
        {
            return (CloneableObservableCollection<T>)this.MemberwiseClone();
        }
    }
}
