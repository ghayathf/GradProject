using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {

        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService _testimonialService)
        {
            this._testimonialService = _testimonialService;
        }

        [HttpGet]
        [Route("GetTestimonialById/{id}")]
        public Gptestimonial GetTestimonialById(int id)
        {
            return _testimonialService.GetTestimonialById(id);
        }
        [HttpPost]
        [Route("CreateTestimonial")]
        public void CreateHomeTestimonial(Gptestimonial finalTestimonial)
        {
            _testimonialService.CreateHomeTestimonial(finalTestimonial);
        }
        [HttpPut]
        [Route("UpdateTestimonial")]
        public void UpdateTestimonial(Gptestimonial finalTestimonial)
        {
            _testimonialService.UpdateTestimonial(finalTestimonial);
        }
        [HttpDelete]
        [Route("DeleteTestimonial/{id}")]
        public void DeleteTestimonial(int id)
        {
            _testimonialService.DeleteTestimonial(id);
        }

        [HttpGet]
        [Route("GetAllTestimonials")]
        public List<TestimonalUser> GetAllTestimonial()
        {
            return _testimonialService.GetAllTestimonial();
        }
        [HttpGet]
        [Route("AccpetedTestimonial")]
        public List<TestimonalUser> GetTestimonialHome()
        {
            return _testimonialService.GetTestimonialHome();
        }

        [HttpGet]
        [Route("GetTestimonialAccepted")]
        public List<TestimonalUser> GetTestimonialAccepted()
        {
            return _testimonialService.GetTestimonialAccepted();
        }

    }
}
