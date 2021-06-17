using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using House.IService;
using House.Common;
using House.Admin.Web.Models;

namespace House.Admin.Web.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService iPermission { get; set; }

        /// <summary>
        /// 查询所有没有软删除的权限数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetList()
        {
            var list = iPermission.GetAll();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddPermissions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPermissions(PermissionModels model)
        {
            if (ModelState.IsValid)
            {
                var id = iPermission.AddPermission(model.PermissionDes, model.PermissionName);
                if (id == 0)
                {
                    return Json(new AjaxResult() { Status = "0" });
                }
                else
                {
                    return Json(new AjaxResult() { Status = "ok" });
                }
            }
            else
            {
                string errMsg = ModelStateExtensions.ExpendErrors(this);

                return Json(new AjaxResult() { Status = "no", Data = errMsg });
            }
        }

        //传统方式提交BeginForm
        [HttpPost]
        public ActionResult GetList(FormCollection fc)
        {
            string name = fc["qx"].ToString();
            var list = iPermission.GetAllByStr(name);
            return View(list);
        }

        public ActionResult PlDelete(long[] SelChecks)
        {
            for (int i = 0; i < SelChecks.Length; i++)
            {
                iPermission.DeleteData(SelChecks[i]);
            }
            return Json(new AjaxResult() { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            iPermission.DeleteData(id);
            return Json(new AjaxResult() { Status = "ok" });
        }
    }
}