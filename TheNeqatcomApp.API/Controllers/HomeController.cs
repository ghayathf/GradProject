using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using System.Threading.Tasks;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        [Route("GetAllHomeInformatio")]
        public List<Gphomepage> GetAllHomeInformation()
        {
            return _homeService.GetAllHomeInformation();
        }

        [HttpGet]
        [Route("GetHomeInformationById/{id}")]
        public Gphomepage GetHomeInformationById(int id)
        {
            return _homeService.GetHomeInformationById(id);
        }
        [HttpPost]
        [Route("CreateHomePage")]
        public void CreateHomeInformation(Gphomepage finalHomepage)
        {
            _homeService.CreateHomeInformation(finalHomepage);
        }
        [HttpPut]
        [Route("UpdateHomePage")]
        public void UpdateHomeInformation(Gphomepage finalHomepage)
        {
            _homeService.UpdateHomeInformation(finalHomepage);
        }

        [HttpDelete]
        [Route("DeleteHomePage/{id}")]
        public void DeleteHomeInformation(int id)
        {
            _homeService.DeleteHomeInformation(id);
        }
        [Route("UploadImage")]
        [HttpPost]
        public async Task<Gphomepage> UploadImage()
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

            Gphomepage item = new Gphomepage();
            item.Logo = fileName;
            return item;
        }
        [HttpGet]
        [Route("getTableLengths")]
        public List<Lengths> getTableLengths()
        {
            return _homeService.getTableLength();
        }
        [HttpGet]
        [Route("GetLoaneestoRemind")]
        public List<LoaneeReminder> GetLoaneestoRemind()
        {
            return _homeService.GetLoaneestoRemind();
        }
        [HttpPut]
        [Route("UpdateBeforeReminder")]
        public void UpdateBeforeReminder()
        {
            _homeService.UpdateBeforeReminder();
        }
        [HttpGet]
        [Route("GetLoaneesInPayDaytoRemind")]
        public List<LoaneeReminder> GetLoaneesInPayDaytoRemind()
        {
            return _homeService.GetLoaneesInPayDaytoRemind();
        }
        [HttpPut]
        [Route("UpdateInPayDateReminder")]
        public void UpdateInPayDateReminder()
        {
            _homeService.UpdateInPayDateReminder();
        }
        [HttpGet]
        [Route("GetLoaneeslatePayDaytoRemind")]
        public List<LoaneeReminder> GetLoaneeslatePayDaytoRemind()
        {
            return _homeService.GetLoaneeslatePayDaytoRemind();
        }
        [HttpPut]
        [Route("UpdateLatePayDateReminder")]
        public void UpdateLatePayDateReminder()
        {
            _homeService.UpdateLatePayDateReminder();
        }
        [HttpPost]
        [Route("CalculateCreditScores")]
        public void CalculateCreditScores()
        {
            _homeService.CalculateCreditScores();
        }
        [HttpGet]
        [Route("CreditScoreStatus/{loaneeid}")]
        public List<bool> CreditScoreStatus(int loaneeid)
        {
            return _homeService.CreditScoreStatus(loaneeid);
        }
    }
}
