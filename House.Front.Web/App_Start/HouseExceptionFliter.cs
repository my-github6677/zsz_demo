using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Web.Mvc;

namespace House.Front.Web.App_Start
{
    public class HouseExceptionFliter:IExceptionFilter
    {
        //只要Action发生未处理的异常，都能被这个类捕获
        private static ILog log = LogManager.GetLogger(typeof(HouseExceptionFliter));//声明一个log4net Ilog对象，建议一个类只声明一个Ilog对象
        public void OnException(ExceptionContext context)
        {
            log.ErrorFormat("发生未处理的异常{0}", context.Exception);
        }

    }
}