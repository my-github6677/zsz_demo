using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace House.Admin.Web.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)] //只能标记在方法上面，可以同时有多个标记存在
    public class CheckPermissionAttribute : Attribute 
    {
        public string Name { get; set; }
        public CheckPermissionAttribute(string name)
        {
            this.Name = name;
        }
    }
}