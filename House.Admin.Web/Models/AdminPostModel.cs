using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class AdminPostModel
    {
        [Required(ErrorMessage ="管理员名不能为空！")]
        [StringLength(16,MinimumLength =2,ErrorMessage ="管理员名称长度必须为2~16位")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码不能为空！")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度必须为2~20位")]
        public string newpassword { get; set; }

        [Required(ErrorMessage = "确认密码不能为空！")]
        [Compare("newpassword",ErrorMessage ="两次密码不一致！")]
        public string newpassword2 { get; set; }

        [Required(ErrorMessage = "手机号码不能为空！")]
        [Phone(ErrorMessage = "手机号码格式不正确！")]
        public string phone { get; set; }

        [Required(ErrorMessage = "邮箱地址不能为空！")]
        [EmailAddress(ErrorMessage = "邮箱地址格式不正确！")]
        public string email { get; set; }
        public long[] RoleIds { get; set; }
        public long? CityId { get; set; }

    }
}