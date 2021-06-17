using House.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace House.Admin.Web.Models
{
    public class AdminUserEditGet
    {
        public AdminDTO adminDTO { get; set; }
        public CityDTO[] cityDTO { get; set; }
        public RoleDTO[] roleDTO { get; set; }
        public long[] RoleIds { get; set; }
    }
}