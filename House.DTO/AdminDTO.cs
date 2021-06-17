using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.DTO
{
    public class AdminDTO: BaseDTO
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }

        public long? CityId { get; set; }

        public int LoginErrorTimes { get; set; }

    }
}
