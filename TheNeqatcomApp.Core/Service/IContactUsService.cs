using TheNeqatcomApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.Service
{
    public interface IContactUsService
    {
        List<Gpcontactu> GetAllContactUs();
        Gpcontactu GetContactUsByID(int id);
        void CreateContactUs(Gpcontactu contact);
        void UpdateContactUs(Gpcontactu contact);
        void DeleteContactUs(int id);
    }
}
