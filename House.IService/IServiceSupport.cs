using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.IService
{
    //一个标识接口，所有服务都必须实现这个接口
    //这样可以保证只有真正的服务实现类才会被注册到autofac（只有继承该接口的接口才会被注册到autofac）,例如DbContext不需要ioc去管理,为了区分这些
    public interface IServiceSupport
    {

    }
}
