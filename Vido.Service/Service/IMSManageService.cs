using NetVips;
using Pos.Application.Extensions.Helper;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Proc;
using Vido.Model.Model.Request;
using Vido.Model.Model.Table;

namespace Vido.Core.Service
{
    public interface IIMSManageService : IEntityService<Category>
    {
        int ResgisStore(string sName, string sPhone, string sEmail, string sPass);
        void UpdateStoreStatus(int status, int RVCNo);
        void DeleteStore(int RVCNo);
        List<POS_CONFIG> LoadStoreStatus();
    }
    public class IMSManageService : POSEntityService<Category>, IIMSManageService
    {
        public IMSManageService() { }
        public IMSManageService(IDbConnection db) : base(db)
        {

        }
        public int ResgisStore(string sName, string sPhone, string sEmail, string sPass)
        {
                int newRVCNo = _connection.AutoConnect().SqlFirstOrDefault<int>("Exec CreateNewStore  @sName, @sPhone, @sEmail, @sPass",
                    new { sName,sPhone,sEmail,sPass }
                );
            return newRVCNo;
        }
        public void UpdateStoreStatus(int status, int RVCNo)
        {
            string sql = $"Update RD_STORE set status = {status} Where RVCNo = {RVCNo}";
            _connection.AutoConnect().SqlExecute("Update RD_STORE set status = @status Where RVCNo = @RVCNo",new { status, RVCNo});
        }
        public void DeleteStore(int RVCNo)
        {
            _connection.AutoConnect().SqlExecute("exec [RDReset] @RVCNo", new { RVCNo });
        }
        public List<POS_CONFIG> LoadStoreStatus()
        {
            return _connection.AutoConnect().SqlQuery<POS_CONFIG>("select A.*, B.status,B.Email,B.Password, C.UTCName from POS_CONFIG A left join RD_STORE B on A.RVCNo = B.StoreID left join RDDefRVCList C on A.RVCNo = C.RVCNo").ToList();   
        }

    }
}
