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
    public interface ITicketService : IEntityService<RDStore>
    {
        List<LoadCategoryRsp> LoadCategory(int RVCNo);
        List<LoadItemRsp> LoadItemByStore(int RVCNo);
        List<Employee> LoadEmpByStore(int RVCNo); 
        RDPara GetParaByRCVNo(int RVCNo, string ParaName);
        Task P_UpdateTimeBeforeCloseBill(int RVCNo, decimal EmpId, decimal CheckNo);
        Task BeforeCloseBil(int RVCNo, decimal AppointmentID, decimal CustomerID, decimal CheckNo);
        Task CloseBill(int RVCNo, decimal CheckNo);
        Task CloseBill_RunAfter(int RVCNo, decimal CheckNo);
        Task P_LogTicket(string Action, decimal SaleId, decimal AptId, int RVCNo);
        Task Cancel(decimal saleid, decimal AppointmentID, string Resion, int rvcNo);
        List<RDTmpTrn> select_RDTmpTrn_By_OrgAppointment(decimal OrgAppointment, int RVCNo);
        SPOS_Appointment Load_SPOSAppointment(decimal AppointmentID, int RVCNo);
        List<SPOS_APPOINTMENT_DETAIL> Load_SPOS_APPOINTMENT_DETAIL(decimal AppointmentID, int RVCNo);
        IEnumerable<POS_ITEMS> getPosItemByITemId(int RVCNo, long ItemID);
        IEnumerable<POS_ITEMS> getPosItemByRVCNo(int RVCNo);
        decimal Insert_SPOS_DETAIL(SPOS_APPOINTMENT_DETAIL item);
        decimal Insert_TmpTrn(RDTmpTrn item);
        decimal Insert_RDGiftCardDetail(RDGiftCardDetail item);
        decimal Insert_ResponseTerminal(ResponseTerminal item);
        LoadBillModel LoadBillNotAsync(int rvcNo, decimal orgApptId);
        List<RDTmpTrn> selectRDTmpTrn(string WHERE);
        void UpdatePrice(decimal AppointmentDetailID, decimal NewPrice, int RVCNo);
        void RDVoidDiscountItem(int RVCNo, decimal TrnSeq, decimal OrgTrnSeq, decimal CheckNo);
        void RDVoidItem(int RVCNo, decimal AptDetaiID, decimal CheckNo, decimal AppointmentID);
        void Delete_TipAdjust(decimal checkno, int RVCNo);
        void RDChangeClient(int RVCNo, decimal CusId, decimal AppointmentID);
        void Update_After_ChangeEmployee(decimal sEmployeeID, int RVCNo, decimal OrgTrnSeq, decimal TrnSeq, string CRVValue, long ChairCode, decimal AptDetaiID, bool IsResetDuaration, bool IsRequest);
        int P_DNControlItemDiscount(int RVCNo, decimal EmployeeID, decimal AptDetailId, decimal DiscountValue, bool IsAmount, bool OnSalon, bool IsFee, bool OnSalonTech, bool OnTech, string descName);
        void AddOderTip(int RVCNo, decimal CheckNo, decimal EmployeeID, decimal TipAmount);
        void RemoveOderTip(int RVCNo, decimal CheckNo, decimal TrnSeq);
        List<RDTransactionByRVC> LoadLstOPaymentAvailable(int RVCNo); 
        GetBillPreviewV2 GetBillPreviewV2(int RVCNo, decimal AptId);
        List<ResponseTerminal> getResponseTerminalWithOutSignData(int RVCNo, decimal AptId);
        List<ResponseTerminal> getResponseTerminal(int RVCNo, decimal AptId);
        IEnumerable<SPOS_Appointment> Load_Combine_SPOSAppointment(decimal OrgAptId, int RVCNo);
        Task Update_Note_Ticket(string note, int RVCNo, decimal AppointmentID);
        Task VoidTotalDiscount(decimal CheckNo, int RVCNo);
        Task UpdatePayDiscountTicket(int RVCNo, decimal AptId);
        Task UpdateFullCashPay(int RVCNo, decimal AptId);
        RDTmpTrn CreateEmptyTmpTrn(int RVCNo);
        void addOPayment(int RVCNo, int trncode, decimal value, decimal checkno, string desc, decimal EmpID, decimal SaleId);
    }
    public class TicketService : POSEntityService<RDStore>, ITicketService
    {
        public RDTmpTrn CreateEmptyTmpTrn(int RVCNo)
        {
            var currentDate = DateTime.UtcNow;
            return new RDTmpTrn
            {
                NYear = currentDate.Year,
                NMonth = currentDate.Month,
                NDay = currentDate.Day,
                CheckNo = 0,
                TrnTime = currentDate,
                EmployeeID = 0,
                TrnCode = 0,
                ItemCode = "0",
                TrnDesc = "",
                ItemPrice = 0,
                BaseSub = 0,
                BaseSrc = 0,
                BaseTax = 0,
                BaseTTL = 0,
                BaseSTax = 0,
                SrcNo = 0,
                STaxNo = 0,
                TaxNo = 0,
                TrnQty = 0,
                RVC = 0,
                AuthorCashier = 0,
                Status = 0,
                CategoryCode = 0,
                AppointmentID = 0,
                Split = 0,
                OrderNo = 0,
                ICost = 0,
                DscRef = 0,
                ItemDsc = 0,
                OrgCheckNo = 0,
                OrgAppointment = 0,
                AppointmentDetailID = 0,
                RefundRef = "0",
                RVCNo = RVCNo
            };
        }
        public async Task Cancel(decimal saleid, decimal AppointmentID, string Resion, int rvcNo)
        {
            if (AppointmentID > 0)
            {
                string sql = $"exec dbo.P_CancelProcess '{AppointmentID}',N'{Resion}','{saleid}',{rvcNo}";
                await _connection.AutoConnect().SqlExecuteAsync(sql);
            }
        }
        public GetBillPreviewV2 GetBillPreviewV2(int RVCNo, decimal AptId)
        {
            string x = $"Exec P_GetBillPreviewV2  {AptId},{RVCNo}";
            return _connection.AutoConnect().SqlFirstOrDefault<GetBillPreviewV2>(x);
        }
        public List<ResponseTerminal> getResponseTerminal(int RVCNo, decimal AptId)
        {
            string sql = $"select * from ResponseTerminal with (nolock) where RVCno={RVCNo} and CheckNo = {AptId}";
            return _connection.AutoConnect().SqlQuery<ResponseTerminal>(sql).ToList();
        }
        public List<ResponseTerminal> getResponseTerminalWithOutSignData(int RVCNo, decimal AptId)
        {
            string sql = $"select ID, RefId, RegisterId, InvNum, ResultCode, RespMSG, Message, AuthCode, PNRef, PaymentType,  Token, TransType, TrnSeq, CheckNo, CreationDate,\r\nCardType, AcntLast4, Name, BatchNum, Amount, Tip, TransNum, StaffId, AptId,\r\nIsBatch, EntryType, AID, AppName, TVR, TSI, TerminalName, IsVoided,RVCNo, MerchantID, CreditType, IsPending, TransCode, TerminalId, IsTip, RemainTip,\r\nIsDivideTip, Hash, ApprovalDate, IsPaymentOnline, ApprovalCode, OrderId, Currency,BatchCode, BatchId, TransactionIdentifier, EcrTransID, OrigECRRefNum from ResponseTerminal with (nolock) where RVCno={RVCNo} and CheckNo = {AptId}";
            return _connection.AutoConnect().SqlQuery<ResponseTerminal>(sql).ToList();
        }
        public List<LoadCategoryRsp> LoadCategory(int RVCNo)
        {
            return _connection.AutoConnect().SqlQuery<LoadCategoryRsp>($"Exec P_LoadCategory {RVCNo}").ToList();
        }
        public List<LoadItemRsp> LoadItemByStore(int RVCNo)
        {
            return _connection.AutoConnect().SqlQuery<LoadItemRsp>($"Exec P_LoadItemByStore {RVCNo}").ToList();
        }
        public List<Employee> LoadEmpByStore(int RVCNo)
        {
            return _connection.AutoConnect().SqlQuery<Employee>($"Exec P_GetListEmployee {RVCNo}").ToList();
        }
        public RDPara GetParaByRCVNo(int RVCNo, string ParaName)
        {
            string query = $"select * from RDPara with(nolock) where RVCNo={RVCNo} and ParaName =N'{ParaName}'";
            return _connection.AutoConnect().SqlFirstOrDefault<RDPara>(query);
        }
        public SPOS_Appointment Load_SPOSAppointment(decimal AppointmentID, int RVCNo)
        {
            var query = $"EXEC P_LoadSPOSAppointment '{AppointmentID}','{RVCNo}'";
            return _connection.AutoConnect().SqlFirstOrDefault<SPOS_Appointment>(query);
        }
        public List<SPOS_APPOINTMENT_DETAIL> Load_SPOS_APPOINTMENT_DETAIL(decimal AppointmentID, int RVCNo)
        {
            var query = $"select * from SPOS_APPOINTMENT_DETAIL with (nolock) where RVCNo={RVCNo} and  AppointmentID=   {AppointmentID}";
            return _connection.AutoConnect().SqlQuery<SPOS_APPOINTMENT_DETAIL>(query).ToList();
        }
        public IEnumerable<POS_ITEMS> getPosItemByITemId(int RVCNo, long ItemID)
        {
            string sql = $"select * from POS_ITEMS with (nolock)  where  RVCNo = {RVCNo} and ItemID = {ItemID}";
            return _connection.AutoConnect().SqlQuery<POS_ITEMS>(sql);
        }
        public IEnumerable<POS_ITEMS> getPosItemByRVCNo(int RVCNo)
        {
            string sql = $"select * from POS_ITEMS with (nolock)  where RVCNo = 0 or RVCNo = {RVCNo}";
            return _connection.AutoConnect().SqlQuery<POS_ITEMS>(sql);
        }


        public decimal Insert_SPOS_DETAIL(SPOS_APPOINTMENT_DETAIL item)
        {
            return _connection.AutoConnect().Insert<long, SPOS_APPOINTMENT_DETAIL>(item);
        }
        public decimal Insert_TmpTrn(RDTmpTrn item)
        {
            return _connection.AutoConnect().Insert<long, RDTmpTrn>(item);
        }
        public decimal Insert_ResponseTerminal(ResponseTerminal item)
        {
            return _connection.AutoConnect().Insert<long, ResponseTerminal>(item);
        }
        public decimal Insert_RDGiftCardDetail(RDGiftCardDetail item)
        {
            return _connection.AutoConnect().Insert<long, RDGiftCardDetail>(item);
        }
        public LoadBillModel LoadBillNotAsync(int rvcNo, decimal orgApptId)
        {
            return _connection.AutoConnect().SqlFirstOrDefault<LoadBillModel>(" exec P_TicketReloadBill @orgApptId, @rvcNo", new { orgApptId = orgApptId, rvcNo = rvcNo });
        }
        public int P_DNControlItemDiscount(int RVCNo, decimal EmployeeID, decimal AptDetailId, decimal DiscountValue, bool IsAmount, bool OnSalon, bool IsFee, bool OnSalonTech, bool OnTech, string descName)
        {
            string sql = $@"exec P_DNControlItemDiscount  {RVCNo} ,'{descName}',{AptDetailId},{DiscountValue * -1},{IsAmount},{OnSalon},{OnSalonTech},{OnTech},{IsFee},{EmployeeID}";
            return _connection.AutoConnect().SqlFirstOrDefault<int>(sql);
        }

        public List<RDTmpTrn> selectRDTmpTrn(string WHERE)
        {
            string sql = $"select * from RDTmpTrn with (nolock)  where {WHERE}";
            return _connection.AutoConnect().SqlQuery<RDTmpTrn>(sql).ToList();
        }
        public void UpdatePrice(decimal AppointmentDetailID, decimal NewPrice, int RVCNo)
        {
            string x = $"Exec P_UpdatePrice {AppointmentDetailID}, {NewPrice},{RVCNo}";
            _connection.AutoConnect().SqlExecute(x);
        }
        public void RDVoidDiscountItem(int RVCNo, decimal TrnSeq, decimal OrgTrnSeq, decimal CheckNo)
        {
            string sql = $"exec P_RDVoidDiscountItem {RVCNo},{TrnSeq},{OrgTrnSeq},{CheckNo}";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public void RDVoidItem(int RVCNo, decimal AptDetaiID, decimal CheckNo, decimal AppointmentID)
        {
            string sql = $"exec P_RDVoidItem {RVCNo},{AptDetaiID},{CheckNo},{AppointmentID}";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public void Update_After_ChangeEmployee(decimal sEmployeeID, int RVCNo, decimal OrgTrnSeq, decimal TrnSeq, string CRVValue, long ChairCode, decimal AptDetaiID, bool IsResetDuaration, bool IsRequest)
        {
            string sql = $@" update RDTmpTrn set EmployeeID ={sEmployeeID}
                                                , IDSaler = (case when Type not in(1,4)  then  {sEmployeeID} else 0 end) 
                                                ,IsResetDuration='{IsResetDuaration}' where RVCNo='{RVCNo}' 
                                                and (TrnSeq = {TrnSeq} or (RefundRef='{OrgTrnSeq}' or ExtraRef='{OrgTrnSeq}'  and '{OrgTrnSeq}'!='0'))";
            sql = sql + "\r\n";
            sql = sql + $@"Update SPOS_APPOINTMENT_DETAIL set CRVValue={CRVValue}, ChairCode={ChairCode}, EmployeeID ={sEmployeeID}
                                                ,IsRequestTech ='{IsRequest}'
                                                , LastChange =getutcdate()  where  RVCNo='{RVCNo}' and (OrgAptDetail ={AptDetaiID} or AppointmentDetailID={AptDetaiID})";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public void Delete_TipAdjust(decimal checkno, int RVCNo)
        {
            string sql = $"delete from RDTipsAdjustment where  RVCNo='{RVCNo}' and CheckNo='{checkno}'";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public void RDChangeClient (int RVCNo, decimal CusId, decimal AppointmentID)
        {
            string sql = $"exec RDChangeClient {CusId},{AppointmentID},{RVCNo}";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public void AddOderTip(int RVCNo, decimal CheckNo, decimal EmployeeID, decimal TipAmount)
        {
            string sql = $"exec P_AddOderTip '{RVCNo}',{CheckNo},{EmployeeID},{TipAmount}";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public void RemoveOderTip(int RVCNo, decimal CheckNo, decimal TrnSeq)
        {
            string sql = $@"delete from RDTipsAdjustment where checkno= {CheckNo} and RVCNo='{RVCNo}'
            delete from RDTmpTrn where TrnSeq = {TrnSeq}";
            _connection.AutoConnect().SqlExecute(sql);
        }
        public List<RDTransactionByRVC> LoadLstOPaymentAvailable(int RVCNo)
        {
            string x = $"exec P_LstOPaymentAvailable {RVCNo}";
            List<RDTransactionByRVC> resut = _connection.AutoConnect().SqlQuery<RDTransactionByRVC>(x).ToList();
            return resut;
        }
        public void addOPayment(int RVCNo, int trncode, decimal value, decimal checkno, string desc, decimal EmpID, decimal SaleId)
        {
            string sql = $"exec P_addOPayment '{checkno}','{trncode}','{(desc ?? "").Trim()}','{value}','{EmpID}','{SaleId}','{RVCNo}'";
            _connection.AutoConnect().SqlExecute(sql);
        }

        public IEnumerable<SPOS_Appointment> Load_Combine_SPOSAppointment(decimal OrgAptId, int RVCNo)
        {
            var query = $"select * from SPOS_APPOINTMENT with (nolock) where RVCNo={RVCNo} and  CheckNo=   {OrgAptId}";
            return _connection.AutoConnect().SqlQuery<SPOS_Appointment>(query);
        }

        public List<RDTmpTrn> select_RDTmpTrn_By_OrgAppointment(decimal OrgAppointment, int RVCNo)
        {
            string sql = $"select * from RDTmpTrn with (nolock)  where RVCNo ={RVCNo} and OrgAppointment={OrgAppointment} ";
            return _connection.AutoConnect().SqlQuery<RDTmpTrn>(sql).ToList();
        }

        public async Task P_UpdateTimeBeforeCloseBill(int RVCNo, decimal EmpId, decimal CheckNo)
        {
            await _connection.AutoConnect().SqlExecuteAsync($"Exec P_UpdateTimeBeforeCloseBill {EmpId},{CheckNo},{RVCNo}");
        }
        public async Task P_LogTicket(string Action, decimal SaleId, decimal AptId, int RVCNo)
        {
            await _connection.AutoConnect().SqlExecuteAsync($"EXEC P_LogTicket '{Action}',{SaleId},{AptId},{RVCNo}");
        }
        public async Task BeforeCloseBil(int RVCNo, decimal AppointmentID, decimal CustomerID, decimal CheckNo)
        {
            await _connection.AutoConnect().SqlExecuteAsync($" exec P_BeforeCloseBill {CheckNo}, {AppointmentID},{CustomerID},{RVCNo}");
        }
        public async Task CloseBill(int RVCNo, decimal CheckNo)
        {
            await _connection.AutoConnect().SqlExecuteAsync($"exec P_CloseBill {CheckNo},{RVCNo}");
        }
        public async Task CloseBill_RunAfter(int RVCNo, decimal CheckNo)
        {
            System.Diagnostics.Debug.WriteLine("Run CloseBill_RunAfter");
            await _connection.AutoConnect().SqlExecuteAsync($" exec P_CloseBill_RunAfter {CheckNo},{RVCNo}");
            System.Diagnostics.Debug.WriteLine("End CloseBill_RunAfter");
        }
        public async Task Update_Note_Ticket(string note, int RVCNo, decimal AppointmentID)
        {
            string sql = $"Update SPOS_APPOINTMENT set AptComment = '{note}' where  RVCNo ={RVCNo} AND  AppointmentID ={AppointmentID}";
            await _connection.AutoConnect().SqlExecuteAsync(sql);
        }

        public async Task VoidTotalDiscount(decimal CheckNo, int RVCNo)
        {
            string sql = $@"P_RDVoidDiscountAllBill {RVCNo},{CheckNo}";
            await _connection.AutoConnect().SqlExecuteAsync(sql);
        }

        public async Task UpdatePayDiscountTicket(int RVCNo, decimal AptId)
        {
            string sql = $"exec P_UpdatePayDiscount {AptId},{RVCNo}";
            await _connection.AutoConnect().SqlExecuteAsync(sql);
        }
        public async Task UpdateFullCashPay(int RVCNo, decimal AptId)
        {
            string sql = $"exec P_UpdateFullCashPay '{AptId}',{RVCNo}";
            await _connection.AutoConnect().SqlExecuteAsync(sql);
        }
    }
}
