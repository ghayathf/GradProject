using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Service;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using System.Threading.Tasks;

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
        public async Task <Gpcategory>UploadImage()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // Retrieve the connection string for your Azure Blob Storage
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=neqatcomstorage;AccountKey=CAx4ethtWMCMon9qcXk/ZetYTUtYyzhlWmAq+fj5sGXoUT5cihFTdH8eLKjQqCsDDdwWg7gB4D2B+ASt0oVPqQ==;EndpointSuffix=core.windows.net";

            // Create a CloudStorageAccount object using the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            // Create a CloudBlobClient object to interact with Blob storage
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Create a container reference (replace 'images-container' with your desired container name)
            string containerName = "images-container";
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't exist
            await container.CreateIfNotExistsAsync();

            // Set the public access level of the container to allow public read access to the images
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Create a CloudBlockBlob object to represent the uploaded image
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the image file to Azure Blob Storage
            using (var stream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }



            Gpcategory item = new Gpcategory();
            item.Categoryimage = fileName;
            return item;
        }
    }
}
