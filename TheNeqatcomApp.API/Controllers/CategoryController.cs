using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetAllCategories")]
        public List<Gpcategory> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }
        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public Gpcategory GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }
        [HttpPost]
        [Route("CreateCategory")]
        public void CreateCategory(Gpcategory gpcategory)
        {
            _categoryService.CreateCategory(gpcategory);
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public void UpdateCategory(Gpcategory gpcategory)
        {
            _categoryService.UpdateCategory(gpcategory);
        }
        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public void DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
        }
        [Route("UploadImage")]
        [HttpPost]
        public Gpcategory UploadImage()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var fullpath = Path.Combine("C:\\neqatcom_Angular\\src\\assets\\HomeAssets\\images", fileName);

            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            Gpcategory item = new Gpcategory();
            item.Categoryimage = fileName;
            return item;
        }
    }
}
