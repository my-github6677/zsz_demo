using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class SettingEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
