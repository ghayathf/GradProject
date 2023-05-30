using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
namespace TheNeqatcomApp.Core.Repository
{
    public interface ITestimonialRepository
    {
        List<TestimonalUser> GetAllTestimonial();
        List<TestimonalUser> GetTestimonialHome();
        List<TestimonalUser> GetTestimonialAccepted();
        Gptestimonial GetTestimonialById(int id);
        void CreateHomeTestimonial(Gptestimonial finalTestimonial);
        void UpdateTestimonial(Gptestimonial finalTestimonial);
        void DeleteTestimonial(int id);
    }
}
