using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryservice;
        public static IWebHostEnvironment _environment;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _categoryservice = categoryService;
            _environment = environment;
        }
        [HttpGet("getall")]
        //[Authorize()]
        public IActionResult GetCategoryList()
        {
            var result = _categoryservice.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getCategory")]
        [Authorize()]
        public IActionResult GetById(int Id)
        {
            var result = _categoryservice.GetByCategory(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addCategory")]
        [Authorize()]
        public IActionResult AddCategory([FromBody] Category categories)
        {
            //model.CreatedDate = DateTime.Now;
            var result = _categoryservice.Add(categories);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("updateCategory")]
        [Authorize()]
        public IActionResult UpdateCategory([FromBody] Category categories)
        {
            //category.UpdatedDate = DateTime.Now;
            var result = _categoryservice.Update(categories);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("deleteCategory")]
        [Authorize()]
        public IActionResult DeleteCategory(Category categories)
        {
            var result = _categoryservice.Delete(categories);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("delCatByCatId")]
        [Authorize()]
        public IActionResult DeleteCatById(int id)
        {
            var cat = _categoryservice.GetByCategory(id).Data;
            if (cat != null)
            {
                var result = _categoryservice.Delete(cat);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        //[HttpPost("UpImg")]
        //[Authorize()]
        //public bool UploadImg()
        //{
        //    var httpRequest = HttpContext.Request;
        //    foreach (var file in httpRequest.Form.Files)
        //    {
        //        var postedFile = file;
        //        if (!Directory.Exists(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\"))
        //        {
        //            Directory.CreateDirectory(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\");
        //        }
        //        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\" + postedFile.FileName))
        //        {
        //            postedFile.CopyTo(fileStream);
        //            fileStream.Flush();
        //        }
        //    }
        //    return true;
        //}
        //[HttpPost("deleteCategoryPhoto")]
        //[Authorize()]
        //public bool DeleteCategoryPhoto(int id)
        //{
        //    var categoryUrl = _categoryservice.GetByCategory(id).Data;
        //    if (categoryUrl!=null)
        //    {
        //        var data = categoryUrl.ImgUrl.Split(',');
        //        try
        //        {
        //            foreach (var item in data)
        //            {
        //                var file = _environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\" + item;
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
