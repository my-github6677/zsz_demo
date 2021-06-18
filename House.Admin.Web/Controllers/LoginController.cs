using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using House.Admin.Web.Models;
using House.Common;
using House.IService;

namespace House.Admin.Web.Controllers{
    
    public class LoginController : Controller
    {
        public ILoginService loginService { get; set; }
        public IAdminService adminService { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LoginID"] != null)
            {
                Session["LoginID"] = null;
                Session.Abandon();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //1.判断验证码是否一致
                if (loginModel.VerCode != TempData["code"].ToString())
                {
                    return Json(new AjaxResult() { Status = "error", ErrorMsg = "验证码不一致" });
                }
                //2.判断用户名密码
                var result = loginService.GetByPhoneAndPwd(loginModel.phoneNum, loginModel.Pwd);
                if (!result)
                {
                    return Json(new AjaxResult() { Status = "error", ErrorMsg = "手机号码或密码错误" });
                }
                Session["LoginID"] = adminService.IsExistsByPhone(loginModel.phoneNum).Id;
                return Json(new AjaxResult() { Status = "yes" });
            }
            else 
            {
                return Json(new AjaxResult() { Status = "no", ErrorMsg = ModelStateExtensions.ExpendErrors(this) });
            }
        }

        public ActionResult GetVerCode()
        {
            //获取n个随机数生成验证码
            var vercode = Common.CommonHelper.CreateVerifyCode(5);

            //将验证码保存至TempData  TempData中使用过一次后即清空
            TempData["code"] = vercode;

            //使用CaptchaGen验证码组件生成图片流
            //1.验证码文字 2.高度 3.宽度 4.字体大小 5.字体扭曲程度
            MemoryStream memoryStream = ImageFactory.GenerateImage(vercode, 60, 110, 20, 10);
            return File(memoryStream, "image/jpeg");
        }

        public ActionResult IndexView()
        {
            return View();
        }
    }
}