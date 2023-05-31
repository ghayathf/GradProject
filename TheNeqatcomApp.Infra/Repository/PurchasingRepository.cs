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
            var p = new DynamicParameters();
            p.Add("LOANIDD", id, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute(
                @"DECLARE
                      v_estimatedprice GPLOAN.ESTIMATEDPRICE % TYPE;
            v_monthlyamount GPLOAN.MONTHLYAMOUNT % TYPE;
            v_latedayscounter GPLOAN.LATEDAYSCOUNTER % TYPE;
            BEGIN
              SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER INTO v_estimatedprice, v_monthlyamount, v_latedayscounter
              FROM GPLOAN WHERE LOANID = @LOANIDD;

            IF v_estimatedprice > v_monthlyamount THEN
              UPDATE GPLOAN
              SET ESTIMATEDPRICE = v_estimatedprice - v_monthlyamount
                        WHERE LOANID = @LOANIDD;
            INSERT INTO GPPurchasing(Paymenttype, Paymentdate, loanid, paymentamount)
                          VALUES(3, SYSDATE, @LOANIDD, v_monthlyamount);
            ELSE
              UPDATE GPLOAN
              SET ESTIMATEDPRICE = 0
                        WHERE LOANID = @LOANIDD;
            INSERT INTO GPPurchasing(Paymenttype, Paymentdate, loanid, paymentamount)
                          VALUES(3, SYSDATE, @LOANIDD, v_estimatedprice);
            END IF;

            COMMIT;

            IF v_latedayscounter > 30 THEN
              INSERT INTO gpcomplaints(COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS)
                          VALUES('Late payment for more than one month', (SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), (SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), SYSDATE, 3);
        v_latedayscounter:= 0;
            END IF;

            UPDATE GPLOAN SET
              STARTDATE = ADD_MONTHS(STARTDATE, 1),
              ENDDATE = ENDDATE + v_latedayscounter,
              LATEPAYSTATUS = 0,
              BEFOREPAYSTATUS = 0,
              INPAYDATESTATUS = 0,
              LATEDAYSCOUNTER = v_latedayscounter;

            COMMIT;

            UPDATE gploan
                      SET loanstatus = 4
                      WHERE ESTIMATEDPRICE = 0;

            COMMIT;
            END; ", p);
        }
        public void PayCash(int id)
        {
            var p = new DynamicParameters();
            p.Add("LOANIDD", id, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute(@"DECLARE
                      v_estimatedprice GPLOAN.ESTIMATEDPRICE%TYPE;
                      v_monthlyamount GPLOAN.MONTHLYAMOUNT%TYPE;
                      v_latedayscounter GPLOAN.LATEDAYSCOUNTER%TYPE;
                    BEGIN
                      SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER INTO v_estimatedprice, v_monthlyamount, v_latedayscounter
                      FROM GPLOAN WHERE LOANID = @LOANIDD;

                      IF v_estimatedprice > v_monthlyamount THEN
                        UPDATE GPLOAN
                        SET ESTIMATEDPRICE = v_estimatedprice - v_monthlyamount
                        WHERE LOANID = @LOANIDD;
                        INSERT INTO GPPurchasing(Paymenttype,Paymentdate,loanid,paymentamount)
                          VALUES(1, SYSDATE, @LOANIDD, v_monthlyamount);
                      ELSE
                        UPDATE GPLOAN
                        SET ESTIMATEDPRICE = 0
                        WHERE LOANID = @LOANIDD;
                        INSERT INTO GPPurchasing(Paymenttype,Paymentdate,loanid,paymentamount)
                          VALUES(1, SYSDATE, @LOANIDD, v_estimatedprice);
                      END IF;

                      COMMIT;

                      IF v_latedayscounter > 30 THEN
                        INSERT INTO gpcomplaints(COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS)
                          VALUES('Late payment for more than one month', (SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), (SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), SYSDATE, 3);
                        v_latedayscounter := 0; 
                      END IF;

                      UPDATE GPLOAN SET
                        STARTDATE = ADD_MONTHS(STARTDATE, 1),
                        ENDDATE = ENDDATE + v_latedayscounter,
                        LATEPAYSTATUS = 0,
                        BEFOREPAYSTATUS = 0,
                        INPAYDATESTATUS = 0,
                        LATEDAYSCOUNTER = v_latedayscounter;
                    
                      COMMIT;

                      UPDATE gploan
                      SET loanstatus = 4
                      WHERE ESTIMATEDPRICE = 0;
                      
                      COMMIT;
                    END;", p);
        }
        public void PayOnline(int id)
        {
            var p = new DynamicParameters();
            p.Add("LOANIDD", id, DbType.Int32, ParameterDirection.Input);
            _dbContext.Connection.Execute(@"DECLARE
                      v_estimatedprice GPLOAN.ESTIMATEDPRICE%TYPE;
                      v_monthlyamount GPLOAN.MONTHLYAMOUNT%TYPE;
                      v_latedayscounter GPLOAN.LATEDAYSCOUNTER%TYPE;
                    BEGIN
                      SELECT ESTIMATEDPRICE, MONTHLYAMOUNT, LATEDAYSCOUNTER INTO v_estimatedprice, v_monthlyamount, v_latedayscounter
                      FROM GPLOAN WHERE LOANID = @LOANIDD;

                      IF v_estimatedprice > v_monthlyamount THEN
                        UPDATE GPLOAN
                        SET ESTIMATEDPRICE = v_estimatedprice - v_monthlyamount
                        WHERE LOANID = @LOANIDD;
                        INSERT INTO GPPurchasing(Paymenttype,Paymentdate,loanid,paymentamount)
                          VALUES(2, SYSDATE, @LOANIDD, v_monthlyamount);
                      ELSE
                        UPDATE GPLOAN
                        SET ESTIMATEDPRICE = 0
                        WHERE LOANID = @LOANIDD;
                        INSERT INTO GPPurchasing(Paymenttype,Paymentdate,loanid,paymentamount)
                          VALUES(2, SYSDATE, @LOANIDD, v_estimatedprice);
                      END IF;

                      COMMIT;

                      IF v_latedayscounter > 30 THEN
                        INSERT INTO gpcomplaints(COMPLIANTNOTES, leid, loid, DATEOFCOMPLAINTS, MANAGESTATUS)
                          VALUES('Late payment for more than one month', (SELECT LenderId FROM GPOFFER WHERE OfferId = (SELECT OfferId FROM GPLOAN WHERE LOANID = @LOANIDD)), (SELECT LoaneeId FROM GPLOAN WHERE LOANID = @LOANIDD), SYSDATE, 3);
                        v_latedayscounter := 0; 
                      END IF;

                      UPDATE GPLOAN SET
                        STARTDATE = ADD_MONTHS(STARTDATE, 1),
                        ENDDATE = ENDDATE + v_latedayscounter,
                        LATEPAYSTATUS = 0,
                        BEFOREPAYSTATUS = 0,
                        INPAYDATESTATUS = 0,
                        LATEDAYSCOUNTER = v_latedayscounter;
                    
                      COMMIT;

                      UPDATE gploan
                      SET loanstatus = 4
                      WHERE ESTIMATEDPRICE = 0;
                      
                      COMMIT;
                    END;", p);
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
