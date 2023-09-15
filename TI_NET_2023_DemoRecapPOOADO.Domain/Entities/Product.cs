using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.enums;

namespace TI_NET_2023_DemoRecapPOOADO.Domain.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
