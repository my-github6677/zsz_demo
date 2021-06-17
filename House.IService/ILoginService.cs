using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.IService
{
    public interface ILoginService: IServiceSupport
    {
        bool GetByPhoneAndPwd(string phoneNum, string Pwd);
    }
}
