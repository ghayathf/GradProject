using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Repository
{
    public interface ICategoryRepository
    {
        List<Gpcategory>GetAllCategories();
        Gpcategory GetCategoryById(int id);
        void CreateCategory(Gpcategory gpcategory);
        void UpdateCategory(Gpcategory gpcategory);
        void DeleteCategory(int id);
    }
}
