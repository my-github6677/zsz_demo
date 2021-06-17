using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using House.Service;

namespace UnitTestHouse
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            log4net.Config.XmlConfigurator.Configure();//初始化log4net
            using (HouseContext db = new HouseContext())
            {
                CityEntity city = new CityEntity();
                city.Name = "苏州";
                db.Cities.Add(city);
                db.SaveChanges();
            }
        }
    }
}
