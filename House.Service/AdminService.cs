using House.DTO;
using House.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.Common;

namespace House.Service
{
    public class AdminService : IAdminService
    {
        public int AddAdminUserRole(long userId, long[] RoleIds)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> adminbs = new BaseService<AdminUserEntity>(db);
                var adminUser = adminbs.GetById(userId);
                if (adminUser == null)
                {
                    throw new Exception("不存在Id为" + userId + "的管理员");
                }
                else
                {
                    BaseService<RoleEntity> bs = new BaseService<RoleEntity>(db);
                    var roles = bs.GetAll().ToList().Where(m => RoleIds.Contains(m.Id)).ToArray();//得到的结果不再进行操作可以加AsNoTracking()
                    for (int i = 0; i < roles.Length; i++)
                    {
                        adminUser.Roles.Add(roles[i]);
                    }
                    return db.SaveChanges();
                }
            }
        }

        public long AddUser(string Name, string newpassword, string phone, string email, long? CityId)
        {
            using (HouseContext db = new HouseContext())
            {
                AdminUserEntity adminUserEntity = new AdminUserEntity();
                adminUserEntity.Name = Name;
                string salt = CommonHelper.CreateVerifyCode(6);//获取密码盐
                adminUserEntity.PasswordSalt = salt;
                string pwdMD5 = CommonHelper.CalcMD5(newpassword + salt);//用户输入的密码和盐md5加密
                adminUserEntity.PasswordHash = pwdMD5;
                adminUserEntity.PhoneNum = phone;
                adminUserEntity.Email = email;
                adminUserEntity.CityId = null;
                if (CityId != 0)
                {
                    adminUserEntity.CityId = CityId;
                }
                db.AdminUsers.Add(adminUserEntity);
                db.SaveChanges();
                return adminUserEntity.Id;
            }
        }

        /// <summary>
        /// 查询用户的权限，是否符合Attribute标记
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasPermissions(long? loginId, string name)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> ba = new BaseService<AdminUserEntity>(db);
                var amdin = ba.GetAll().Include(m => m.City).SingleOrDefault(m => m.Id == loginId);
                if (amdin == null)
                {
                    throw new Exception("不存在id为" + loginId + "的用户");
                }
                else
                {
                    //每个Role都有一个Permissions属性
                    //Roles.SelectMany(r => r.Permissions)就是遍历Roles的每一个Role
                    //然后把每个Role的Permissions放到一个集合中
                    //IEnumerable<PermissionEntity>
                    return amdin.Roles.SelectMany(role => role.Permissions).Any(per => per.Name == name);
                }
            }
        }


        public AdminDTO[] GetAll()
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(db);
                return bs.GetAll().AsNoTracking().Include(m => m.City).ToList().Select(m => ToDto(m)).ToArray();
            }
        }

        public AdminDTO GetById(long id)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> ba = new BaseService<AdminUserEntity>(db);
                var dto = ba.GetById(id);
                return ToDto(dto);
            }
        }

        public AdminDTO[] GetByTimeName(string stime,string etime,string username)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(db);
                var templist = bs.GetAll().AsNoTracking().ToList().Select(m => ToDto(m)).Where(m => m.Name.Contains(username));
                if (!string.IsNullOrEmpty(stime))
                {
                    templist = templist.Where(m => m.CreateDateTime.ToString("yyyy-MM-dd").CompareTo(stime) >= 0);
                }
                if (!string.IsNullOrEmpty(etime))
                {
                    templist = templist.Where(m => m.CreateDateTime.ToString("yyyy-MM-dd").CompareTo(etime) <= 0);
                }
                return templist.ToArray();
            }
        }

        public AdminDTO IsExistsByPhone(string phone)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(db);
                var dtos = bs.GetAll().AsNoTracking().Include(m => m.City).ToList().Select(m => ToDto(m)).Where(m => m.PhoneNum.Equals(phone));
                if (dtos.Count() == 0)
                {
                    return null;
                }
                else if (dtos.Count() == 1)
                {
                    return dtos.Single();
                }
                else
                {
                    throw new Exception("存在多个手机号为'" + phone + "'的数据");
                }
                
            }
        }

        public int UpdateAdminUser(long Id, string Name, string newpassword, string phone, string email, long? CityId, long[] RoleIds)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> ba = new BaseService<AdminUserEntity>(db);
                var adminEntity = ba.GetById(Id);
                if (adminEntity == null)
                {
                    throw new Exception("不存在id为" + Id + "的管理员数据");
                }
                else
                {
                    adminEntity.Name = Name;
                    adminEntity.Email = email;
                    if (CityId == 0)
                    {
                        adminEntity.CityId = null;
                    }
                    else 
                    {
                        adminEntity.CityId = CityId;
                    }                    
                    adminEntity.PhoneNum = phone;
                    if (!string.IsNullOrEmpty(newpassword))
                    {
                        var slat = CommonHelper.CreateVerifyCode(6);
                        var pwdmd5 = CommonHelper.CalcMD5(newpassword + slat);
                        adminEntity.PasswordSalt = slat;
                        adminEntity.PasswordHash = pwdmd5;
                    }
                    BaseService<RoleEntity> br = new BaseService<RoleEntity>(db);
                    adminEntity.Roles.Clear();
                    for (int i = 0; i < RoleIds.Length; i++)
                    {
                        var role = br.GetById(RoleIds[i]);
                        adminEntity.Roles.Add(role);
                    }
                    return db.SaveChanges();
                }
            }
        }

        private AdminDTO ToDto(AdminUserEntity entity)
        {
            AdminDTO dto = new AdminDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.PasswordHash = entity.PasswordHash;
            dto.PasswordSalt = entity.PasswordSalt;
            dto.PhoneNum = entity.PhoneNum;
            dto.Email = entity.Email;
            dto.LoginErrorTimes = entity.LoginErrorTimes;
            dto.CreateDateTime = entity.CreateDateTime;
            if (entity.CityId == null)
            {
                dto.CityName = "总部";
                dto.CityId = 0;
            }
            else
            {
                dto.CityName = entity.City.Name;
                dto.CityId = entity.CityId;
            }
            return dto;
        }
    }
}
