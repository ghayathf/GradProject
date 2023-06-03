using Dapper;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace Neqatcom.Infra.Repository
{
    public class PurchasingRepository : IPurchasingRepository
    {
        private readonly IDBContext _dbContext;
        public PurchasingRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void CreatePurchasing(Gppurchasing purchasing)
        {
            var p = new DynamicParameters();
            p.Add("type", purchasing.Paymenttype, DbType.Int32, ParameterDirection.Input);
            p.Add("datee", purchasing.Paymentdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("LID", purchasing.Loanid, DbType.Int32, ParameterDirection.Input);

             _dbContext.Connection.Execute("INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid) VALUES(@type, @datee, @LID)",p);
        }

        public void DeletePurchasing(int id)
        {
            var p = new DynamicParameters();
            p.Add("idd", id, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute("DELETE FROM GPPurchasing WHERE purchaseid = @idd", p);
        }

        public void ForGiveMonthly(int id)
        {

            // Retrieve values into variables
            var query = "SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER FROM GPLOAN WHERE LOANID = @LOANIDD";
            var parameters = new { LOANIDD = id };
            var result = _dbContext.Connection.QueryFirstOrDefault(query, parameters);

            if (result == null)
            {
                // Handle case where loan ID is not found
                return;
            }

            double v_estimatedprice = result.ESTIMATEDPRICE;
            double v_monthlyamount = result.MONTHLYAMOUNT;
            int v_latedayscounter = result.LATEDAYSCOUNTER;

            // Update GPLOAN and insert into GPPurchasing based on conditions
            if (v_estimatedprice > v_monthlyamount)
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    UpdatedEstimatedPrice = v_estimatedprice - v_monthlyamount
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    MonthlyAmount = v_monthlyamount
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @UpdatedEstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (3, GETDATE(), @LOANIDD, @MonthlyAmount)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }
            else
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    EstimatedPrice = 0
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    EstimatedPrice = v_estimatedprice
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @EstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (3, GETDATE(), @LOANIDD, @EstimatedPrice)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }


            // Check if late payment for more than one month and insert into gpcomplaints
            if (v_latedayscounter > 30)
            {
                query = "INSERT INTO gpcomplaints (COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS) " +
                        "VALUES ('Late payment for more than one month', " +
                        "(SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), " +
                        "(SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), GETDATE(), 3);" +
                        "UPDATE GPLOAN SET LATEDAYSCOUNTER = 0 WHERE LOANID = @LOANIDD";
                _dbContext.Connection.Execute(query, new { LOANIDD = id });
            }

            // Update GPLOAN with other fields and set loanstatus
            query = "UPDATE GPLOAN SET STARTDATE = DATEADD(MONTH, 1, STARTDATE), " +
                    "ENDDATE = DATEADD(DAY, @LateDaysCounter, ENDDATE), " +
                    "LATEPAYSTATUS = 0, BEFOREPAYSTATUS = 0, INPAYDATESTATUS = 0, LATEDAYSCOUNTER = @LateDaysCounter, " +
                    "loanstatus = CASE WHEN ESTIMATEDPRICE = 0 THEN 4 ELSE loanstatus END " +
                    "WHERE LOANID = @LOANIDD";
            var updateParameters = new { LOANIDD = id, LateDaysCounter = v_latedayscounter };
            _dbContext.Connection.Execute(query, updateParameters);
        }
        public void PayCash(int id)
        {
            // Retrieve values into variables
            var query = "SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER FROM GPLOAN WHERE LOANID = @LOANIDD";
            var parameters = new { LOANIDD = id };
            var result = _dbContext.Connection.QueryFirstOrDefault(query, parameters);

            if (result == null)
            {
                // Handle case where loan ID is not found
                return;
            }

            double v_estimatedprice = result.ESTIMATEDPRICE;
            double v_monthlyamount = result.MONTHLYAMOUNT;
            int v_latedayscounter = result.LATEDAYSCOUNTER;

            // Update GPLOAN and insert into GPPurchasing based on conditions
            if (v_estimatedprice > v_monthlyamount)
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    UpdatedEstimatedPrice = v_estimatedprice - v_monthlyamount
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    MonthlyAmount = v_monthlyamount
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @UpdatedEstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (1, GETDATE(), @LOANIDD, @MonthlyAmount)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }
            else
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    EstimatedPrice = 0
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    EstimatedPrice = v_estimatedprice
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @EstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (1, GETDATE(), @LOANIDD, @EstimatedPrice)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }


            // Check if late payment for more than one month and insert into gpcomplaints
            if (v_latedayscounter > 30)
            {
                query = "INSERT INTO gpcomplaints (COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS) " +
                        "VALUES ('Late payment for more than one month', " +
                        "(SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), " +
                        "(SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), GETDATE(), 3);" +
                        "UPDATE GPLOAN SET LATEDAYSCOUNTER = 0 WHERE LOANID = @LOANIDD";
                _dbContext.Connection.Execute(query, new { LOANIDD = id });
            }

            // Update GPLOAN with other fields and set loanstatus
            query = "UPDATE GPLOAN SET STARTDATE = DATEADD(MONTH, 1, STARTDATE), " +
                    "ENDDATE = DATEADD(DAY, @LateDaysCounter, ENDDATE), " +
                    "LATEPAYSTATUS = 0, BEFOREPAYSTATUS = 0, INPAYDATESTATUS = 0, LATEDAYSCOUNTER = @LateDaysCounter, " +
                    "loanstatus = CASE WHEN ESTIMATEDPRICE = 0 THEN 4 ELSE loanstatus END " +
                    "WHERE LOANID = @LOANIDD";
            var updateParameters = new { LOANIDD = id, LateDaysCounter = v_latedayscounter };
            _dbContext.Connection.Execute(query, updateParameters);


        }

        public void PayOnline(int id)
        {

            // Retrieve values into variables
            var query = "SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER FROM GPLOAN WHERE LOANID = @LOANIDD";
            var parameters = new { LOANIDD = id };
            var result = _dbContext.Connection.QueryFirstOrDefault(query, parameters);

            if (result == null)
            {
                // Handle case where loan ID is not found
                return;
            }

            double v_estimatedprice = result.ESTIMATEDPRICE;
            double v_monthlyamount = result.MONTHLYAMOUNT;
            int v_latedayscounter = result.LATEDAYSCOUNTER;

            // Update GPLOAN and insert into GPPurchasing based on conditions
            if (v_estimatedprice > v_monthlyamount)
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    UpdatedEstimatedPrice = v_estimatedprice - v_monthlyamount
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    MonthlyAmount = v_monthlyamount
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @UpdatedEstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (2, GETDATE(), @LOANIDD, @MonthlyAmount)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }
            else
            {
                var updateParameters2 = new
                {
                    LOANIDD = id,
                    EstimatedPrice = 0
                };

                var insertParameters = new
                {
                    LOANIDD = id,
                    EstimatedPrice = v_estimatedprice
                };

                string updateQuery = "UPDATE GPLOAN SET ESTIMATEDPRICE = @EstimatedPrice WHERE LOANID = @LOANIDD;";
                string insertQuery = "INSERT INTO GPPurchasing (Paymenttype, Paymentdate, loanid, paymentamount) VALUES (2, GETDATE(), @LOANIDD, @EstimatedPrice)";

                _dbContext.Connection.Execute(updateQuery, updateParameters2);
                _dbContext.Connection.Execute(insertQuery, insertParameters);
            }


            // Check if late payment for more than one month and insert into gpcomplaints
            if (v_latedayscounter > 30)
            {
                query = "INSERT INTO gpcomplaints (COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS) " +
                        "VALUES ('Late payment for more than one month', " +
                        "(SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), " +
                        "(SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), GETDATE(), 3);" +
                        "UPDATE GPLOAN SET LATEDAYSCOUNTER = 0 WHERE LOANID = @LOANIDD";
                _dbContext.Connection.Execute(query, new { LOANIDD = id });
            }

            // Update GPLOAN with other fields and set loanstatus
            query = "UPDATE GPLOAN SET STARTDATE = DATEADD(MONTH, 1, STARTDATE), " +
                    "ENDDATE = DATEADD(DAY, @LateDaysCounter, ENDDATE), " +
                    "LATEPAYSTATUS = 0, BEFOREPAYSTATUS = 0, INPAYDATESTATUS = 0, LATEDAYSCOUNTER = @LateDaysCounter, " +
                    "loanstatus = CASE WHEN ESTIMATEDPRICE = 0 THEN 4 ELSE loanstatus END " +
                    "WHERE LOANID = @LOANIDD";
            var updateParameters = new { LOANIDD = id, LateDaysCounter = v_latedayscounter };
            _dbContext.Connection.Execute(query, updateParameters);
        }
        public List<Gppurchasing> GetAllPurchasing()
        {
            var sql = "SELECT * FROM GPPurchasing";

            var result = _dbContext.Connection.Query<Gppurchasing>(sql, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public Gppurchasing GetPurchasingById(int id)
        {
            var sql = "SELECT * FROM GPPurchasing WHERE purchaseid = @idd";

            var parameters = new { idd = id };

            var result = _dbContext.Connection.QueryFirstOrDefault<Gppurchasing>(sql, parameters);

            return result;
        }

        public List<Gppurchasing> GettAllPayments(int id)
        {
            var sql = "SELECT * FROM GPPurchasing WHERE loanid = @LOANIDD";

            var parameters = new { LOANIDD = id };

            var result = _dbContext.Connection.Query<Gppurchasing>(sql, parameters).ToList();

            return result;
        }

            public void UpdatePurchasing(Gppurchasing purchasing)
            {
            var p = new DynamicParameters();
            p.Add("idd", purchasing.Purchaseid, dbType: DbType.Int32, ParameterDirection.Input);
            p.Add("type", purchasing.Paymenttype, dbType: DbType.Int32, ParameterDirection.Input);
            p.Add("date", purchasing.Paymentdate, dbType: DbType.DateTime, ParameterDirection.Input);
            p.Add("LID", purchasing.Loanid, dbType: DbType.Int32, ParameterDirection.Input);

             _dbContext.Connection.Execute(@"UPDATE GPPurchasing 
                SET Paymenttype = @type,
                    Paymentdate = @date,
                    loanid = @LID
                WHERE purchaseid = @idd", p);
            }
    }
}
