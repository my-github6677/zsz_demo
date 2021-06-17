using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.Service;

namespace House.Service.Configs
{
    class RoleConfig : EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfig()
        {
            ToTable("T_Roles");
            HasMany(r => r.Permissions).WithMany(p => p.Roles).Map(m => m.ToTable("T_RolePermissions")
                .MapLeftKey("RoleId").MapRightKey("PermissionId"));
            Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
