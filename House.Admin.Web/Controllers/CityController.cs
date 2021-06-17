using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using House.IService;
using House.Service;
using House.DTO;

namespace House.Admin.Web.Controllers
{
    public class CityController : Controller
    {
        public ICityService cityService { get; set; } //声明一个接口对象，方便各个方法调用
        // GET: City
        public ActionResult Index()
        {
            //cityService = new CityService(); //让IOC容器AutoFac帮你创建数据，不需要手动操作
            CityDTO[] cityDTOs = cityService.GetCities();
            return Content(cityDTOs.Length.ToString());
        }
    }
}