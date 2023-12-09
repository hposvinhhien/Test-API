using Dapper;
using Pos.Application.Extensions.Helper;
using Pos.Model.Model.Table;
using Promotion.Application.Extensions;
using Promotion.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Proc;
using Vido.Model.Model.Table;
using Vido.Model.Model.ViewModel;

namespace Vido.Core.Service
{
    public interface IGiftcardService : IEntityService<RDStore>
    {
        string CheckSeriGiftCard(string IDGiftCard, decimal EmpId, int RVCNo);
        IEnumerable<RDGiftCardDetail> GetRDGiftCardDetail(int RVCNo, string WHERE);
        IEnumerable<RDGiftCard> GetRDGiftCard(int RVCNo, string WHERE);
        int DelRDGiftCardDetail(int RVCNo, string WHERE);
        void RDUpdGiftCards(int RVCNo, string Gcode);
        void RDUpdGiftCard(decimal IDGiftCard);
        decimal PostGiftCard(string SeriNumber, long ItemID, decimal Amount, decimal OrgAptID, decimal AptID, decimal EmpID, decimal SellID, int RVCNo);
        decimal GetIDGiftCard(string SeriNumber, int RVCNo, int MasterStore);
        void InsertGiftCard(decimal BaseSub, string SeriNumber, decimal OrgAptID, decimal TrnSeq, DateTime? litmit, int RVCNo, string source = "offline");
        void updateLimitGiftcard(DateTime? litmit, decimal IDGirftCard);
        void InsertFullGiftCard(decimal Amount, decimal IDGirftCard, decimal AptID, decimal OrgAptID, decimal TrnSeq, decimal RVCNo, decimal MasterStore);
    }
    public class GiftcardService : POSEntityService<RDStore>, IGiftcardService
    {

        public string CheckSeriGiftCard(string IDGiftCard, decimal EmpId, int RVCNo)
        {
            return _connection.AutoConnect().SqlFirstOrDefault<string>($"Exec P_CheckGiftCodeGiftCard '{IDGiftCard}',{EmpId},{RVCNo}");
        }
        public IEnumerable<RDGiftCardDetail> GetRDGiftCardDetail(int RVCNo, string WHERE)
        {
            string sql = $"select * from RDGiftCardDetail with(nolock) where RVCNo={RVCNo} and {WHERE}";
            return _connection.AutoConnect().SqlQuery<RDGiftCardDetail>(sql);
        }
        public IEnumerable<RDGiftCard> GetRDGiftCard(int RVCNo, string WHERE)
        {
            string sql = $"select * from RDGiftCard with(nolock) where RVCNo={RVCNo} and {WHERE}";
            return _connection.AutoConnect().SqlQuery<RDGiftCard>(sql);
        }
        public int DelRDGiftCardDetail(int RVCNo, string WHERE)
        {
            string sql = $"DELETE FROM RDGiftCardDetail WHERE RVCNo={RVCNo} and {WHERE}";
            return _connection.AutoConnect().SqlExecute(sql);
        }
        public void RDUpdGiftCards(int RVCNo, string Gcode)
        {
            string sql = $"exec P_RDUpdGiftCards '{Gcode}',{RVCNo}";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public void RDUpdGiftCard(decimal IDGiftCard)
        {
            string sql = $"exec P_RDUpdGiftCard '{IDGiftCard}'";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public decimal PostGiftCard(string SeriNumber, long ItemID, decimal Amount, decimal OrgAptID, decimal AptID, decimal EmpID, decimal SellID, int RVCNo)
        {
            string sql = $"Exec P_PostGiftCard '{SeriNumber}',{ItemID},{Amount},{OrgAptID},{AptID},{EmpID},{SellID},{RVCNo}";
            return _connection.AutoConnect().SqlFirstOrDefault<decimal>(sql);
        }
        public decimal GetIDGiftCard(string SeriNumber, int RVCNo, int MasterStore)
        {
            string sql = $@"exec P_RDGiftCardByMultiStore '{SeriNumber}',{RVCNo},{MasterStore}";
            var ck = _connection.AutoConnect().SqlFirstOrDefault<RDGiftCard>(sql);
            if (ck == null)
                return 0;
            return ck.ID;
        }
        public void InsertGiftCard(decimal BaseSub, string SeriNumber, decimal OrgAptID, decimal TrnSeq, DateTime? litmit, int RVCNo, string source = "offline")
        {
            string sql = $@"exec P_InsertGiftCard '{SeriNumber}','{BaseSub}', {OrgAptID} ,{TrnSeq}, '{litmit}' ,{RVCNo}, '{source}'";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public void updateLimitGiftcard(DateTime? litmit, decimal IDGirftCard)
        {
            string sql = $@"update RDGiftCard set LimitTo = nullif('{litmit}','') where ID ='{IDGirftCard}'";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public void InsertFullGiftCard(decimal Amount, decimal IDGirftCard, decimal AptID, decimal OrgAptID, decimal TrnSeq, decimal RVCNo, decimal MasterStore)
        {
            string sql = $@"EXEC P_InsertFullGiftCard '{Amount}','{IDGirftCard}','{AptID}','{OrgAptID}','{TrnSeq}','{RVCNo}','{MasterStore}'";
            _connection.AutoConnect().SqlExecute(sql);
        }
    }
}
