using House.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class LoginService : ILoginService
    {
        public bool GetByPhoneAndPwd(string phoneNum, string Pwd)
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<AdminUserEntity> ba = new BaseService<AdminUserEntity>(db);
                var admin = ba.GetAll().FirstOrDefault(m => m.PhoneNum == phoneNum);
                var truePwd = Common.CommonHelper.CalcMD5(Pwd + admin.PasswordSalt);
                return admin.PasswordHash == truePwd;
            }
        }
    }
}
