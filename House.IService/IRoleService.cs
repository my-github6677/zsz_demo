using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.DTO;

namespace House.IService
{
    public interface IRoleService: IServiceSupport
    {
        long AddRole(string Name);

        int AddRolePermission(long roleID, long[] PermissionIds);

        RoleDTO[] GetAll(int currentIndex, int pageSize);

        RoleDTO[] GetAll();

        RoleDTO[] GetByName(string name);

        RoleDTO GetById(long id);

        RoleDTO[] GetByAdminUserId(long id);

        int EditRolePermission(long Id, string Name, long[] PermissionIds);
    }
}
