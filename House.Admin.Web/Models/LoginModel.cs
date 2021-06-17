using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="手机号码不能为空！")]
        [Phone(ErrorMessage ="手机号码格式不正确！")]
        public string phoneNum { get; set; }

        [Required(ErrorMessage ="密码不能为空！")]
        public string Pwd { get; set; }

        [Required(ErrorMessage ="验证码不能为空！")]
        [StringLength(5,MinimumLength =5,ErrorMessage ="验证长度必须是5位！")]
        public string VerCode { get; set; }
    }
}