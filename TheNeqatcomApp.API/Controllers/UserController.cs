using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Auth([FromBody] Gpuser login)
        {
            var token = userService.Auth(login);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
        [HttpGet]
        [Route("GetAllFollower/{lendId}")]
        public List<Followers> GetAllGpfollower(int lendId)
        {
            return userService.GetAllGpfollower(lendId);
        }
        [HttpPost]
        [Route("addfollower/{lendId}/{loaneId}")]
        public void addfollower(int lendId, int loaneId)
        {
            userService.addfollower(lendId, loaneId);
        }
        [HttpDelete]
        [Route("DeleteFollower/{lendId}/{loaneId}")]
        public void DeleteFollower(int lendId, int loaneId)
        {
            userService.DeleteFollower(lendId, loaneId);
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public List<Gpuser> GetAllUsers()
        {
            return userService.GetAllUsers();
        }
        [HttpPut]
        [Route("UpdatePassword")]
        public void updatePassword(Gpuser gpuser)
        {
            userService.updatePassword(gpuser);
        }
        [HttpGet]
        [Route("GetUserById/{id}")]
        public Gpuser GetUserById(int id)
        {
            return userService.GetUserById(id);
        }
        [HttpPost]
        [Route("CreateUser")]
        public void CreateUser(Gpuser user)
        {
            userService.CreateUser(user);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public void UpdateUser(Gpuser user)
        {
            userService.UpdateUser(user);
        }
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            userService.DeleteUser(id);
        }
        [Route("UploadImage")]
        [HttpPost]
        public async Task<Gpuser> UploadImage()
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

            // Set the content type based on the file extension
            string contentType;
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".jfif":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".bmp":
                    contentType = "image/bmp";
                    break;
                case ".tiff":
                    contentType = "image/tiff";
                    break;
                default:
                    contentType = "application/octet-stream"; // Set a default content type if the file type is not recognized
                    break;
            }
            blockBlob.Properties.ContentType = contentType;

            // Upload the image file to Azure Blob Storage
            using (var stream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }

            Gpuser item = new Gpuser();
            item.Userimage = blockBlob.Uri.ToString(); // Store the image URL instead of the blob name
            return item;
        }


    }
}
