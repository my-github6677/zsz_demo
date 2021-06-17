using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using House.Admin.Web.Models;
using House.Common;
using House.IService;

namespace House.Admin.Web.Controllers
{
    public class AdminController : Controller
    {
        public IAdminService adminService { get; set; }
        public ICityService cityService { get; set; }
        public IRoleService roleService { get; set; }

        //查询所有没有软删除的数据
        public ActionResult Index()
        {
            var list = adminService.GetAll();
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            AdminGetModel model = new AdminGetModel();
            var citys = cityService.GetCities().ToList();
            citys.Insert(0, new DTO.CityDTO() { Id = 0, Name = "总部" });
            var roles = roleService.GetAll();
            model.cityDTO = citys.ToArray();
            model.roleDTO = roles;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long Id)
        {
            var adminDTO = adminService.GetById(Id);
            if (adminDTO == null)
            {
                return View("Error", (object)"id指定的操作员不存在");
            }
            var cityDTO = cityService.GetCities().ToList();
            cityDTO.Insert(0, new DTO.CityDTO { Id = 0, Name = "总部" });
            var roleDTO = roleService.GetAll();
            var RoleIds = roleService.GetByAdminUserId(Id);
            AdminUserEditGet adminUserEditGet = new AdminUserEditGet() { adminDTO = adminDTO, cityDTO = cityDTO.ToArray(), roleDTO = roleDTO, RoleIds = RoleIds.Select(m => m.Id).ToArray() };
            return View(adminUserEditGet);
        }

        [HttpPost]
        public ActionResult Edit(AdminUserEditPost model)
        {
            if (ModelState.IsValid)
            {
                var adminDTO = adminService.IsExistsByPhone(model.phone);
                if (model.RoleIds.Length <= 0)
                {
                    return Json(new AjaxResult() { Status = "noroles" });
                }
                else if (adminDTO != null && adminDTO.Id != model.Id)
                {
                    return Json(new AjaxResult() { Status = "exists" });
                }
                else
                {
                    int res = adminService.UpdateAdminUser(model.Id, model.Name, model.newpassword, model.phone, model.email, model.CityId, model.RoleIds);
                    if (res > 0)
                    {
                        return Json(new AjaxResult() { Status = "ok" });
                    }
                    else
                    {
                        return Json(new AjaxResult() { Status = "no" });
                    }
                }
            }
            else
            {
                return Json(new AjaxResult() { Status = "noValid", ErrorMsg = ModelStateExtensions.ExpendErrors(this) });
            }
        }

        [HttpPost]
        public ActionResult Add(AdminPostModel model)
        {
            if (ModelState.IsValid)
            {
                var array = adminService.IsExistsByPhone(model.phone);
                if (array != null)
                {
                    return Json(new AjaxResult() { Status = "exists" });
                }
                else if (model.RoleIds.Length == 0)
                {
                    return Json(new AjaxResult() { Status = "noarray" });
                }
                else
                {
                    var userId = adminService.AddUser(model.Name, model.newpassword, model.phone, model.email, model.CityId);
                    var result = adminService.AddAdminUserRole(userId, model.RoleIds);
                    if (result > 0)
                    {
                        return Json(new AjaxResult() { Status = "ok" });
                    }
                    else
                    {
                        return Json(new AjaxResult() { Status = "no" });
                    }
                }
            }
            else
            {
                return Json(new AjaxResult() { Status = "err", ErrorMsg = ModelStateExtensions.ExpendErrors(this) });
            }            
        }

        public ActionResult IsExists(string phone,long? adminId)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                var adminDTO = adminService.IsExistsByPhone(phone);
                bool isExists = false;
                if (adminId == null) //说明是添加状态
                {
                    isExists = adminDTO == null; //判断adminDTO是否为空，赋值给isExists (true,false)
                }
                else //说明是更新状态 
                {
                    isExists = (adminDTO == null || adminDTO.Id == adminId); //判断条件是否符合，并给isExists赋值 (true,false)
                }
                return Json(new AjaxResult() { Status = isExists ? "noexists" : "exists" });
            }
            else
            {
                return Json(new AjaxResult() { Status = "phone" });
            }
            
        }

        public ActionResult GetByTimeName(FormCollection fc)
        {
            var list = adminService.GetByTimeName(fc["stime"], fc["etime"], fc["username"]);
            return View("Index", list);
        }
    }
}