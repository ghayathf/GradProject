using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;

namespace TheNeqatcomApp.Infra.Repository
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly IDBContext _dbcontext;

        public TestimonialRepository(IDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public void CreateHomeTestimonial(Gptestimonial finalTestimonial)
        {
            var p = new DynamicParameters();
            p.Add("msg", finalTestimonial.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("status", finalTestimonial.Testimonialstatus, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("USERID", finalTestimonial.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = "INSERT INTO GPTestimonial(message, testimonialstatus, userid) VALUES (@msg, @status, @USERID)";

            var result = _dbcontext.Connection.Execute(query, p);
        }


        public void DeleteTestimonial(int id)
        {
            var parameters = new
            {
                id
            };

            var result = _dbcontext.Connection.Execute("Delete from GPTestimonial where testimonialid = @id",parameters);
        }




        public List<TestimonalUser> GetAllTestimonial()
        {
            string query = "SELECT *  FROM GPTestimonial INNER JOIN GPUSER ON GPTestimonial.userid = gpuser.userid WHERE GPTestimonial.testimonialstatus = 0";

            IEnumerable<TestimonalUser> result = _dbcontext.Connection.Query<TestimonalUser>(query);
            return result.ToList();
        }

        public List<TestimonalUser> GetTestimonialAccepted()
        {
            string query = @"
        WITH RandomizedTestimonials AS (
            SELECT TOP 3
                GPTestimonial.message,
                GPTestimonial.testimonialid,
                GPTestimonial.userid AS testimonial_userid,
                GPTestimonial.testimonialstatus,
                GPUSER.userid AS Userid,
                GPUSER.firstname,
                GPUSER.lastname,
                GPUSER.email
            FROM GPTestimonial
            INNER JOIN GPUSER ON GPTestimonial.userid = GPUSER.userid
            WHERE GPTestimonial.testimonialstatus = 1
            ORDER BY NEWID()
        )
        SELECT * FROM RandomizedTestimonials";

            IEnumerable<TestimonalUser> result = _dbcontext.Connection.Query<TestimonalUser>(query);
            return result.ToList();
        }







        public Gptestimonial GetTestimonialById(int id)
        {
            var p = new DynamicParameters();
            p.Add("idd", id, DbType.Int32, ParameterDirection.Input);

            IEnumerable<Gptestimonial> result = _dbcontext.Connection.Query<Gptestimonial>("GP_Testimonial_Package.GetTestimonialById", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public List<TestimonalUser> GetTestimonialHome()
        {
            string query = "SELECT TOP 3 GPTestimonial.*, GPUSER.userid AS gpuserid,GPUSER.Role, GPUSER.username,gpuser.FirstName,gpuser.LastName,GPUser.userImage INTO #RandomizedTestimonialssss FROM GPTestimonial INNER JOIN GPUSER ON GPTestimonial.userid = gpuser.userid WHERE GPTestimonial.testimonialstatus = 1 ORDER BY NEWID();SELECT* FROM #RandomizedTestimonialssss";

            IEnumerable<TestimonalUser> result = _dbcontext.Connection.Query<TestimonalUser>(query);
            return result.ToList();
        }






        public void UpdateTestimonial(Gptestimonial finalTestimonial)
        {
            var p = new DynamicParameters();
            p.Add("idd", finalTestimonial.Testimonialid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("msg", finalTestimonial.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("status", finalTestimonial.Testimonialstatus, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("USERID", finalTestimonial.Userid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            string query = "UPDATE GPTestimonial SET message = @msg, testimonialstatus = @status WHERE testimonialid = @idd";

            var result = _dbcontext.Connection.Execute(query, p);
        }

    }
}
