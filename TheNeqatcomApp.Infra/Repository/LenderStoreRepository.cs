using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;

namespace TheNeqatcomApp.Infra.Repository
{
    public class LenderStoreRepository : ILenderStoreRepository
    {
        private readonly IDBContext _dbContext;
        public LenderStoreRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void createLenderStore(Gplenderstore gplenderstore)
        {
            var parameters = new
            {
                CommercialRegister = gplenderstore.Commercialregister,
                UserID = gplenderstore.Lenderuserid,
                RegisterStatus = -1,
                ShadowStatus = 0,
                CompanySize = gplenderstore.Companysize,
                SiteURL = gplenderstore.Siteurl,
                BankAccount = gplenderstore.Bankaccount
            };

            var result = _dbContext.Connection.Execute("INSERT INTO GPLENDERSTORE (COMMERCIALREGISTER, LENDERUSERID, REGISTERSTATUS, SHADOWSTATUS, COMPANYSIZE, SITEURL, bankaccount) VALUES (@CommercialRegister, @UserID, @RegisterStatus, @ShadowStatus, @CompanySize, @SiteURL, @BankAccount)",
                                                        parameters);
        }

        public void DeleteLenderStore(int id)
        {
            var parameters = new
            {
                id
            };

            var result = _dbContext.Connection.Execute("DELETE FROM GPLENDERSTORE WHERE Lenderid = @id", parameters);
        }

        public List<Gplenderstore> GetAllLenderStore()
        {
            string query = "SELECT * FROM GPLENDERSTORE";
            IEnumerable<Gplenderstore> result = _dbContext.Connection.Query<Gplenderstore>(query);
            return result.ToList();
        }

        public List<LenderUser> GetAllLenderUser()
        {
            string query = "SELECT * FROM GPLENDERSTORE JOIN GPUSER ON GPLENDERSTORE.LENDERUSERID = GPUSER.USERID WHERE GPLENDERSTORE.REGISTERSTATUS = -1";
            IEnumerable<LenderUser> result = _dbContext.Connection.Query<LenderUser>(query);
            return result.ToList();
        }

        public List<LoaneesForLendercs> GetAllLoaneesForLendercs(int id)
        {
            var parameters = new { id };
            string query = @"
        SELECT DISTINCT *
        FROM GPLOAN
        INNER JOIN GPOFFER ON GPLOAN.OFFERID = GPOFFER.OFFERID
        INNER JOIN GPLOANEE ON GPLOAN.LOANEEID = GPLOANEE.LOANEEID
        INNER JOIN GPUSER ON gploanee.loaneeuserid = GPUSER.USERID
        LEFT JOIN GPLENDERSTORE ON GPOFFER.LENDERID = GPLENDERSTORE.LENDERID
        WHERE GPLENDERSTORE.LENDERID = @id
        AND GPUSER.USERID IN (
            SELECT GPUSER.USERID
            FROM GPLOAN
            INNER JOIN GPOFFER ON GPLOAN.OFFERID = GPOFFER.OFFERID
            INNER JOIN GPLOANEE ON GPLOAN.LOANEEID = GPLOANEE.LOANEEID
            INNER JOIN GPUSER ON gploanee.loaneeuserid = GPUSER.USERID
            LEFT JOIN GPLENDERSTORE ON GPOFFER.LENDERID = GPLENDERSTORE.LENDERID
            WHERE GPLENDERSTORE.LENDERID = @id
            GROUP BY GPUSER.USERID
            HAVING MIN(GPLOAN.LOANID) = (
                SELECT MIN(LOANID)
                FROM GPLOAN
                INNER JOIN GPOFFER ON GPLOAN.OFFERID = GPOFFER.OFFERID
                INNER JOIN GPLOANEE ON GPLOAN.LOANEEID = GPLOANEE.LOANEEID
                INNER JOIN GPUSER ON gploanee.loaneeuserid = GPUSER.USERID
                LEFT JOIN GPLENDERSTORE ON GPOFFER.LENDERID = GPLENDERSTORE.LENDERID
                WHERE GPLENDERSTORE.LENDERID = @id
                GROUP BY GPUSER.USERID
            )
        )";

            IEnumerable<LoaneesForLendercs> result = _dbContext.Connection.Query<LoaneesForLendercs>(query, parameters);
            return result.ToList();
        }



        public List<LoanOffer> GetAllLoanOffer(int lenderid, int loaneeid)
        {
            string query = @"SELECT gploan.*, gpoffer.*, gpuser.*
                     FROM GPLoan gploan
                     INNER JOIN GPOffer gpoffer
                     ON gploan.OFFERID = gpoffer.offerID
                     INNER JOIN GPLoanee gploanee
                     ON gploan.LOANEEID = gploanee.loaneeID
                     INNER JOIN GPUser gpuser
                     ON gpuser.userid = gploanee.loaneeuserid
                     INNER JOIN GPLenderStore gplenderstore
                     ON gpoffer.LENDERID = gplenderstore.lenderID
                     WHERE gplenderstore.lenderID = @lenderid
                     AND gploanee.loaneeID = @loaneeid";

            var parameters = new { lenderid, loaneeid };
            IEnumerable<LoanOffer> result = _dbContext.Connection.Query<LoanOffer>(query, parameters);
            return result.ToList();
        }

