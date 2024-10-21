using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.DTOs;
using MyWebApiProject.Models;
using MyWebApiProject.Services;

namespace MyWebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<Record>>> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetPaginatedProducts(pageNumber, pageSize);
            var totalCount = await _productRepository.GetTotalProductsCount();

            var response = new PaginatedResponse<Record>
            {
                Items = products,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            return Ok(response);
        }
    }
}
