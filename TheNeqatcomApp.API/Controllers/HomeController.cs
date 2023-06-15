using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;

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
        public Gphomepage UploadImage()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            var fullPath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
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
