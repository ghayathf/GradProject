using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            this.contactUsRepository = contactUsRepository;
        }

        public void CreateContactUs(Gpcontactu contact)
        {
            contactUsRepository.CreateContactUs(contact);
        }

        public void DeleteContactUs(int id)
        {
            contactUsRepository.DeleteContactUs(id);
        }

        public List<Gpcontactu> GetAllContactUs()
        {
            return contactUsRepository.GetAllContactUs();
        }

        public Gpcontactu GetContactUsByID(int id)
        {
            return contactUsRepository.GetContactUsByID(id);
        }

        public void UpdateContactUs(Gpcontactu contact)
        {
            contactUsRepository.UpdateContactUs(contact);
        }
    }
}
