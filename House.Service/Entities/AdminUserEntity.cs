using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class AdminUserEntity:BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public long? CityId { get; set; }  //说明数据库这个列可以为空

        public virtual CityEntity City { get; set; }

        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }

        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
