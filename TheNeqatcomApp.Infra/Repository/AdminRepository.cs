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
            (SELECT ROUND(AVG(CreditScore), 2) FROM GPLoanee) AS averageCreditScore,
            (SELECT COUNT(*) FROM GPLoan) AS totalLoansCount";

            IEnumerable<AdminStatisticsLoanee> result = dbContext.Connection.Query<AdminStatisticsLoanee>(sql);
            return result.FirstOrDefault();
        }


        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            var sql = @"
        SELECT gploan.*, gploanee.*, gpuser.*, gpmeetings.*
        FROM gploan
        INNER JOIN gploanee ON gploan.loaneeid = gploanee.loaneeid
        INNER JOIN gpuser ON gploanee.LOANEEUSERID = gpuser.userid
        INNER JOIN gpmeetings ON gpmeetings.loanid = gploan.loanid
        WHERE DATEADD(DAY, 3, CAST(gpmeetings.startdate AS DATE)) <= CAST(GETDATE() AS DATE)
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
        WHERE DATEADD(DAY, 3, CAST(gpmeetings.startdate AS DATE)) <= CAST(GETDATE() AS DATE)
        AND (gploan.loanstatus = 1 OR gploan.loanstatus = 2)";

            IEnumerable<CancleLoanMsgforLender> loan = dbContext.Connection.Query<CancleLoanMsgforLender>(sql);
            return loan.ToList();
        }



        public CategoriesStatistics categoriesStatistics()
        {
            var sql = @"
        SELECT
            (SELECT COUNT(*) FROM GPCategory) AS TotalCategories,
            (SELECT COUNT(*) FROM GPOffer) AS TotalOffers,
            (SELECT COUNT(*) FROM GPLoan) AS TotalLoans";

            IEnumerable<CategoriesStatistics> result = dbContext.Connection.Query<CategoriesStatistics>(sql);
            return result.FirstOrDefault();
        }


        public ComplaintsStatistics complaintsStatistics()
        {
            var sql = @"
        SELECT
            SUM(CASE WHEN manageStatus = 1 THEN 1 ELSE 0 END) AS LoaneeToLenderCount,
            SUM(CASE WHEN manageStatus = 2 THEN 1 ELSE 0 END) AS LenderToLoaneeCount,
            SUM(CASE WHEN manageStatus = 3 THEN 1 ELSE 0 END) AS SystemToLoaneeCount
        FROM
            GPCOMPLAINTS";

            IEnumerable<ComplaintsStatistics> result = dbContext.Connection.Query<ComplaintsStatistics>(sql);
            return result.FirstOrDefault();
        }


        public void deleteComplaint(int cid)
        {
            var sql = "UPDATE GPComplaints SET managestatus = -1 WHERE complaintsid = @CID";
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
            WHERE LenderID = @LenderID
        ) AND commercialcode IS NOT NULL";

            v_count = dbContext.Connection.ExecuteScalar<int>(countQuery, new { LenderID = IDD });

            if (v_count > 0)
            {
                // If the commercial register exists, update the register status in the lenderstore table
                string updateQuery = @"
            UPDATE GPLenderstore 
            SET RegisterStatus = 1 
            WHERE LenderID = @LenderID";

                dbContext.Connection.Execute(updateQuery, new { LenderID = IDD });
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
        SET warncounter = ISNULL(warncounter, 0) + 1
        WHERE loaneeID = @LoaID;

        UPDATE GPComplaints
        SET managestatus = 0
        WHERE complaintsid = @CID";

            var parameters = new { LoaID = loaid, CID = CID };

            dbContext.Connection.Execute(sql, parameters);
        }


        public void ManageComplaints(int LID, int CID)
        {
            var sql = @" UPDATE GPLenderStore
            SET warncounter = ISNULL(warncounter, 0) + 1,
                ShadowStatus = CASE
                                    WHEN ISNULL(warncounter, 0) + 1 <= 3 THEN ShadowStatus
                                    ELSE 1
                                END,
                WarnDate = CASE
                                WHEN ISNULL(warncounter, 0) + 1 <= 3 THEN WarnDate
                                ELSE GetDate()
                            END
            WHERE lenderID = @LID;

            UPDATE GPComplaints
            SET GPComplaints.managestatus = 0
            WHERE GPComplaints.complaintsid = @CID";

            var parameters = new { LID = LID, CID = CID }; 

            dbContext.Connection.Execute(sql, parameters);
        }

        public List<LoaneeComplaintsDTO> GetAllCompliants()
        {
            var sql = @"SELECT GPComplaints.*,GPLoanee.loaneeid,GPLenderStore.*,GPUser.*
FROM GPComplaints 
LEFT JOIN GPLoanee ON GPComplaints.LOID = GPLoanee.loaneeID
LEFT JOIN GPLenderStore ON GPComplaints.LEID = GPLenderStore.lenderID
LEFT JOIN GPUser ON  GPLenderStore.LENDERuserID = GPUser.userID
where GPComplaints.managestatus=1";

            return dbContext.Connection.Query<LoaneeComplaintsDTO>(sql).ToList();
        }

        public void CheckFiveDays()
        {
            var sql = @"UPDATE GPLenderStore
SET ShadowStatus = 0, warncounter = 0
WHERE ShadowStatus = 1 AND warndate < GetDate() - 5;
";
            dbContext.Connection.Execute(sql);
        }
    }
}