        public List<Lengths> GetLenderCounters(int IDD)
        {
            string query = @"
        SELECT 'gploan' AS TableName, COUNT(*) AS TableLength
        FROM gploan
        WHERE OfferId IN (
            SELECT OfferId
            FROM gpoffer
            WHERE LenderId = @IDD
        )
        UNION ALL
        SELECT 'gpmeetings' AS TableName, COUNT(*) AS TableLength
        FROM gpmeetings
        WHERE LenderId = @IDD
        UNION ALL
        SELECT 'gpoffer' AS TableName, COUNT(*) AS TableLength
        FROM gpoffer
        WHERE LenderId = @IDD";

            var parameters = new { IDD };
            IEnumerable<Lengths> result = _dbContext.Connection.Query<Lengths>(query, parameters);
            return result.ToList();
        }


        public LenderInfo GetLenderInfo(int id)
        {
            string query = @"SELECT GPLENDERSTORE.lenderid, GPLENDERSTORE.commercialRegister, GPLENDERSTORE.lenderuserid, GPLENDERSTORE.registerStatus, GPLENDERSTORE.ShadowStatus, GPLENDERSTORE.companySize, GPLENDERSTORE.SiteURL, GPUSER.UserID, GPUSER.FirstName, GPUSER.LastName, GPUSER.Email, GPUSER.password, GPUSER.phoneNum, GPUSER.Address, GPUSER.Role, GPUSER.userName, GPUSER.userimage, CEILING(AVG(gpmeetings.feedbackk)) AS avg_feedback
                     FROM GPLENDERSTORE
                     LEFT JOIN GPUSER ON GPLENDERSTORE.lenderuserid = GPUSER.userid
                     LEFT JOIN gpmeetings ON GPLENDERSTORE.lenderid = gpmeetings.lenderid
                     WHERE GPLENDERSTORE.lenderid = @id
                     GROUP BY GPLENDERSTORE.lenderid, GPLENDERSTORE.commercialRegister, GPLENDERSTORE.lenderuserid, GPLENDERSTORE.registerStatus, GPLENDERSTORE.ShadowStatus, GPLENDERSTORE.companySize, GPLENDERSTORE.SiteURL, GPUSER.UserID, GPUSER.FirstName, GPUSER.LastName, GPUSER.Email, GPUSER.password, GPUSER.phoneNum, GPUSER.Address, GPUSER.Role, GPUSER.userName, GPUSER.userimage";

            var parameters = new { id };
            LenderInfo result = _dbContext.Connection.QueryFirstOrDefault<LenderInfo>(query, parameters);
            return result;
        }


        public List<LenderPayment> GetLenderPayments(int lenderid)
        {
            string query = @"SELECT 
                        FORMAT(p.paymentdate, 'MMMM') AS MonthName,
                        SUM(p.paymentamount) AS TotalPayments
                    FROM 
                        GPPURCHASING p
                        INNER JOIN gploan l ON p.loanid = l.loanid
                        INNER JOIN gpoffer o ON l.offerid = o.offerid
                    WHERE 
                        o.lenderid = @lenderid
                    GROUP BY 
                        FORMAT(p.paymentdate, 'MMMM')";

            var parameters = new { lenderid };
            IEnumerable<LenderPayment> result = _dbContext.Connection.Query<LenderPayment>(query, parameters);
            return result.ToList();
        }


        public Gplenderstore GetLenderStoreById(int id)
        {
            string query = "SELECT * FROM GPLENDERSTORE WHERE Lenderid = @id";
            var parameters = new { id };
            Gplenderstore result = _dbContext.Connection.QueryFirstOrDefault<Gplenderstore>(query, parameters);
            return result;
        }

        public void giveComplaintForLoanee(Gpcomplaint gpcomplaint)
        {
            string query = @"INSERT INTO gpcomplaints (
                        compliantnotes,
                        dateofcomplaints,
                        LOID,
                        LEID,
                        managestatus
                    ) VALUES (
                        @Note,
                        GetDate(),
                        @LoaneeID,
                        @LenderID,
                        2
                    )";

            var parameters = new
            {
                Note = gpcomplaint.Compliantnotes,
                LoaneeID = gpcomplaint.Loid,
                LenderID = gpcomplaint.Leid
            };

            _dbContext.Connection.Execute(query, parameters);
        }

        public void UpdateLenderStore(Gplenderstore gplenderstore)
        {
            string query = @"UPDATE GPLENDERSTORE SET 
                        COMMERCIALREGISTER = @CommercialRegister, 
                        LENDERUSERID = @LenderUserID,
                        REGISTERSTATUS = @RegisterStatus,
                        SHADOWSTATUS = @ShadowStatus,
                        COMPANYSIZE = @CompanySize,
                        SITEURL = @SiteURL
                    WHERE Lenderid = @LenderID";

            var parameters = new
            {
                CommercialRegister = gplenderstore.Commercialregister,
                LenderUserID = gplenderstore.Lenderuserid,
                RegisterStatus = gplenderstore.Registerstatus,
                ShadowStatus = gplenderstore.Shadowstatus,
                CompanySize = gplenderstore.Companysize,
                SiteURL = gplenderstore.Siteurl,
                LenderID = gplenderstore.Lenderid
            };

            _dbContext.Connection.Execute(query, parameters);
        }
    }
}
