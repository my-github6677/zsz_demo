using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class RoleAddModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "角色名称不能为空")]
        [StringLength(16,MinimumLength =2,ErrorMessage = "角色名称长度必须2~16位")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请选择角色权限")]
        public long[] PermissionIds { get; set; }
    }
}