using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPhonebook.Core.Models
{
    public class UserSetting
    {
        public Guid Id { get; init; }
        public string UserMacId { get; init; }
        public Setting Setting { get; set; }
        public string Value { get; set; }
    }
}
