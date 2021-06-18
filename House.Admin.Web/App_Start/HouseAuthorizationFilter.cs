using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using House.Common;
using House.IService;

namespace House.Admin.Web.App_Start
{
    public class HouseAuthorizationFilter : IAuthorizationFilter
    {
        //每个action执行之前都会执行次方法
        public void OnAuthorization(AuthorizationContext filterContext)
        {            
            //0:先获取要执行的action上面的attribute标记
            //获取所有attribute标记
            CheckPermissionAttribute[] checkPermission =
                (CheckPermissionAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute), false);
            if (checkPermission.Length <= 0)
            {
                //说明没有任何标记，跳出过滤器，继续执行后续action
                return;
            }

            //1:看是否登录，没有登录就返回登录页面，登录的话在做后续的判断
            long? loginId = (long?)filterContext.HttpContext.Session["LoginID"];
            if (loginId == null)
            {
                //说明没有登录，返回登录界面
                //判断请求是form表单提交还是ajax请求
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajax = new AjaxResult();
                    ajax.Data = "/Login/Index";
                    ajax.ErrorMsg = "未登录或已过期，请重新登录";
                    ajax.Status = "redirect";
                    //filterContext.Result只要被赋值，就不会再继续执行原本想要执行的action，而是会返回result的结果
                    filterContext.Result = new JsonResult { Data = ajax };
                }
                else
                {
                    //表单提交
                    filterContext.Result = new RedirectResult("/Login/Index");
                }
            }
            else  //表示已登录
            {
                //推荐下面的用法：
                //由于HouseAuthorizeFilter不是被autofac创建，因此不会自动进行属性的注入
                //需要手动获取Service对象
                IAdminService userService =
                    DependencyResolver.Current.GetService<IAdminService>();

                //2:后续的判断就是看有没有执行此action的权限
                for (int i = 0; i < checkPermission.Length; i++)
                {
                    //判断当前登录用户是否具有checkPermission[i].Name权限
                    if (!userService.HasPermissions(loginId, checkPermission[i].Name)) //没有权限，提示用户
                    {
                        //只要碰到任何一个没有的权限，就禁止访问
                        //在IAuthorizationFilter里面，只要修改filterContext.Result 
                        //那么真正的Action方法就不会执行了
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            AjaxResult ajaxResult = new AjaxResult();
                            ajaxResult.Status = "error";
                            ajaxResult.ErrorMsg = "没有权限" + checkPermission[i].Name;
                            filterContext.Result = new JsonResult { Data = ajaxResult };
                        }
                        else
                        {
                            filterContext.Result
                           = new ContentResult { Content = "没有" + checkPermission[i].Name + "这个权限" };
                        }
                        return; //跳出过滤器，继续执行后续action
                    }
                }
            }
        }
    }
}