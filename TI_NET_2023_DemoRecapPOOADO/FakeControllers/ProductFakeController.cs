using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.BLL.Services;
using TI_NET_2023_DemoRecapPOOADO.Domain.DTOs;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;
using TI_NET_2023_DemoRecapPOOADO.Domain.Mappers;

namespace TI_NET_2023_DemoRecapPOOADO.FakeControllers
{
    public class ProductFakeController
    {
        private readonly ProductService _productService;

        public ProductFakeController()
        {
            _productService = new ProductService();
        }

        public Product? Add(ProductFormDTO product)
        {
            try
            { 
                return _productService.Add(product.ToEntity());
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<ProductShortDTO> GetAll()
        {
            //IEnumerable<Product> products = _productService.GetAll();
            //List<ProductShortDTO> productShorts = new List<ProductShortDTO>();
            //foreach (Product product in products)
            //{
            //    productShorts.Add(product.toShortDTO());
            //}
            //return productShorts;

            return _productService.GetAll().Select(p => p.toShortDTO()).ToList();
        }
    }
}
