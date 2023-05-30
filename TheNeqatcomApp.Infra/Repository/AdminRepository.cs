using Dapper;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;

namespace TheNeqatcomApp.Infra.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDBContext dbContext;
        public AdminRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public AdminRepository() { }
        public List<ActorCounterDTO> ActorCounter()
        {
            throw new NotImplementedException();
        }

        public AdminStatisticsLoanee AdminStatisticsLoanee()
        {
            throw new NotImplementedException();
        }

        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            throw new NotImplementedException();
        }

        public List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender()
        {
            throw new NotImplementedException();
        }

        public CategoriesStatistics categoriesStatistics()
        {
            throw new NotImplementedException();
        }

        public ComplaintsStatistics complaintsStatistics()
        {
            throw new NotImplementedException();
        }

        public void deleteComplaint(int cid)
        {
            throw new NotImplementedException();
        }

        public List<Gpcommercialregister> GetGpcommercialregisters()
        {
            string query = @"
        SELECT *
        FROM GPCommercialRegister;
    ";

            IEnumerable<Gpcommercialregister> result = dbContext.Connection.Query<Gpcommercialregister>(query);

            return result.ToList();
        }

        public List<LenderComplaints> GetLenderStoresComplaints()
        {
            throw new NotImplementedException();
        }

        public void HandleRegistarction(int IDD)
        {
            int v_count;

            // Check if the commercial register exists in the commercialregister table
            string countQuery = @"
        SELECT COUNT(*) 
        FROM gpcommercialregister 
        WHERE commercialcode = (
            SELECT commercialregister 
            FROM GPLenderstore 
            WHERE LenderID = @IDD
        ) AND commercialcode IS NOT NULL";

            v_count = dbContext.Connection.ExecuteScalar<int>(countQuery, new { IDD });

            if (v_count > 0)
            {
                // If the commercial register exists, update the register status in the lenderstore table
                string updateQuery = @"
            UPDATE GPLenderstore 
            SET RegisterStatus = 1 
            WHERE LenderID = @IDD";

                dbContext.Connection.Execute(updateQuery, new { IDD });
            }
        }



        public LenderAdminStatistics lenderAdminStatistics()
        {
            throw new NotImplementedException();
        }

        public List<LoaneeCreditScores> loaneeCreditScores()
        {
            throw new NotImplementedException();
        }

        public void ManageLenderComplaints(int loaid, int CID)
        {
            throw new NotImplementedException();
        }
    }
}
