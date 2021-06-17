using House.Admin.Web.Models;
using House.Common;
using House.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace House.Admin.Web.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public IPermissionService permissionService { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            var roledto = roleService.GetAll();
            return View(roledto);
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            var roledto = roleService.GetByName(fc["js"]);
            return View(roledto);
        }

        [HttpGet]
        public ActionResult Edit(long Id)
        {
            var role = roleService.GetById(Id);
            var perExists = permissionService.GetPermissionByRoleId(Id);
            var perAll = permissionService.GetAll();
            RoleEditModel model = new RoleEditModel { roleDTO = role, permissionExists = perExists, permissionsAll = perAll };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleAddModel model)
        {
            if (ModelState.IsValid)
            {
                int result = roleService.EditRolePermission(model.Id, model.Name, model.PermissionIds);
                if (result > 0)
                {
                    return Json(new AjaxResult() { Status = "ok" });
                }
                else
                {
                    return Json(new AjaxResult() { Status = "no" });
                }
            }
            else
            {
                return Json(new AjaxResult() { Status = "err", ErrorMsg = ModelStateExtensions.ExpendErrors(this) });
            }
            
        }

        [HttpGet]
        public ActionResult Add()
        {
            var list = permissionService.GetAll();
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            if (ModelState.IsValid)
            {
                var roleID = roleService.AddRole(model.Name);
                if (roleID > 0)
                {
                    int result = roleService.AddRolePermission(roleID, model.PermissionIds);
                    if (result > 0)
                    {
                        return Json(new AjaxResult() { Status = "ok" });
                    }
                    else
                    {
                        return Json(new AjaxResult() { Status = "no" });
                    }
                }
                else
                {
                    return Json(new AjaxResult() { Status = "no" });
                }
            }
            else
            {
                return Json(new AjaxResult() { Status = "f", ErrorMsg = ModelStateExtensions.ExpendErrors(this) });
            }
        }
    }
}