using DUDU.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Service;
using Vido.Model.Model.Comon;

namespace Vido.Core.Event
{
    public class ItemEvent
    {
        private readonly IItemService _iItemService;
        public ItemEvent(IItemService iItemService)
        {
            _iItemService = iItemService;
        }
        public ResultJs<int> AddItem(int rvcNo, AddItemRequest request)
        {
            ResultJs<int> result = new ResultJs<int>();
            try
            {

                result.status = 200;
                result.data = _iItemService.AddItem(rvcNo, request);
            }
            catch (Exception e)
            {
                result.status = 400;
            }
            return result;
        }

        public ResultJs<List<Item>> GetAllItem(int rvcNo)
        {
            ResultJs<List<Item>> result = new ResultJs<List<Item>>();
            try
            {
                var data = _iItemService.GetAllItem(rvcNo);
                result.status = 200;
                result.data = data;
            }
            catch (Exception e)
            {
                result.status = 400;
            }
            return result;
        }

        public ResultJs<ItemViewModel> GetItemDetail(int rvcNo, long itemID)
        {
            ResultJs<ItemViewModel> result = new ResultJs<ItemViewModel>();
            try
            {
                var data = _iItemService.GetItemDetail(rvcNo, itemID);
                result.status = 200;
                result.data = data;
            }
            catch (Exception e)
            {
                result.status = 400;
            }
            return result;
        }

        public ResultJs<bool> EditItem(int rvcNo, EditItemRequest request)
        {
            ResultJs<bool> result = new ResultJs<bool>();
            try
            {
                _iItemService.EditItem(rvcNo, request);
                result.status = 200;
                result.data = true;
            }
            catch (Exception e)
            {
                result.status = 400;
                result.data = false;
            }
            return result;
        }

        public ResultJs<bool> DeleteItem(int rvcNo, long itemID)
        {
            ResultJs<bool> result = new ResultJs<bool>();
            try
            {
                _iItemService.DeleteItem(rvcNo, itemID);
                result.status = 200;
                result.data = true;
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
