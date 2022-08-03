using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo repository;
        private readonly IMapper mapper;

        public ProductController(IProductRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
        {
            Console.WriteLine("--> Getting Products...");

            var productItem = repository.GetAllProducts();

            return Ok(mapper.Map<IEnumerable<ProductReadDto>>(productItem));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            Console.WriteLine("--> Getting Product By ID...");

            var productItem = repository.GetProductById(id);

            if (productItem != null)
            {
                return Ok(mapper.Map<ProductReadDto>(productItem));
            }

            return NotFound();
        }

        [HttpGet("category/{category}", Name = "GetProductsByCategory")]
        public ActionResult<ProductReadDto> GetProductsByCategory(string category)
        {
            Console.WriteLine("--> Getting Product By Category...");

            var productItem = repository.GetProductsByCategory(category);

            if (productItem != null)
            {
                return Ok(mapper.Map<IEnumerable<ProductReadDto>>(productItem));
            }

            return NotFound();
        }


        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var productModel = mapper.Map<Product>(productCreateDto);
            repository.CreateProduct(productModel);
            repository.SaveChanges();
            Console.WriteLine("--> Creating Product...");

            var productItem = mapper.Map<ProductReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { productItem.Id }, productItem);
        }
    }
}
