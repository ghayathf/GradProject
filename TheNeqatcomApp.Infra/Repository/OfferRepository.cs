using Dapper;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Repository;

namespace Neqatcom.Infra.Repository
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IDBContext _dbContext;
        public OfferRepository(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void CreateOffer(Gpoffer offer)
        {
            // Check if the category exists before inserting
            string categoryQuery = "SELECT 1 FROM GPCategory WHERE CategoryID = @CategoryId";
            bool categoryExists = _dbContext.Connection.ExecuteScalar<bool>(categoryQuery, new { CategoryId = offer.Categoryid });

            if (categoryExists)
            {
                var parameters = new
                {
                    months = offer.Totalmonths,
                    des = offer.Descriptions,
                    LID = offer.Lenderid,
                    CID = offer.Categoryid,
                    minmonth_ = offer.Minmonth
                };

                _dbContext.Connection.Execute(
                    @"INSERT INTO GPOffer (TOTALMONTHS, DESCRIPTIONS, LENDERID, CATEGORYID, MINMONTH)
              VALUES (@months, @des, @LID, @CID, @minmonth_)",
                    parameters);
            }
            else
            {
                // Handle the case where the category ID doesn't exist
                // You can throw an exception or perform error handling accordingly
            }
        }


        public void DeleteOffer(int id)
        {
            var parameters = new { idd = id };
            _dbContext.Connection.Execute("DELETE FROM GPOffer WHERE Offerid = @idd", parameters);
        }
        public List<LoaneeMain> GetLoaneeMain()
        {
            var sql = @"SELECT GPOFFER.*, GPCATEGORY.*, GPLENDERSTORE.*, GPUSER.*
                FROM GPOFFER
                LEFT JOIN GPCATEGORY ON GPOFFER.CATEGORYID = GPCATEGORY.CATEGORYID
                LEFT JOIN GPLENDERSTORE ON GPOFFER.LENDERID = GPLENDERSTORE.LENDERID
                LEFT JOIN GPUSER ON GPLENDERSTORE.LenderUserID = GPUSER.UserID";
            var result = _dbContext.Connection.Query<LoaneeMain>(sql);

            return result.ToList();
        }
        public List<OffersForLenderMain> GetOffersForLenderMain(int lendId)
        {
            var sql = @"SELECT subquery.OfferID, subquery.TotalMonths, subquery.Descriptions, subquery.LenderId, subquery.minmonth, subquery.CategoryId, subquery.CategoryID AS CategoryID2, subquery.CategoryName, subquery.CategoryImage
                FROM (
                   SELECT gpoffer.OfferID, gpoffer.TotalMonths, gpoffer.Descriptions, gpoffer.LenderId, gpoffer.minmonth, gpoffer.CategoryId, gpcategory.CategoryID AS gpcat_CategoryID, gpcategory.CategoryName, gpcategory.CategoryImage,
                          ROW_NUMBER() OVER (ORDER BY NEWID()) AS rn
                   FROM gpoffer
                   LEFT JOIN gpcategory ON gpoffer.categoryid = gpcategory.categoryid
                   WHERE gpoffer.lenderid = @lendId
                ) subquery
                WHERE rn <= 3";

            var result = _dbContext.Connection.Query<OffersForLenderMain>(sql, new { lendId });

            return result.ToList();
        }
        public List<Gpoffer> GetAllOferById(int id)
        {
            var sql = "SELECT * FROM GPOffer WHERE lenderid = @id";
            var result = _dbContext.Connection.Query<Gpoffer>(sql, new { id });
            
            return result.ToList();

        }

        public List<Gpoffer> GetAllOffers()
        {
            var sql = "SELECT * FROM GPOffer";
            var result = _dbContext.Connection.Query<Gpoffer>(sql);

            return result.ToList();
        }

        public Gpoffer GetOfferById(int id)
        {
            var sql = "SELECT * FROM GPOffer WHERE Offerid = @idd";

            var p = new DynamicParameters();
            p.Add("idd", id, DbType.Int32, ParameterDirection.Input);

            var result = _dbContext.Connection.QueryFirstOrDefault<Gpoffer>(sql, p);

            return result;
        }

        public void UpdateOffer(Gpoffer offer)
        {
            var sql = "UPDATE GPOffer SET TOTALMONTHS = @months, DESCRIPTIONS = @des, MINMONTH = @minmonth_, categoryid = @CID, lenderid = @LID WHERE Offerid = @idd";

            var p = new DynamicParameters();
            p.Add("idd", offer.Offerid, DbType.Int32, ParameterDirection.Input);
            p.Add("months", offer.Totalmonths, DbType.Int32, ParameterDirection.Input);
            p.Add("des", offer.Descriptions, DbType.String, ParameterDirection.Input);
            p.Add("CID", offer.Categoryid, DbType.Int32, ParameterDirection.Input);
            p.Add("LID", offer.Lenderid, DbType.Int32, ParameterDirection.Input);
            p.Add("minmonth_", offer.Minmonth, DbType.Int32, ParameterDirection.Input);

            var result = _dbContext.Connection.Execute(sql, p);
        }

        public List<LoaneeMain> GetLoansRandomly()
        {

            var sql = @"
        SELECT *
        FROM (
            SELECT GPOFFER.OFFERID, GPOFFER.TOTALMONTHS, GPOFFER.DESCRIPTIONS, GPOFFER.MINMONTH,
                GPCATEGORY.CATEGORYID, GPCATEGORY.CATEGORYNAME,
                GPLENDERSTORE.LENDERID, GPLENDERSTORE.COMMERCIALREGISTER,
                GPLENDERSTORE.COMPANYSIZE, GPLENDERSTORE.SITEURL,
                GPUSER.USERID, GPUSER.FIRSTNAME, GPUSER.LASTNAME, GPUSER.EMAIL,
                GPUSER.PHONENUM, GPUSER.ADDRESS, GPUSER.USERIMAGE
            FROM GPOFFER
            LEFT JOIN GPCATEGORY ON GPOFFER.CATEGORYID = GPCATEGORY.CATEGORYID
            LEFT JOIN GPLENDERSTORE ON GPOFFER.LENDERID = GPLENDERSTORE.LENDERID
            LEFT JOIN GPUSER ON GPLENDERSTORE.LENDERUSERID = GPUSER.USERID
            WHERE GPLENDERSTORE.LENDERID IN (
                SELECT DISTINCT GPLENDERSTORE.LENDERID
                FROM GPLENDERSTORE
            )
            ORDER BY DBMS_RANDOM.VALUE
        )
        WHERE ROWNUM <= 3";

            IEnumerable<LoaneeMain> result = _dbContext.Connection.Query<LoaneeMain>(sql);
            return result.ToList();
        }
    }
}
