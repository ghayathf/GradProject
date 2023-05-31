using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Infra.Common;

namespace TheNeqatcomApp.Infra.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDBContext _dbcontext;

        public CategoryRepository(IDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public void CreateCategory(Gpcategory gpcategory)
        {
            string query = "INSERT INTO GPCATEGORY (CategoryNAME, CategoryIMAGE) VALUES (@CategoryName, @CategoryImage)";
            var parameters = new
            {
                CategoryName = gpcategory.Categoryname,
                CategoryImage = gpcategory.Categoryimage
            };

            _dbcontext.Connection.Execute(query, parameters);
        }


        public void DeleteCategory(int id)
        {
            string query = "DELETE FROM GPCATEGORY WHERE CategoryID = @CategoryId";
            var parameters = new { CategoryId = id };


            _dbcontext.Connection.Execute(query, parameters);
        }
        public List<Gpcategory> GetAllCategories()
        {
            string query = "SELECT * FROM GPCATEGORY";
            IEnumerable<Gpcategory> categories = _dbcontext.Connection.Query<Gpcategory>(query);
            return categories.ToList();
        }


        public Gpcategory GetCategoryById(int id)
        {
            string query = "SELECT * FROM GPCATEGORY WHERE CategoryID = @Id";
            var parameters = new { Id = id };
            Gpcategory category = _dbcontext.Connection.QueryFirstOrDefault<Gpcategory>(query, parameters);
            return category;
        }

        public void UpdateCategory(Gpcategory gpcategory)
        {
            string query = "UPDATE GPCATEGORY SET CategoryNAME = @CategoryName, CategoryIMAGE = @CategoryImage WHERE CategoryID = @CategoryId";
            var parameters = new
            {
                CategoryName = gpcategory.Categoryname,
                CategoryImage = gpcategory.Categoryimage,
                CategoryId = gpcategory.Categoryid
            };

            _dbcontext.Connection.Execute(query, parameters);
        }

    }
}
