using Dapper;
using Pos.Application.Extensions.Helper;
using Pos.Model.Model.Proc;
using Pos.Model.Model.Table;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Vido.Core.Service
{
    public interface IStoreService : IEntityService<RDStore>
    {
        FT_Store GetStoreByID(int storeID);
    }
    public class StoreService : POSEntityService<RDStore>, IStoreService
    {
        public FT_Store GetStoreByID(int storeID)
        {
           return _connection.AutoConnect().SqlFirstOrDefault<FT_Store>($"select * from FT_Store() where StoreID={storeID}");
        }
    }
}
