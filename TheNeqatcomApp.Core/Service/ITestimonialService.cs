using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.Service
{
    public interface ITestimonialService
    {
        List<TestimonalUser> GetAllTestimonial();
        Gptestimonial GetTestimonialById(int id);
        List<TestimonalUser> GetTestimonialHome();
        List<TestimonalUser> GetTestimonialAccepted();
        void CreateHomeTestimonial(Gptestimonial finalTestimonial);
        void UpdateTestimonial(Gptestimonial finalTestimonial);
        void DeleteTestimonial(int id);
    }
}
