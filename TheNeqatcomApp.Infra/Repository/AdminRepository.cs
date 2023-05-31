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
            var sql = @"
        SELECT Role, COUNT(*) AS Count
        FROM GPUser
        GROUP BY Role
        ORDER BY Role";

            IEnumerable<ActorCounterDTO> loan = dbContext.Connection.Query<ActorCounterDTO>(sql);
            return loan.ToList();
        }

        public AdminStatisticsLoanee AdminStatisticsLoanee()
        {
            var sql = @"
        SELECT
            (SELECT COUNT(*) FROM GPLoanee) AS loaneesCount,
            (SELECT ROUND(AVG(CreditScore)) FROM GPLoanee) AS averageCreditScore,
            (SELECT COUNT(*) FROM GPLoan) AS totalLoansCount
        FROM DUAL";

            IEnumerable<AdminStatisticsLoanee> loan = dbContext.Connection.Query<AdminStatisticsLoanee>(sql);
            return loan.FirstOrDefault();
        }

        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            var sql = @"
        SELECT gploan.*, gploanee.*, gpuser.*, gpmeetings.*
        FROM gploan
        INNER JOIN gploanee ON gploan.loaneeid = gploanee.loaneeid
        INNER JOIN gpuser ON gploanee.LOANEEUSERID = gpuser.userid
        INNER JOIN gpmeetings ON gpmeetings.loanid = gploan.loanid
        WHERE TRUNC(gpmeetings.startdate) + 3 <= TRUNC(SYSDATE)
        AND (gploan.loanstatus = 1 OR gploan.loanstatus = 2)";

            IEnumerable<CancleLoanAuto> loan = dbContext.Connection.Query<CancleLoanAuto>(sql);
            return loan.ToList();
        }

        public List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender()
        {
            var sql = @"
        SELECT gploan.*, gpmeetings.*, gplenderstore.*, gpuser.*
        FROM gploan
        INNER JOIN gpmeetings ON gploan.loanid = gpmeetings.loanid
        INNER JOIN gplenderstore ON gplenderstore.Lenderid = gpmeetings.lenderid
        INNER JOIN gpuser ON gplenderstore.lenderuserid = gpuser.userid
        WHERE TRUNC(gpmeetings.startdate) + 3 <= TRUNC(SYSDATE)
        AND (gploan.loanstatus = 1 OR gploan.loanstatus = 2)";

            IEnumerable<CancleLoanMsgforLender> loan = dbContext.Connection.Query<CancleLoanMsgforLender>(sql);
            return loan.ToList();
        }

        public CategoriesStatistics categoriesStatistics()
        {
            var sql = @"
        SELECT (
            SELECT COUNT(*) FROM GPCategory
        ) AS TotalCategories,
        (
            SELECT COUNT(*) FROM GPOffer
        ) AS TotalOffers,
        (
            SELECT COUNT(*) FROM GPLoan
        ) AS TotalLoans
        FROM DUAL";

            IEnumerable<CategoriesStatistics> loan = dbContext.Connection.Query<CategoriesStatistics>(sql);
            return loan.FirstOrDefault();
        }

        public ComplaintsStatistics complaintsStatistics()
        {
            var sql = @"
        SELECT (
            SELECT COUNT(*) FROM GPCOMPLAINTS WHERE manageStatus = 1
        ) AS LoaneeToLenderCount,
        (
            SELECT COUNT(*) FROM GPCOMPLAINTS WHERE manageStatus = 2
        ) AS LenderToLoaneeCount,
        (
            SELECT COUNT(*) FROM GPCOMPLAINTS WHERE manageStatus = 3
        ) AS SystemToLoaneeCount
        FROM DUAL";

            IEnumerable<ComplaintsStatistics> loan = dbContext.Connection.Query<ComplaintsStatistics>(sql);
            return loan.FirstOrDefault();
        }

        public void deleteComplaint(int cid)
        {
            var sql = "UPDATE GPComplaints SET managestatus = -1 WHERE complaintsid = :CID";
            var p = new { CID = cid };

            dbContext.Connection.Execute(sql, p);
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
            var sql = @"
        SELECT GPComplaints.*, GPLenderStore.lenderID, GPloanee.*, GPUser.*
        FROM GPComplaints 
        LEFT JOIN GPLoanee ON GPComplaints.LOID = GPLoanee.loaneeID
        LEFT JOIN GPLenderStore ON GPComplaints.LEID = GPLenderStore.lenderID
        LEFT JOIN GPUser ON GPLoanee.LoaneeuserID = GPUser.userID
        WHERE GPComplaints.managestatus = 2 OR GPComplaints.managestatus = 3";

            return dbContext.Connection.Query<LenderComplaints>(sql).ToList();
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
            var sql = @"
        SELECT
            (SELECT COUNT(*) FROM GPLenderStore WHERE registerStatus = 1) AS registeredLendersCount,
            (SELECT COUNT(*) FROM GPLenderStore WHERE registerStatus = -1) AS unregisteredLendersCount,
            COUNT(*) AS totalLendersCount
        FROM GPLenderStore";

            return dbContext.Connection.QueryFirstOrDefault<LenderAdminStatistics>(sql);

        }

        public List<LoaneeCreditScores> loaneeCreditScores()
        {
            var sql = @"
        SELECT CreditScore, COUNT(*) AS Count
        FROM GPLoanee
        WHERE CreditScore BETWEEN 1 AND 10
        GROUP BY CreditScore
        ORDER BY CreditScore";

            return dbContext.Connection.Query<LoaneeCreditScores>(sql).ToList();

        }

        public void ManageLenderComplaints(int loaid, int CID)
        {
            var sql = @"
        UPDATE GPLoanee
        SET warncounter = NVL(warncounter, 0) + 1
        WHERE loaneeID = :LoaID;

        UPDATE GPComplaints
        SET managestatus = 0
        WHERE complaintsid = :CID";

            var parameters = new { LoaID = loaid, CID = CID };

            dbContext.Connection.Execute(sql, parameters);
        }
    }
}
