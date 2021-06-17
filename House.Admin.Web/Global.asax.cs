using Autofac;
using Autofac.Integration.Mvc;
using House.Admin.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using House.IService;
using House.Common;

namespace House.Admin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//程序启动然后启动log4net日志框架
            GlobalFilters.Filters.Add(new HouseExceptionFilter());//增加自定义异常处理器

            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());//处理圆角半角和空格问题（？）
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());

            //ioc配置(？)
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的 Controller 都注册
            //获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("House.Service") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract
                    && typeof(IServiceSupport).IsAssignableFrom(type))
                    .AsImplementedInterfaces().PropertiesAutowired();
            //Assign：赋值
            //type1.IsAssignableFrom(type2)   type2是否实现了type1接口/type2是否继承自type1
            //typeof(IServiceSupport).IsAssignableFrom(type)只注册实现了IServiceSupport的类
            //避免其他无关的类注册到AutoFac中

            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!!

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
    }
}
