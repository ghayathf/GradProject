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
    public class LoanRepository : ILoanRepository
    {
        private readonly IDBContext _dbContext;
        public LoanRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void ConfirmNewLoanInfo(Gploan loan)
        {
            string query = @"UPDATE GPLOAN
                    SET TOTALMONTHS = @TotalMonths,
                        TOTALPRICE = @TotalPrice,
                        MONTHLYAMOUNT = @MonthlyAmount,
                        STARTDATE = @StartDate,
                        ENDDATE = @EndDate,
                        LOANSTATUS = 2,
                        estimatedprice = @TotalPrice
                    WHERE LOANID = @LoanID";

            var parameters = new
            {
                TotalMonths = loan.Totalmonths,
                TotalPrice = loan.Totalprice,
                MonthlyAmount = loan.Monthlyamount,
                StartDate = loan.Startdate,
                EndDate = loan.Enddate,
                LoanID = loan.Loanid
            };

            _dbContext.Connection.Execute(query, parameters);
        }

        public void CreateLoan(Gploan loan)
        {
            var parameters = new
            {
                TotalMon = loan.Totalmonths,
                TotalPri = loan.Totalprice,
                EstimatedPri = loan.Estimatedprice,
                MonAmount = loan.Monthlyamount,
                PreCounter = loan.Predayscounter,
                LateCounter = loan.Latedayscounter,
                Startd = loan.Startdate,
                Endd = loan.Enddate,
                OfferIDD = loan.Offerid,
                LoaneeIDD = loan.Loaneeid
            };

            var result = _dbContext.Connection.Execute("INSERT INTO GPLOAN (TOTALMONTHS, TOTALPRICE, ESTIMATEDPRICE, MonthlyAmount, PreDaysCounter, LateDaysCounter, StartDate, EndDate, OfferID, LoaneeID) " +
                                                        "VALUES (@TotalMon, @TotalPri, @EstimatedPri, @MonAmount, @PreCounter, @LateCounter, @Startd, @Endd, @OfferIDD, @LoaneeIDD)",
                                                        parameters);

        }

        public void DeleteLoan(int IDD)
        {
            var parameters = new
            {
                IDD
            };

            var result = _dbContext.Connection.Execute("DELETE FROM GPLOAN WHERE LoanID = @IDD", parameters);
        }

        public int ExistingLoanCounter(int LoaneeID)
        {
            string query = @"SELECT COUNT(*) FROM GPLoan
               WHERE LoaneeId = @LoaneeID AND loanstatus = 3";

            var parameters = new { LoaneeID };
            int result = _dbContext.Connection.QueryFirstOrDefault<int>(query, parameters);
            return result;
        }

        public List<Gploan> GetAllLoans()
        {
            string query = "SELECT * FROM GPLOAN";
            IEnumerable<Gploan> result = _dbContext.Connection.Query<Gploan>(query);
            return result.ToList();
        }

        public List<RequestedLoan> GetAllRequestedLoan(int LSID, int statuss)
        {
            string query = @" SELECT l.*, o.*, c.*, ls.*, le.*, u.*, m.*
FROM GPLoan l
JOIN GPOffer o ON l.OfferId = o.offerID
JOIN GPCategory c ON o.CategoryId = c.categoryID
JOIN GPLenderStore ls ON o.LenderId = ls.lenderID
JOIN GPLoanee le ON l.LoaneeId = le.loaneeID
JOIN GPUser u ON le.loaneeuserid = u.userid
LEFT JOIN GPMeetings m ON m.loanid = l.loanid
WHERE ls.lenderID = @LSID
AND l.loanstatus = @statuss";

            var parameters = new { LSID ,
                statuss
            };
            IEnumerable<RequestedLoan> result = _dbContext.Connection.Query<RequestedLoan>(query, parameters);
            return result.ToList();
        }

        public List<RequestedLoan> GetAllRequestedPostPone(int LSID, int statuss)
        {
            string query = @"SELECT l.*, o.*, c.*, ls.*, le.*, u.*, m.*
FROM GPLoan l
JOIN GPOffer o ON l.OfferId = o.offerID
JOIN GPCategory c ON o.CategoryId = c.categoryID
JOIN GPLenderStore ls ON o.LenderId = ls.lenderID
JOIN GPLoanee le ON l.LoaneeId = le.loaneeID
JOIN GPUser u ON le.loaneeuserid = u.userid
LEFT JOIN GPMeetings m ON m.loanid = l.loanid
WHERE ls.lenderID = @LSID
AND l.POSTPONESTATUS = @statuss";

            var parameters = new
            {
                LSID,
                statuss
            };
            IEnumerable<RequestedLoan> result = _dbContext.Connection.Query<RequestedLoan>(query, parameters);
            return result.ToList();
        }

        public Gploan GetLoanByID(int IDD)
        {

            string query = "SELECT * FROM GPLOAN WHERE Loanid = @IDD";
            var parameters = new { IDD };
            Gploan result = _dbContext.Connection.QueryFirstOrDefault<Gploan>(query, parameters);
            return result;
        }

        public void RequestNewLoan(int loaneeid, int offerid, int totalmonths)
        {
           
            var parameters = new
            {
                loaneeid,
                offerid,
                totalmonths
            };
            var result = _dbContext.Connection.Execute("insert into gploan (LoaneeID, OfferID, TOTALMONTHS, StartDate, EndDate)" +
                                                       "VALUES(@loaneeid, @offerid, @totalmonths, TO_DATE('2000-01-01', 'YYYY-MM-DD'), TO_DATE('2000-01-01', 'YYYY-MM-DD'))",
                                                       parameters);
        }

        public void UpdateLoan(Gploan loan)
        {
            string query = @"UPDATE GPLOAN
              SET
              TOTALMONTHS=@TOTALMONTHS,
              TOTALPRICE=@TOTALPRICE,
                ESTIMATEDPRICE=@ESTIMATEDPRICE,
               PreDaysCounter=@PreDaysCounter,
             LateDaysCounter=@LateDaysCounter,
             StartDate=@StartDate,
             EndDate=@EndDate,
             OfferID=@OfferID,
             LoaneeID=@LoaneeID
            WHERE loanID = @loanId";

            var parameters = new
            {
                TOTALMONTHS = loan.Totalmonths,
                TOTALPRICE = loan.Totalprice,
                ESTIMATEDPRICE = loan.Estimatedprice,
                PreDaysCounter = loan.Predayscounter,
                LateDaysCounter = loan.Latedayscounter,
                StartDate = loan.Startdate,
                EndDate = loan.Enddate,
                OfferID=loan.Offerid,
                LoaneeID=loan.Loaneeid,
                loanId=loan.Loanid
            };

            _dbContext.Connection.Execute(query, parameters);
        }

        public void UpdateLoanStatus(int LoanID, int status)
        {
            string query = @" UPDATE GPLoan
  SET loanstatus = @status
  WHERE loanID = @LoanID";

            var parameters = new
            {
                LoanID,
                status

            };

            _dbContext.Connection.Execute(query, parameters);
        }

        public void UpdatePostponeStatus(int LoanID, int status, int loaneeidd)
        {
            string query = @"IF(@status = 0) THEN
    UPDATE GPLoan
    SET POSTPONESTATUS = @status,
        STARTDATE = ADD_MONTHS(STARTDATE, 1),
        ENDDATE = ADD_MONTHS(ENDDATE, 1),
         LATEPAYSTATUS=0,
         BEFOREPAYSTATUS=0,
         INPAYDATESTATUS=0
    WHERE loanID = @LoanID;

    UPDATE GPLoanee
    SET POSTPONECOUNTER = POSTPONECOUNTER + 1
    WHERE loaneeID = @loaneeidd;

    INSERT INTO GPPurchasing (PAYMENTDATE, PAYMENTTYPE, LOANID)
    VALUES (SYSDATE, 5, @LoanID);
  ELSE
    UPDATE GPLoan
    SET POSTPONESTATUS = newStatus
    WHERE loanID = @LoanID";

            var parameters = new
            {
                LoanID,
                status,
                loaneeidd

            };

            _dbContext.Connection.Execute(query, parameters);
        }
    }
}
