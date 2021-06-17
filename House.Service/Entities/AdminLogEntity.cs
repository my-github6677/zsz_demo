﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class AdminLogEntity :BaseEntity
    {
        public long AdminUserId { get; set; }
        public virtual AdminUserEntity AdminUser { get; set; }
        public string Message { get; set; }
    }
}
