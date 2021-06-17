using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.DTO;
using House.IService;
using System.Data.Entity;

namespace House.Service
{
    public class PermissionService : IPermissionService
    {
        public long AddPermission(string PermissionDes, string PermissionName)
        {
            using (HouseContext context = new HouseContext())
            {
                //校验权限是否已经存在
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(context);
                //PermissionEntity[] array = context.Permissions.Where(r => r.Name.Equals(PermissionName)).ToArray();
                var count = baseService.GetAll().Where(r => r.Name == PermissionName).LongCount();
                if (count < 1)
                {

                    PermissionEntity permissionEntity = new PermissionEntity();
                    permissionEntity.Description = PermissionDes;
                    permissionEntity.Name = PermissionName;
                    context.Permissions.Add(permissionEntity);
                    context.SaveChanges();

                    return permissionEntity.Id;  //返回新增成功后数据的Id
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool DeleteData(long id)
        {
            using (HouseContext context = new HouseContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(context);
                //当查询的目的只是单纯用来显示数据的，可以用AsNoTracking优化EF性能
                return baseService.MarkDeleted(id);
            }
        }

        public PermissionsDTO[] GetAll()
        {
            using (HouseContext context = new HouseContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(context);
                //当查询的目的只是单纯用来显示数据的，可以用AsNoTracking优化EF性能
                return baseService.GetAll().AsNoTracking().ToList().Select(m => ToDTO(m)).ToArray();
            }
        }

        public PermissionsDTO[] GetAllByStr(string id)
        {
            using (HouseContext context = new HouseContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(context);
                return baseService.GetAll().Where(p => p.Name.Contains(id)).AsNoTracking().ToList().Select(m => ToDTO(m)).ToArray();
            }
        }

        /// <summary>
        /// 根据角色id查找角色权限表中权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public PermissionsDTO[] GetPermissionByRoleId(long roleId)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> role = new BaseService<RoleEntity>(db);
                var roleEntity = role.GetById(roleId);
                if (roleEntity == null)
                {
                    throw new Exception("角色Id" + roleId + "不存在");
                }
                else
                {
                    return roleEntity.Permissions.Select(pe => ToDTO(pe)).ToArray();                  
                }
            }
        }

        private PermissionsDTO ToDTO(PermissionEntity pe)
        {
            PermissionsDTO dto = new PermissionsDTO();
            dto.Id = pe.Id;
            dto.Name = pe.Name;
            dto.CreateDateTime = pe.CreateDateTime;
            dto.Description = pe.Description;
            return dto;
        }
    }
}
