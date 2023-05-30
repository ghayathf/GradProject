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
        public Gpuser UploadImage()
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var fullpath = Path.Combine("C:\\neqatcom_Angular\\src\\assets\\HomeAssets\\images", fileName);

            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            Gpuser item = new Gpuser();
            item.Userimage = fileName;
            return item;
        }
    }
}
