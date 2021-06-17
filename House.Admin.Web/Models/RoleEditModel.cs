using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using House.DTO;

namespace House.Admin.Web.Models
{
    public class RoleEditModel
    {
        public RoleDTO roleDTO { get; set; }

        public PermissionsDTO[] permissionExists { get; set; }

        public PermissionsDTO[] permissionsAll { get; set; }
    }
}