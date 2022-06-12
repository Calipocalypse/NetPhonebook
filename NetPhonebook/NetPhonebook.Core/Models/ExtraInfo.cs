using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class ExtraInfo
    {   
        public Guid Id { get; init; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
