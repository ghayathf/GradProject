using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;

namespace Neqatcom.Infra.Repository
{
   public class LoaneeRepository:ILoaneeRepository
    {
        private readonly IDBContext _dbContext;
        public LoaneeRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateLoanee(Gploanee loanee)
        {

            var parameters = new
            {
                NN = loanee.Nationalnumber,
                DofBirth = loanee.Dateofbirth,
                LSalary = loanee.Salary,
                NumOffam = loanee.Numoffamily,
                Score = 10,
                userIDD = loanee.Loaneeuserid
            };

            var result = _dbContext.Connection.Execute("INSERT INTO GPLOANEE(NationalNumber, DateOfBirth, Salary, NumOffamily, CreditScore, loaneeuserid) VALUES(@NN, @DofBirth, @LSalary, @NumOffam, @Score, @userIDD)",
                                                        parameters);

        }

        public void giveComplaintForLender(Gpcomplaint gpcomplaint)
        {
            var parameters = new
            {
                note = gpcomplaint.Compliantnotes,
                LoaneID = gpcomplaint.Loid,
                LendId = gpcomplaint.Leid
            };

            var result = _dbContext.Connection.Execute("INSERT INTO gpcomplaints (compliantnotes, dateofcomplaints, LOID, LEID, managestatus) VALUES(@note, DATEADD(DAY, 1, GETDATE()), @LoaneID, @LendId, 1)", parameters);
        }

        public void DeleteLoanee(int IDD)
        {

            var parameters = new { IDD };
            var result = _dbContext.Connection.Execute("DELETE FROM GPLOANEE WHERE loaneeID = @IDD", parameters);
        }
        public List<Gpnationalnumber> GetAllGpnationalnumber()
        {
            string query = "SELECT * FROM GPNationalnumber";
            IEnumerable<Gpnationalnumber> result = _dbContext.Connection.Query<Gpnationalnumber>(query);
            return result.ToList();
        }
        public List<Gploanee> GetAllLoanees()
        {
            var sql = "SELECT * FROM GPLOANEE";

            IEnumerable<Gploanee> loanees = _dbContext.Connection.Query<Gploanee>(sql);

            return loanees.ToList();
        }

        public List<LoaneeUser> GetAllLoaneeUser()
        {
            var sql = @"SELECT gploanee.*,gpuser.*
                    FROM gploanee
                    inner JOIN gpuser ON gploanee.loaneeuserid = gpuser.userid";

            IEnumerable<LoaneeUser> loaneeUsers = _dbContext.Connection.Query<LoaneeUser>(sql);

            return loaneeUsers.ToList();
        }

        public List<CurrentAndFinishedLoans> GetCurrentAndFinishedLoans(int LID)
        {
            var sql = @"
    SELECT L.*, O.*, LE.*, U.*, C.*
    FROM gploan L
    LEFT JOIN gpoffer O ON O.offerid = L.offerid
    LEFT JOIN gplenderstore LE ON LE.lenderid = O.lenderid
    LEFT JOIN gpuser U ON U.userid = LE.lenderuserid
    LEFT JOIN gpcategory C ON C.categoryID = O.categoryid
    WHERE L.loaneeid = @LID
    AND (L.loanstatus = 3 OR L.loanstatus = 4 OR L.loanstatus = 0)";

            var parameters = new { LID };

            IEnumerable<CurrentAndFinishedLoans> loans = _dbContext.Connection.Query<CurrentAndFinishedLoans>(sql, parameters);

            return loans.ToList();
        }

        public Gploanee GetLoaneeByID(int IDD)
        {
            var sql = "SELECT * FROM GPLOANee WHERE loaneeID = @IDD";

            var parameters = new { IDD };

            IEnumerable<Gploanee> loanees = _dbContext.Connection.Query<Gploanee>(sql, parameters);

            return loanees.FirstOrDefault();
        }

        public List<ConfirmLoans> GetLoansToConfirm(int loaneeidd)
        {
            var sql = @"SELECT M.*, L.LoanID,L.TotalMonths,L.TotalPrice,L.EstimatedPrice,L.monthlyAmount,L.preDaysCounter,L.LateDaysCounter,L.endDate,L.OfferId,L.LoaneeId
,L.Loanstatus,L.Postponestatus,L.Postponedate,L.Beforepaystatus,L.Inpaydatestatus,L.Latepaystatus, O.*, C.*, LE.*, U.*
                    FROM gpmeetings M
                    LEFT JOIN gploan L ON M.loanid = L.loanid
                    LEFT JOIN gpoffer O ON O.offerid = L.offerid
                    LEFT JOIN gpcategory C ON C.categoryid = O.categoryid
                    LEFT JOIN gplenderstore LE ON LE.lenderid = O.lenderid
                    LEFT JOIN gpuser U ON U.userid = LE.lenderuserid
                    WHERE L.loaneeid = 1
                    AND (L.loanstatus = 2 OR L.loanstatus = 1)";

            var parameters = new { loaneeidd };

            IEnumerable<ConfirmLoans> loans = _dbContext.Connection.Query<ConfirmLoans>(sql, parameters);

            return loans.ToList();
        }

        public void UpdateLoanee(Gploanee loanee)
        {
            var parameters = new
            {
                IDD = loanee.Loaneeid,
                NN = loanee.Nationalnumber,
                DofBirth = loanee.Dateofbirth,
                LSalary = loanee.Salary,
                NumOffam = loanee.Numoffamily,
                Score = loanee.Creditscore,
                userIDD = loanee.Loaneeuserid
            };

            var result = _dbContext.Connection.Execute("UPDATE GPLOANEE SET NationalNumber = @NN, DateOfBirth = @DofBirth, Salary = @LSalary, NumOffamily = @NumOffam, CreditScore = @Score,  loaneeuserid = @userIDD WHERE loaneeID = @IDD", parameters);
        }

        public void GiveRateForLender(int IDD, int feedback)
        {
            var parameters = new
            {
                IDD,
                feedback
            };
            var result = _dbContext.Connection.Execute("UPDATE GPMEETINGS  SET feedbackk = @feedback WHERE MeetingID = @IDD", parameters);
        }
    }
}
