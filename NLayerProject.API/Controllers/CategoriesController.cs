using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDTO>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDTO categoryDTO)
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDTO));
            return Created(string.Empty, _mapper.Map<CategoryDTO>(newCategory));
        }

        [HttpPut]
        public IActionResult Update(CategoryDTO categoryDTO)
        {
            var updateCategory = _categoryService.Update(_mapper.Map<Category>(categoryDTO));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);
            return NoContent();
        }
    }
}
