using House.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.IService
{
    public interface IAdminService: IServiceSupport
    {
        AdminDTO[] GetAll();

        AdminDTO GetById(long id);

        long AddUser(string Name, string newpassword, string phone, string email, long? CityId);

        int AddAdminUserRole(long userId, long[] RoleIds);

        AdminDTO IsExistsByPhone(string phone);

        AdminDTO[] GetByTimeName(string stime, string etime, string username);

        int UpdateAdminUser(long Id, string Name, string newpassword, string phone, string email, long? CityId, long[] RoleIds);

        bool HasPermissions(long? loginId, string name);
    }
}
