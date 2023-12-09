using Pos.Application.Extensions.Helper;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;

namespace Vido.Core.Service
{
    public interface ICategoryService : IEntityService<Category>
    {

        bool AddCategory(AddCategoryRequest request);
        bool EditCategory(EditCategoryRequest request);
        List<Category> GetListCategory(int rvcNo);
        Category GetCategoryDetail(long categoryID, int rvcNo);
        bool DeleteCategory(long categoryID, int rvcNo);
    }

    public class CategoryService : POSEntityService<Category>, ICategoryService
    {
        public CategoryService() { }
        public CategoryService(IDbConnection db) : base(db)
        {

        }

        public bool AddCategory(AddCategoryRequest request)
        {
            try
            {
                string query = $"P_AddCategory N'{request.CategoryName}',{request.Type},'{request.Note}',{(request.Status ? 1 : 0)},0,{request.RVCNo},N'{request.ImageName}','{request.Color}',0";
                _connection.AutoConnect().SqlExecute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Category> GetListCategory(int rvcNo)
        {
            try
            {
                string query = $"select * from POS_CATEGORIES with(nolock) where RVCNo={rvcNo} and isnull(IsDeleted,0)=0";
                List<Category> data = _connection.AutoConnect().SqlQuery<Category>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                return new List<Category>();
            }
        }

        public Category GetCategoryDetail(long categoryID, int rvcNo)
        {
            try
            {
                string query = $"select top 1 * from POS_CATEGORIES with(nolock) where RVCNo={rvcNo} and CategoryID={categoryID}";
                Category data = _connection.AutoConnect().SqlFirstOrDefault<Category>(query);
                return data;
            }
            catch (Exception e)
            {
                return new Category();
            }
        }

        public bool EditCategory(EditCategoryRequest request)
        {
            try
            {
                string query = $"P_EditCategory {request.CategoryID},N'{request.CategoryName}',{request.Type},'{request.Note}',{(request.Status ? 1 : 0)},0,{request.RVCNo},N'{request.ImageName}','{request.Color}',0";

                _connection.AutoConnect().SqlExecute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteCategory(long categoryID, int rvcNo)
        {
            try
            {
                string query = $"update POS_CATEGORIES set IsDeleted=1 where RVCNo={rvcNo} and CategoryID={categoryID} ";
                _connection.AutoConnect().SqlExecute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}
