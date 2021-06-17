using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace House.Admin.Web.App_Start
{
    public class HouseExceptionFilter:IExceptionFilter
    {
        //只要Action发生未处理的异常，这个方法都能捕获到
        private static ILog log = LogManager.GetLogger(typeof(IExceptionFilter)); //声明一个log4net对象，建议一个类只声明一个Ilog对象
        public void OnException(ExceptionContext fliterContext)
        {
            log.ErrorFormat("出现未处理的异常{0}", fliterContext.Exception);
        }
    }
}