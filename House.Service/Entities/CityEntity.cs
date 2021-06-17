using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class CityEntity :BaseEntity
    {
        public string Name { get; set; }
    }
}
