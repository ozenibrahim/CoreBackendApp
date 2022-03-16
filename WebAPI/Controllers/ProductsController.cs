using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public static IWebHostEnvironment _environment;
        public ProductsController(IProductService productService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _environment = environment;
        }
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        [Authorize(Roles ="Product.List")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Get List By Category Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("getlistbycategory")]
        [Authorize()]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
        [Authorize()]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        [Authorize()]
        public IActionResult Add([FromBody]Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        [Authorize()]

        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        [Authorize()]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("deleteById")]
        [Authorize()]
        public IActionResult DeleteById(int id)
        {
            var product = _productService.GetById(id).Data;
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //[HttpPost("UpImg")]
        //[Authorize()]
        //public bool UploadImg()
        //{
        //    var httpRequest = HttpContext.Request;
        //    foreach (var file in httpRequest.Form.Files)
        //    {
        //        var postedFile = file;
        //        if (!Directory.Exists(_environment.WebRootPath + "\\Content\\Upload\\Img\\Product\\"))
        //        {
        //            Directory.CreateDirectory(_environment.WebRootPath + "\\Content\\Upload\\Img\\Product\\");
        //        }
        //        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\Upload\\Img\\Product\\" + postedFile.FileName))
        //        {
        //            postedFile.CopyTo(fileStream);
        //            fileStream.Flush();
        //        }
        //    }
        //    return true;
        //}
        //[HttpPost("deleteProductPhoto")]
        //[Authorize()]
        //public bool DeleteProductPhoto(int id)
        //{
        //    var productUrl = _productService.GetById(id).Data;
        //    if (productUrl!=null)
        //    {
        //        var data = productUrl.Picture.Split(',');
        //        try
        //        {
        //            foreach (var item in data)
        //            {
        //                var file = _environment.WebRootPath + "\\Content\\Upload\\Img\\Product\\" + item;
        //                System.IO.File.Delete(file);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
