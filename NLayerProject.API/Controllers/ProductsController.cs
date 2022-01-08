using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.API.Filters;
using NLayerProject.Core.Entities;
using NLayerProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryById(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDTO>(product));
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDTO));
            return Created(string.Empty, _mapper.Map<ProductDTO>(newProduct));
        }

        [HttpPut]
        public IActionResult Update(ProductDTO productDTO)
        {
            var updateProduct = _productService.Update(_mapper.Map<Product>(productDTO));
            return NoContent();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);
            return NoContent();
        }
    }
}
