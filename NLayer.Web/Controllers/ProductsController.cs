using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService services, IProductService productService, IMapper mapper)
        {
            _services = services;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            return View((await _services.GetProductsWithCategory()).Data);
        }


        public async Task<IActionResult> Save()
        {
            var products = await _productService.GetAllAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());

            ViewBag.Products = new SelectList(productsDto, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {


            if (ModelState.IsValid)
            {
                await _services.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var products = await _productService.GetAllAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());

            ViewBag.Products = new SelectList(productsDto, "Id", "Name", productDto.CategoryId);
            return View();



        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _services.GetByIdAsync(id);
            var products = await _productService.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
            ViewBag.Product = new SelectList(productsDto, "Id", "Name", product.Id);
            return View(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _services.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var products = await _productService.GetAllAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());

            ViewBag.Products = new SelectList(productsDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _services.GetByIdAsync(id);
            await _services.RemoveAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
