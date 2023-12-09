using Dapper;
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
using Vido.Model.Model.ViewModel;

namespace Vido.Core.Service
{
    public interface IClientService : IEntityService<Category>
    {

        CUS_CUSTOMER RegisterClient(FindAndRegCustomerModel model);
        CUS_CUSTOMER Get(int rvcNo, decimal id);
        IEnumerable<CUS_CUSTOMER> GetAll(int rvcNo, CustomerRequest model);
        int UpdateClient(CUS_CUSTOMER customer);
        Task DeleteClient(int rvcNo, decimal id);
    }

    public class ClientService : POSEntityService<Category>, IClientService
    {
        public ClientService() { }
        public ClientService(IDbConnection db) : base(db)
        {

        }
        public CUS_CUSTOMER RegisterClient(FindAndRegCustomerModel model)
        {
            string sql = $"exec RDClientFastRegister '{model.firstName}','{model.lastName}','{model.phone}',{model.rvcNo}";
            return _connection.AutoConnect().SqlFirstOrDefault<CUS_CUSTOMER>(sql);
        }
        public CUS_CUSTOMER Get(int rvcNo, decimal id)
        {

                return _connection.AutoConnect().SqlFirstOrDefault<CUS_CUSTOMER>($@"P_CustomerGet @CustomerId, @RVCNo",
                 new
                 {
                     RVCNo = rvcNo,
                     CustomerId = id
                 });
        }

        public IEnumerable<CUS_CUSTOMER> GetAll(int rvcNo, CustomerRequest model)
        {
            model.InfoSearch.ReplacePhone();

            return _connection.AutoConnect().SqlQuery<CUS_CUSTOMER>($@" [P_CustomerGetAll]  @Begin, @RecordReturn, @InfoSearch, {rvcNo}, @Sort", 
                model);
        }

        public int UpdateClient(CUS_CUSTOMER customer)
        {
            return _connection.AutoConnect().Update(customer);
        }

        public async Task DeleteClient(int rvcNo, decimal id)
        {

             await _connection.AutoConnect().SqlExecuteAsync($@"P_DeleteCustomer @CustomerId, @RVCNo",
             new
             {
                 RVCNo = rvcNo,
                 CustomerId = id
             });
        }
    }
}
