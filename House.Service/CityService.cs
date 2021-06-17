using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.DTO;
using House.IService;

namespace House.Service
{
    public class CityService : ICityService
    {
        public CityDTO[] GetCities()
        {
            using (HouseContext db = new HouseContext())
            {
                BaseService<CityEntity> baseService = new BaseService<CityEntity>(db);
                return baseService.GetAll().ToList().Select(m => ToDTO(m)).ToArray();
                //Select就是筛选出一个新的模型  这个地方一定得加ToList()，不然sql语句不会执行（因为是延迟加载，用到的时候才执行）
            }
        }

        public CityDTO ToDTO(CityEntity cityEntity)
        {
            CityDTO city = new CityDTO();
            city.Id = cityEntity.Id;
            city.Name = cityEntity.Name;
            city.CreateDateTime = cityEntity.CreateDateTime;

            return city;
        }
    }
}
