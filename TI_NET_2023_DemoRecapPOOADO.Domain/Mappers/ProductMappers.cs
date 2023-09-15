using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.DTOs;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;

namespace TI_NET_2023_DemoRecapPOOADO.Domain.Mappers
{
    public static class ProductMappers
    {
        public static Product ToEntity(this ProductFormDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Quantity = dto.Quantity,
                Category = dto.Category,
                Price = dto.Price
            };
        }
    }
}
