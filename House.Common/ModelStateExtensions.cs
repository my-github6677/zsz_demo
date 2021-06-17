using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Common
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取model验证错误信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string ExpendErrors(this System.Web.Mvc.Controller controller)
        {
            StringBuilder strBuild = new StringBuilder();
            foreach (var item in controller.ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    int itemErrorCount = item.Errors.Count;
                    for (int i = 0; i < itemErrorCount; i++)
                    {
                        strBuild.Append(item.Errors[i].ErrorMessage);
                        strBuild.Append("<br/>");
                    }
                }
            }
            return strBuild.ToString();
        }
    }
}
