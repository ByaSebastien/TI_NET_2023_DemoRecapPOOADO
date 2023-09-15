﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;

namespace TI_NET_2023_DemoRecapPOOADO.DAL.Repositories
{
    public interface IProductRepository
    {
        Product Create(Product product);
        Product ReadOne(int id);
        IEnumerable<Product> ReadAll();
        bool Update(int id, Product product);
        bool Delete(int id);
    }
}
