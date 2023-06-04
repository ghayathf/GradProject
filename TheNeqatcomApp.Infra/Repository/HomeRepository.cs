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
    public class HomeRepository : IHomeRepository
    {
        private readonly IDBContext _dbContext;
        public HomeRepository(IDBContext _dbContext)
        {
            this._dbContext = _dbContext;

        }
        public void CalculateCreditScores()
        {
            string query = @"
        DECLARE @LoaneeId INT;
        DECLARE @LateDaysSum INT;
        DECLARE @LoanCount INT;
        DECLARE @CreditScore DECIMAL(10, 2);

        DECLARE loaneeCursor CURSOR FOR
        SELECT LoaneeId
        FROM GPLoanee;

        OPEN loaneeCursor;

        FETCH NEXT FROM loaneeCursor INTO @LoaneeId;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SELECT @LateDaysSum = SUM(l.LateDaysCounter), @LoanCount = COUNT(*)
            FROM GPLoan l
            WHERE l.LoaneeId = @LoaneeId;

            IF @LoanCount > 0
            BEGIN
                SELECT @CreditScore = CASE
                    WHEN (10 - (gn.WarnCounter * 0.6) - (@LateDaysSum * 0.3) - (gn.PostponeCounter * 0.1)) > 10 THEN 10
                    WHEN (10 - (gn.WarnCounter * 0.6) - (@LateDaysSum * 0.3) - (gn.PostponeCounter * 0.1)) < 1 THEN 1
                    ELSE ROUND(10 - (gn.WarnCounter * 0.6) - (@LateDaysSum * 0.3) - (gn.PostponeCounter * 0.1), 2)
                END
                FROM GPLoanee gn
                WHERE gn.LoaneeId = @LoaneeId;
            END
            ELSE
            BEGIN
                SET @CreditScore = 10; -- Set a default value or adjust as needed
            END

            UPDATE GPLoanee
            SET CreditScore = @CreditScore
            WHERE LoaneeId = @LoaneeId;

            FETCH NEXT FROM loaneeCursor INTO @LoaneeId;
        END

        CLOSE loaneeCursor;
        DEALLOCATE loaneeCursor;
    ";

            _dbContext.Connection.Execute(query);
        }








        public void CreateHomeInformation(Gphomepage finalHomepage)
        {
            var p = new DynamicParameters();
            p.Add("Home_Logo", finalHomepage.Logo, DbType.String, ParameterDirection.Input);
            p.Add("PAR1", finalHomepage.Paragraph1, DbType.String, ParameterDirection.Input);
            p.Add("PAR2", finalHomepage.Paragraph2, DbType.String, ParameterDirection.Input);
            p.Add("PAR3", finalHomepage.Paragraph3, DbType.String, ParameterDirection.Input);
            p.Add("ADDRESS", finalHomepage.Companyaddress, DbType.String, ParameterDirection.Input);
            p.Add("EMAIL", finalHomepage.Companyemail, DbType.String, ParameterDirection.Input);
            p.Add("PHONE", finalHomepage.Companyphonenumber, DbType.String, ParameterDirection.Input);

            var result = _dbContext.Connection.Execute("INSERT INTO GPHOMEPAGE (LOGO, PARAGRAPH1, PARAGRAPH2, PARAGRAPH3, COMPANYADDRESS, COMPANYEMAIL, COMPANYPHONENUMBER) VALUES (@Home_Logo, @PAR1, @PAR2, @PAR3, @ADDRESS, @EMAIL, @PHONE)", p, commandType: CommandType.Text);
        }

        public List<bool> CreditScoreStatus(int loaneeid)
        {
            string query = @"
        SELECT CASE
            WHEN COUNT(*) <= 3 THEN 'TRUE'
            ELSE 'FALSE'
        END AS result
        FROM gppurchasing
        WHERE loanid IN (
            SELECT loanid
            FROM gploan
            WHERE loaneeid IN (
                SELECT loaneeid
                FROM gploanee
                WHERE loaneeid = @LoaneeID
            )
        )
        AND (paymenttype = 1 OR paymenttype = 2)";

            var parameters = new { loaneeid };
            IEnumerable<bool> result = _dbContext.Connection.Query<bool>(query, parameters);
            return result.ToList();
        }

       public void DeleteHomeInformation(int id)
{
    var p = new DynamicParameters();
    p.Add("Id_", id, DbType.Int32, ParameterDirection.Input);

    var result = _dbContext.Connection.Execute("DELETE FROM GPHOMEPAGE WHERE HomeID = @Id_", p, commandType: CommandType.Text);
}


        public List<Gphomepage> GetAllHomeInformation()
        {
            IEnumerable<Gphomepage> result = _dbContext.Connection.Query<Gphomepage>("SELECT * FROM GPHOMEPAGE", commandType: CommandType.Text);
            return result.ToList();
        }

        public Gphomepage GetHomeInformationById(int id)
        {
            var p = new DynamicParameters();
            p.Add("id_", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<Gphomepage> result = _dbContext.Connection.Query<Gphomepage>("SELECT * FROM GPHOMEPAGE WHERE HomeID = @id_", p, commandType: CommandType.Text);
            return result.FirstOrDefault();
        }

        public List<LoaneeReminder> GetLoaneesInPayDaytoRemind()
        {
            var query = @"
        SELECT gploan.*, gploanee.*, gpuser.*
        FROM gploan
        INNER JOIN gploanee ON gploan.loaneeid = gploanee.loaneeid
        INNER JOIN gpuser ON gploanee.LOANEEUSERID = gpuser.userid
        WHERE CAST(gploan.startdate AS DATE) = CAST(GETDATE() AS DATE)
        AND gploan.INPAYDATESTATUS = 0
        AND gploan.loanstatus = 3";

            IEnumerable<LoaneeReminder> result = _dbContext.Connection.Query<LoaneeReminder>(query);
            return result.ToList();
        }

        public List<LoaneeReminder> GetLoaneeslatePayDaytoRemind()
        {
            var query = @"
        SELECT gploan.*, gploanee.*, gpuser.*
        FROM gploan
        INNER JOIN gploanee ON gploan.loaneeid = gploanee.loaneeid
        INNER JOIN gpuser ON gploanee.LOANEEUSERID = gpuser.userid
        WHERE CAST(gploan.startdate AS DATE) < CAST(GETDATE() AS DATE)
        AND gploan.latepaystatus = 0
        AND gploan.loanstatus = 3";

            IEnumerable<LoaneeReminder> result = _dbContext.Connection.Query<LoaneeReminder>(query);
            return result.ToList();
        }


        public List<LoaneeReminder> GetLoaneestoRemind()
        {
            var query = @"
        SELECT gploan.*, gploanee.*, gpuser.*
        FROM gploan
        INNER JOIN gploanee ON gploan.loaneeid = gploanee.loaneeid
        INNER JOIN gpuser ON gploanee.LOANEEUSERID = gpuser.userid
        WHERE DATEADD(DAY, -3, CAST(gploan.startdate AS DATE)) = CAST(GETDATE() AS DATE)
        AND gploan.beforepaystatus = 0 AND gploan.loanstatus = 3";

            IEnumerable<LoaneeReminder> result = _dbContext.Connection.Query<LoaneeReminder>(query);
            return result.ToList();
        }



        public List<Lengths> getTableLength()
        {
            var query = @"
        SELECT 'gploan' AS TableName, COUNT(*) AS TableLength FROM gploan
        UNION ALL
        SELECT 'gploanee' AS TableName, COUNT(*) AS TableLength FROM gploanee
        UNION ALL
        SELECT 'gplenderstore' AS TableName, COUNT(*) AS TableLength FROM gplenderstore WHERE gplenderstore.registerstatus = 1";

            IEnumerable<Lengths> result = _dbContext.Connection.Query<Lengths>(query, commandType: CommandType.Text);
            return result.ToList();
        }

        public void UpdateBeforeReminder()
        {
            var query = @"
        UPDATE gploan
        SET beforepaystatus = 1
        WHERE DATEADD(DAY, -3, CAST(gploan.startdate AS DATE)) = CAST(GETDATE() AS DATE)
        AND gploan.beforepaystatus = 0
        AND gploan.loanstatus = 3";

            _dbContext.Connection.Execute(query);
        }


        public void UpdateHomeInformation(Gphomepage finalHomepage)
        {
            var p = new DynamicParameters();
            p.Add("HOME_ID", finalHomepage.Homeid, DbType.Int32, ParameterDirection.Input);

            p.Add("Home_Logo", finalHomepage.Logo, DbType.String, ParameterDirection.Input);
            p.Add("PAR1", finalHomepage.Paragraph1, DbType.String, ParameterDirection.Input);
            p.Add("PAR2", finalHomepage.Paragraph2, DbType.String, ParameterDirection.Input);
            p.Add("PAR3", finalHomepage.Paragraph3, DbType.String, ParameterDirection.Input);
            p.Add("ADDRESS", finalHomepage.Companyaddress, DbType.String, ParameterDirection.Input);
            p.Add("EMAIL", finalHomepage.Companyemail, DbType.String, ParameterDirection.Input);
            p.Add("PHONE", finalHomepage.Companyphonenumber, DbType.String, ParameterDirection.Input);

            var result = _dbContext.Connection.Execute("UPDATE GPHOMEPAGE SET LOGO = @Home_Logo, PARAGRAPH1 = @PAR1, PARAGRAPH2 = @PAR2, PARAGRAPH3 = @PAR3, COMPANYADDRESS = @ADDRESS, COMPANYEMAIL = @EMAIL, COMPANYPHONENUMBER = @PHONE WHERE HomeID = @HOME_ID", p, commandType: CommandType.Text);
        }

        public void UpdateInPayDateReminder()
        {
            var query = @"
        UPDATE gploan
        SET INPAYDATESTATUS = 1
        WHERE TRUNC(gploan.startdate) = TRUNC(SYSDATE)
        AND gploan.INPAYDATESTATUS = 0
        AND gploan.loanstatus = 3";

            _dbContext.Connection.Execute(query);
        }

        public void UpdateLatePayDateReminder()
        {
            var query = @" update gploan
 set latepaystatus=0
    WHERE TRUNC(gploan.startdate) < TRUNC(SYSDATE)  AND gploan.latepaystatus = 1 AND gploan.loanstatus=3 AND  TRUNC(gploan.postponedate) < TRUNC(SYSDATE)";

            _dbContext.Connection.Execute(query);
        }
    }
}
