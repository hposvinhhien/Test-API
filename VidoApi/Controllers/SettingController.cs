using DUDU.Models.Items;
using Microsoft.AspNetCore.Mvc;
using Vido.Core.Event;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;

namespace VidoApi.Controllers
{
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly CategoryEvent _categoryEvent; 
        private readonly ItemEvent _itemEvent;

        public SettingController(CategoryEvent categoryEvent, ItemEvent itemEvent)
        {
            _categoryEvent = categoryEvent; 
            _itemEvent = itemEvent;
        }
        [HttpPost("[action]")]
        public ResultJs<bool> AddCategory(AddCategoryRequest request)
        {
            return _categoryEvent.AddCategory(request);
        }
        [HttpGet("{rvcNo}/[action]")]
        public ResultJs<List<Category>> GetListCategory(int rvcNo)
        {
            return _categoryEvent.GetListCategory(rvcNo);
        }
        [HttpGet("{rvcNo}/[action]")]
        public ResultJs<Category> GetCategoryDetail(long categoryID, int rvcNo)
        {
            return _categoryEvent.GetCategoryDetail(categoryID, rvcNo);
        }

        [HttpPut("{rvcNo}/[action]")]
        public ResultJs<bool> EditCategory(int rvcNo, EditCategoryRequest request)
        {
            return _categoryEvent.EditCategory(request);
        }

        [HttpPut("{rvcNo}/[action]")]
        public ResultJs<bool> DeleteCategory(int rvcNo, DeleteCategoryRequest request)
        {
            return _categoryEvent.DeleteCategory(request.CategoryID, request.RVCNo);
        }


        [HttpPost("{rvcNo}/[action]")]
        public ResultJs<int> AddItem(int rvcNo, [FromBody] AddItemRequest request)
        {
            return _itemEvent.AddItem(rvcNo, request);
        }


        [HttpGet("{rvcNo}/[action]")]
        public ResultJs<List<Item>> GetAllItem(int rvcNo)
        {
            return _itemEvent.GetAllItem(rvcNo);
        }

        [HttpGet("{rvcNo}/[action]")]
        public ResultJs<ItemViewModel> GetItemDetail(int rvcNo, long itemID)
        {
            return _itemEvent.GetItemDetail(rvcNo, itemID);
        }

        [HttpPut("{rvcNo}/[action]")]
        public ResultJs<bool> EditItem(int rvcNo, EditItemRequest request)
        {
            return _itemEvent.EditItem(rvcNo, request);
        }

        [HttpPut("{rvcNo}/[action]")]
        public ResultJs<bool> DeleteItem(int rvcNo, DeleteItemRequest request)
        {
            return _itemEvent.DeleteItem(rvcNo, request.ItemID);
        }
    }
}
