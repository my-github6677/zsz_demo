using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.Service;

namespace House.Service.Configs
{
    class SettingConfig : EntityTypeConfiguration<SettingEntity>
    {
        public SettingConfig()
        {
            ToTable("T_Settings");
            Property(p => p.Name).IsRequired().HasMaxLength(1024);
            Property(p => p.Value).IsRequired();
        }
    }
}
