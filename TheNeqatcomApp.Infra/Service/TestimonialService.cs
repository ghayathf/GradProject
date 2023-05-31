using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository testimonialRepository;

        public TestimonialService(ITestimonialRepository testimonialRepository)
        {
            this.testimonialRepository = testimonialRepository;
        }
        public void CreateHomeTestimonial(Gptestimonial finalTestimonial)
        {
            testimonialRepository.CreateHomeTestimonial(finalTestimonial);
        }

        public void DeleteTestimonial(int id)
        {
            testimonialRepository.DeleteTestimonial(id);
        }

        public List<TestimonalUser> GetAllTestimonial()
        {
            return testimonialRepository.GetAllTestimonial();
        }

        public List<TestimonalUser> GetTestimonialAccepted()
        {
            return testimonialRepository.GetTestimonialAccepted();
        }

        public Gptestimonial GetTestimonialById(int id)

        {
            return testimonialRepository.GetTestimonialById(id);
        }

        public List<TestimonalUser> GetTestimonialHome()
        {
            return testimonialRepository.GetTestimonialHome();
        }

        public void UpdateTestimonial(Gptestimonial finalTestimonial)
        {
            testimonialRepository.UpdateTestimonial(finalTestimonial);
        }
    }
}
