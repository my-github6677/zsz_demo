using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class AdminUserEditPost
    {
        [Required(ErrorMessage = "管理员名不能为空！")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "管理员名称长度必须为2~16位")]
        public string Name { get; set; }

        public string newpassword { get; set; }

        public string newpassword2 { get; set; }

        [Required(ErrorMessage = "手机号码不能为空！")]
        [Phone(ErrorMessage = "手机号码格式不正确！")]
        public string phone { get; set; }

        [Required(ErrorMessage = "邮箱地址不能为空！")]
        [EmailAddress(ErrorMessage = "邮箱地址格式不正确！")]
        public string email { get; set; }
        public long[] RoleIds { get; set; }
        public long? CityId { get; set; }

        public long Id { get; set; }
    }
}