using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, CategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto));
        }
        // api/categories/GetSingleCategoryByIdWithProducts/2
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {

            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));

        }
    }
}
