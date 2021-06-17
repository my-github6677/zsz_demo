using House.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.IService
{
    public interface IPermissionService: IServiceSupport
    {
        PermissionsDTO[] GetAll();

        PermissionsDTO[] GetAllByStr(string id);

        bool DeleteData(long id);

        long AddPermission(string PermissionDes,string PermissionName);

        PermissionsDTO[] GetPermissionByRoleId(long roleId);

    }
}
