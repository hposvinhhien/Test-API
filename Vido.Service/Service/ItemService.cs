using Dapper;
using DUDU.Models.Items;
using Pos.Application.Extensions.Helper;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Table;

namespace Vido.Core.Service
{
    public interface IItemService : IEntityService<Item>
    {
        int AddItem(int rvcNo, AddItemRequest request);
        List<Item> GetAllItem(int rvcNo);
        List<Item> SearchItemByKey(int rvcNo, string key);
        ItemViewModel GetItemDetail(int rvcNo, long itemID);
        bool EditItem(int rvcNo, EditItemRequest request);
        bool DeleteItem(int rvcNo, long itemID);
        int GetGiftCardCodeItem(int RVCNo);
        Task<bool> CheckServiceByEmployeeID(decimal ItemCode, decimal EmployeeID, int RVCNo = 0, decimal? appointmentId = 0, decimal empIdOld = 0, bool isCheckCategory = true, int copyRebook = 0);
    }
    public class ItemService : POSEntityService<Item>, IItemService
    {
        public int AddItem(int rvcNo, AddItemRequest request)
        {
                string commissionRateXML = "<DATA>";
                commissionRateXML = commissionRateXML + "</DATA>";

                string query = $"P_AddItem {request.CategoryID},N'{request.ItemName}',{request.Price},{request.Duration},{request.Tax}," +
                    $"{request.CashDiscount},'{request.ListStaff}','{request.ColorCode}','{commissionRateXML}',{rvcNo},{request.TurnCount}," +
                    $"{(request.NotCountTurn ? 1 : 0)},{(request.ShowOnCheckInApp ? 1 : 0)},{(request.ShowOnBookOnline ? 1 : 0)},{request.ProductChargeAmount},{(request.IsProductChargePercentage ? 1 : 0)},N'{request.Description}',{request.Commission}";


                return _connection.AutoConnect().SqlQuery<int>(query).FirstOrDefault();
        }

        public bool DeleteItem(int rvcNo, long itemID)
        {
            try
            {
                string query = $"update POS_ITEMS set IsDeleted=1 where RVCNo={rvcNo} and ItemID={itemID}";
                _connection.AutoConnect().SqlExecute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditItem(int rvcNo, EditItemRequest request)
        {
            try
            {
                string commissionRateXML = "<DATA>";
                commissionRateXML = commissionRateXML + "</DATA>";
                string query = $"P_EditItem {request.ItemID},{request.CategoryID},N'{request.ItemName}',{request.Price},{request.Duration},{request.Tax}," +
                    $"{request.CashDiscount},'{request.ListStaff}','{request.ColorCode}','{commissionRateXML}',{rvcNo},{request.TurnCount}," +
                     $"{(request.NotCountTurn ? 1 : 0)},{(request.ShowOnCheckInApp ? 1 : 0)},{(request.ShowOnBookOnline ? 1 : 0)},{request.ProductChargeAmount},{(request.IsProductChargePercentage ? 1 : 0)},N'{request.Description}',{request.Commission}";

                _connection.Execute(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int GetGiftCardCodeItem(int RVCNo)
        {
            string sql = $@"select * from POS_ITEMS with(nolock) where RVCNo={RVCNo} and IsGiftCard=1";
            var query = _connection.Query<int>(sql).FirstOrDefault();
            return query;
        }

        public List<Item> GetAllItem(int rvcNo)
        {
            try
            {
                string query = $"select * from POS_Items with(nolock) where RVCNo={rvcNo} and isnull(IsDeleted,0)=0";

                List<Item> data = _connection.Query<Item>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                return new List<Item>();
            }
        }
        public List<Item> SearchItemByKey(int rvcNo, string key)
        {
            try
            {
                string query = $"select * from POS_Items with(nolock) where RVCNo=${rvcNo} and isnull(IsDeleted,0)=0 and {key}";

                List<Item> data = _connection.Query<Item>(query).ToList();
                return data;
            }
            catch (Exception e)
            {
                return new List<Item>();
            }
        }
        public ItemViewModel GetItemDetail(int rvcNo, long itemID)
        {
            try
            {
                string query = $"exec P_GetItemDetail {itemID},{rvcNo}";

                ItemViewModel data = _connection.Query<ItemViewModel>(query).FirstOrDefault();
                return data;
            }
            catch (Exception e)
            {
                return new ItemViewModel();
            }
        }

        public async Task<bool> CheckServiceByEmployeeID(decimal ItemCode, decimal EmployeeID, int RVCNo = 0, decimal? appointmentId = 0, decimal empIdOld = 0, bool isCheckCategory = true, int copyRebook = 0)
        {
            bool Result = true;
            return true;
            if (EmployeeID < 1)
            {
                return true;
            }

            try
            {
                string sql = $@"select dbo.F_CheckItemByEmployee ({EmployeeID},{ItemCode},{RVCNo})";
                if (appointmentId > 0)
                {
                    if (copyRebook == 0)
                        sql = $@"select dbo.F_EmpCheckMakeService({empIdOld},{EmployeeID},{appointmentId},{RVCNo},'{isCheckCategory}')";
                    else sql = $@"select dbo.F_EmpCheckMakeServiceCopyRebook({empIdOld},{EmployeeID},{appointmentId},{RVCNo},'{isCheckCategory}')";
                }

                int sResult = await _connection.QueryFirstAsync<int>(sql);
                if (sResult == 0)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            catch { }
            return Result;
        }

    }
}
