using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;

namespace TheNeqatcomApp.Infra.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly IDBContext _dbcontext;

        public ContactUsRepository(IDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public void CreateContactUs(Gpcontactu contact)
        {
            string query = "INSERT INTO GPCONTACTUS (FirstNamee, LastNamee, Emaill, PhoneNumber, Message) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Message)";
            var parameters = new
            {
                FirstName = contact.Firstnamee,
                LastName = contact.Lastnamee,
                Email = contact.Emaill,
                PhoneNumber = contact.Phonenumber,
                Message = contact.Message
            };

            _dbcontext.Connection.Execute(query, parameters);
        }

        public void DeleteContactUs(int id)
        {
            string query = "DELETE FROM GPCONTACTUS WHERE ContactID = @ContactId";
            var parameters = new
            {
                ContactId = id
            };

            _dbcontext.Connection.Execute(query, parameters);
        }

        public List<Gpcontactu> GetAllContactUs()
        {
            string query = "SELECT * FROM GPCONTACTUS";
            IEnumerable<Gpcontactu> result = _dbcontext.Connection.Query<Gpcontactu>(query);

            return result.ToList();
        }


        public Gpcontactu GetContactUsByID(int id)
        {
            string query = "SELECT * FROM GPCONTACTUS WHERE ContactID = @Id";
            var parameters = new { Id = id };

            IEnumerable<Gpcontactu> result = _dbcontext.Connection.Query<Gpcontactu>(query, parameters);

            return result.FirstOrDefault();
        }

        public void UpdateContactUs(Gpcontactu contact)
        {
            string query = "UPDATE GPCONTACTUS SET FirstNamee = @FirstName, LastNamee = @LastName, Emaill = @Email, PhoneNumber = @PhoneNumber WHERE ContactID = @ContactId";
            var parameters = new
            {
                FirstName = contact.Firstnamee,
                LastName = contact.Lastnamee,
                Email = contact.Emaill,
                PhoneNumber = contact.Phonenumber,
                ContactId = contact.Contactid
            };

            _dbcontext.Connection.Execute(query, parameters);
        }
    }
}
