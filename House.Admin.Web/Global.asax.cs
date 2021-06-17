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
            log4net.Config.XmlConfigurator.Configure();//��������Ȼ������log4net��־���
            GlobalFilters.Filters.Add(new HouseExceptionFilter());//�����Զ����쳣������

            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());//����Բ�ǰ�ǺͿո����⣨����
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());

            //ioc����(��)
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//�ѵ�ǰ�����е� Controller ��ע��
            //��ȡ����������ĳ���
            Assembly[] assemblies = new Assembly[] { Assembly.Load("House.Service") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract
                    && typeof(IServiceSupport).IsAssignableFrom(type))
                    .AsImplementedInterfaces().PropertiesAutowired();
            //Assign����ֵ
            //type1.IsAssignableFrom(type2)   type2�Ƿ�ʵ����type1�ӿ�/type2�Ƿ�̳���type1
            //typeof(IServiceSupport).IsAssignableFrom(type)ֻע��ʵ����IServiceSupport����
            //���������޹ص���ע�ᵽAutoFac��

            var container = builder.Build();
            //ע��ϵͳ�����DependencyResolver��������MVC��ܴ���Controller�ȶ����ʱ���ǹ�AutofacҪ����
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!!

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
    }
}
