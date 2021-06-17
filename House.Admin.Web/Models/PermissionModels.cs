using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class PermissionModels
    {
        [Required(ErrorMessage ="权限描述不能为空！")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "权限描述请输入2~16个字符！")]
        public string PermissionDes { get; set; }

        [Required(ErrorMessage ="权限名称不能为空！")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "权限名称请输入2~16个字符！")]
        public string PermissionName { get; set; }
    }
}