using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Services;
using System.IO.Compression;
using System.Text.Json;
using System.Text;

namespace MyWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ProductService _productService;

        public ValuesController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetProducts();
            var jsonString = JsonSerializer.Serialize(products);
            var compressedData = CompressString(jsonString);
            return File(compressedData, "application/json", "products.json.gz");
        }

        private byte[] CompressString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var ms = new MemoryStream())
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Compress))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                return ms.ToArray();
            }
        }
    }
}
