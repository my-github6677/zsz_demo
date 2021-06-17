using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;

namespace House.Admin.Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetVerCode()
        {
            //获取n个随机数生成验证码
            var vercode = Common.CommonHelper.CreateVerifyCode(4);

            //将验证码保存至TempData  TempData中使用过一次后即清空
            TempData["code"] = vercode;

            //使用CaptchaGen验证码组件生成图片流
            //1.验证码文字 2.高度 3.宽度 4.字体大小 5.字体扭曲程度
            MemoryStream memoryStream = ImageFactory.GenerateImage(vercode, 60, 110, 25, 7);
            return File(memoryStream, "image/jpeg");
        }
    }
}