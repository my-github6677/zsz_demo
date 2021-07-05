using House.DTO;
using House.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class RoleService : IRoleService
    {
        public long AddRole(string Name)
        {
            using (HouseContext db = new HouseContext())
            {
                RoleEntity r = new RoleEntity();
                r.Name = Name;
                db.Roles.Add(r);
                db.SaveChanges();
                return r.Id;
            }
        }

        public int AddRolePermission(long roleID, long[] PermissionIds)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> r = new BaseService<RoleEntity>(db);
                var role = r.GetById(roleID);
                if (role == null)
                {
                    throw new Exception("不存在Id为" + roleID + "的角色");
                }
                else
                {
                    BaseService<PermissionEntity> p = new BaseService<PermissionEntity>(db);
                    var pe = p.GetAll().ToList().Where(m => PermissionIds.Contains(m.Id)).ToList();
                    foreach (var item in pe)
                    {
                        role.Permissions.Add(item);
                    }
                    return db.SaveChanges();
                }
            }
        }

        public int EditRolePermission(long Id, string Name, long[] PermissionIds)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> br = new BaseService<RoleEntity>(db);
                RoleEntity roleEntity = br.GetById(Id);
                if (roleEntity == null)
                {
                    throw new Exception("不存在角色Id：" + Id);
                }
                else
                {
                    roleEntity.Name = Name;
                    roleEntity.Permissions.Clear();
                    BaseService<PermissionEntity> bp = new BaseService<PermissionEntity>(db);
                    List<PermissionEntity> list = bp.GetAll().Where(m => PermissionIds.Contains(m.Id)).ToList();
                    foreach (var item in list)
                    {
                        roleEntity.Permissions.Add(item);
                    }
                    return db.SaveChanges();
                }
            }
        }

        public RoleDTO[] GetAll(int currentIndex,int pageSize)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(db);
                return bs.GetAll().AsNoTracking().Include(r=>r.AdminUsers).Include(r=>r.Permissions)
                    .OrderByDescending(r => r.CreateDateTime)
                    .ToList().Skip(currentIndex).Take(pageSize).Select(m => ToDto(m)).ToArray();
            }
        }

        public RoleDTO[] GetAll()
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(db);
                return bs.GetAll().AsNoTracking().Include(r => r.AdminUsers).Include(r => r.Permissions)
                    .OrderByDescending(r => r.CreateDateTime)
                    .ToList().Select(m => ToDto(m)).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(long id)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> br = new BaseService<AdminUserEntity>(db);
                var aue = br.GetById(id);
                if (aue == null)
                {
                    throw new Exception("不存在id为" + id + "的管理员");
                }
                else
                {
                    return aue.Roles.Select(m => ToDto(m)).ToArray();
                }
            }

        }

        public RoleDTO GetById(long id)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> r = new BaseService<RoleEntity>(db);
                return ToDto(r.GetById(id));
            }
        }

        public RoleDTO[] GetByName(string name)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(db);
                return bs.GetAll().AsNoTracking().ToList().Where(w => w.Name.ToLower().Contains(name.ToLower())).Select(m => ToDto(m)).ToArray();
            }
        }

        private RoleDTO ToDto(RoleEntity re)
        {
            RoleDTO dto = new RoleDTO();
            dto.Id = re.Id;
            dto.Name = re.Name;
            dto.CreateDateTime = re.CreateDateTime;
            return dto;
        }
    }
}
