using Microsoft.AspNetCore.Mvc.Filters;
using Vido.Core.Extensions.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Service;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;

namespace Vido.Core.Event
{
    public class CategoryEvent
    {
        private readonly IFileProvider _fileProvider;
        private readonly ICategoryService _categoryService; 
        public CategoryEvent(ICategoryService categoryService, IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            _categoryService = categoryService;
        }

        public ResultJs<bool> AddCategory(AddCategoryRequest request)
        {
            ResultJs<bool> result = new ResultJs<bool>();
            try
            {
                if ((request.ImageBase64??"").Contains("base64"))
                {
                    Guid id_img = Guid.NewGuid();
                    request.ImageName = id_img.ToString();
                    _fileProvider.SaveImageFromBase64(Const.Path.IMAGE_CATEGORIES, request.ImageBase64, 200, 200, id_img.ToString());
                    request.ImageName = request.ImageName + ".webp";
                }
                _categoryService.AddCategory(request);
                result.data = true;
                result.status = 200;
            }
            catch (Exception e)
            {
                result.status = 400;
                result.data = false;
            }
            return result;
        }
        public ResultJs<List<Category>> GetListCategory(int rvcNo)
        {
            ResultJs<List<Category>> result = new ResultJs<List<Category>>();
            try
            {
                var data = _categoryService.GetListCategory(rvcNo);
                result.data = data;
                result.status = 200;
            }
            catch (Exception e)
            {
                result.status = 400;
            }
            return result;
        }

        public ResultJs<Category> GetCategoryDetail(long categoryID, int rvcNo)
        {
            ResultJs<Category> result = new ResultJs<Category>();
            try
            {
                var data = _categoryService.GetCategoryDetail(categoryID, rvcNo);
                data.ImageFileName = PosAppSetting.ImagePath + Const.Path.IMAGE_CATEGORIES + data.ImageFileName;
                result.data = data;
                result.status = 200;
            }
            catch (Exception e)
            {
                result.status = 400;
            }
            return result;
        }

        public ResultJs<bool> EditCategory(EditCategoryRequest request)
        {
            ResultJs<bool> result = new ResultJs<bool>();
            try
            {
                request.ImageName = "";
                if (request.ImageBase64.Contains("base64"))
                {
                    Guid id_img = Guid.NewGuid();
                    request.ImageName = id_img.ToString();
                    _fileProvider.SaveImageFromBase64(Const.Path.IMAGE_CATEGORIES, request.ImageBase64, 200, 200, id_img.ToString());
                    _fileProvider.Delete(Const.Path.IMAGE_CATEGORIES, request.ImageName);
                    request.ImageName = request.ImageName + ".webp";
                }
                _categoryService.EditCategory(request);
                result.data = true;
                result.status = 200;
            }
            catch (Exception e)
            {
                result.status = 400;
                result.data = false;
            }
            return result;
        }

        public ResultJs<bool> DeleteCategory(long categoryID, int rvcNo)
        {
            ResultJs<bool> result = new ResultJs<bool>();
            try
            {
                _categoryService.DeleteCategory(categoryID, rvcNo);
                result.data = true;
                result.status = 200;
            }
            catch (Exception e)
            {
                result.status = 400;
                result.data = false;
            }
            return result;
        }
    }
}
