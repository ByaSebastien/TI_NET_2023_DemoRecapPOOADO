using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;
using TI_NET_2023_DemoRecapPOOADO.FakeControllers;
using TI_NET_2023_DemoRecapPOOADO.Domain.enums;
using TI_NET_2023_DemoRecapPOOADO.Domain.DTOs;

ProductFakeController productFakeController = new ProductFakeController();

ProductFormDTO product = new ProductFormDTO()
{
    Name = "Stolichnaya",
    Category = Category.Vodka,
    Description = null,
    Quantity = 10,
    Price = 5
};

Product? result = productFakeController.Add(product);

if(result is not null)
{
    Console.WriteLine($"{result.Id} : {result.Name}");
}