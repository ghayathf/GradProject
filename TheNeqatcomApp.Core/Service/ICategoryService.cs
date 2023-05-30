using TheNeqatcomApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheNeqatcomApp.Core.Service
{
    public interface ICategoryService
    {
        List<Gpcategory>GetAllCategories();
        Gpcategory GetCategoryById(int id);
        void CreateCategory(Gpcategory gpcategory);
        void UpdateCategory(Gpcategory gpcategory);
        void DeleteCategory(int id);
    }
}
