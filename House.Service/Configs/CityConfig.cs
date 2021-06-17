using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.Service;

namespace House.Service.Configs
{
    class CityConfig : EntityTypeConfiguration<CityEntity>
    {
        public CityConfig()
        {
            ToTable("T_Cities");
            Property(e => e.Name).IsRequired().HasMaxLength(20);
        }
    }
}
