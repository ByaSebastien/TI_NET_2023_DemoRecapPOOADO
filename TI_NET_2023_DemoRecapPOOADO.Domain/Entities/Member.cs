using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.enums;

namespace TI_NET_2023_DemoRecapPOOADO.Domain.Entities
{
    public class Member
    {
        public int? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Roles { get; set; }
    }
}
