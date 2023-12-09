using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Service;
using Vido.Model.Model.Proc;
using Vido.Model.Model.Comon;
using Vido.Model.Model.Table;
using Vido.Model.Model.Request;
using Vido.Model.Model.ViewModel;
using Promotion.Application.Extensions;

namespace Vido.Core.Event
{
    public class TicketEvent
    {
        private readonly ITicketService _ticketService;
        private readonly IGiftcardService _giftcardService;
        private readonly IClientService _clientService;

        public TicketEvent(ITicketService ticketService, IGiftcardService giftcardService, IClientService clientService)
        {
            _ticketService = ticketService;
            _giftcardService = giftcardService;
            _clientService = clientService;
        }

        public rsData LoadCategory(int RVCNo)
        {
            rsData result = new rsData();
            result.data = _ticketService.LoadCategory(RVCNo);
            result.status = 200;
            return result;
        }
        public rsData LoadEmployee(int RVCNo)
        {
            rsData result = new rsData();
            result.data = _ticketService.LoadEmpByStore(RVCNo);
            result.status = 200;
            return result;
        }
        public rsData LoadItem(int RVCNo)
        {
            rsData result = new rsData();
            result.data = _ticketService.LoadItemByStore(RVCNo);
            result.status = 200;
            return result;
        }
        public async Task<ApiResult> AddItemAsync(decimal EmployeeRequest, decimal AppointmentID, long ItemCode, decimal customPrice, int status, int RVCNo, string lstDetailComBo = "", bool isChangePrice = false, string barCode = ""
                                      , decimal ProdCharge = 0, int itemDuration = 0
                                      , string name = "", decimal orgEmpID = 0, int customType = 0, decimal Turn = 0, bool ChairCodes = false)
        {
            ApiResult res = new ApiResult();
            try
            {
                decimal AptDetails = await AddServiceReturnAptDetailAsync(AppointmentID, ItemCode, EmployeeRequest, customPrice, status, RVCNo, lstDetailComBo, isChangePrice, barCode
                                               , ProdCharge, itemDuration, name, orgEmpID, customType, Turn, ChairCodes ? 1 : 0);
                if (AptDetails > 0)
                {
                    res.Status = true;
                    res.Message = "Add item success";
                    res.ExtraData = AptDetails;
                }
                else
                {
                    res.Status = false;
                    res.Message = "Add item false";
                    res.ExtraData = 0;
                    return res;
                }

            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
                res.ExtraData = 0;
            }
            return res;
        }

        public async Task<decimal> AddServiceReturnAptDetailAsync(decimal AppointmentID, long ItemCode, decimal EmployeeID, decimal customPrice, int status
                                       , int RVCNo, string lstDetailComBo = "", bool isChangePrice = false, string barCode = ""
                                       , decimal ProdCharge = 0, int itemDuration = 0
                                       , string name = "", decimal orgEmpID = 0, int customType = 0, decimal Turn = 0, int ChairCodes = 0
                                      )
        {
            string sql = "";
            //int ActiveWaitingList = await _paraService.GetByKey<int>(RVCNo, "ActiveWaitingList", 0);
            int ActiveWaitingList = int.Parse(_ticketService.GetParaByRCVNo(RVCNo, "ActiveWaitingList").ParaStr);
            decimal Resuilt = 0;
            decimal CheckNo = 0;
            bool IsRequest = false;
            decimal sEmployeeID = EmployeeID;
            var q = _ticketService.Load_SPOSAppointment(AppointmentID, RVCNo);
            var Details = _ticketService.Load_SPOS_APPOINTMENT_DETAIL(AppointmentID, RVCNo);

            if (q != null)
            {

                try
                {
                    CheckNo = q.CheckNo.HasValue ? q.CheckNo.Value : 0;
                }
                catch
                {
                    CheckNo = 0;
                }
                POS_ITEMS it = new POS_ITEMS();
                var itemPos = _ticketService.getPosItemByITemId(RVCNo, ItemCode);
                if (itemPos.Count() != 0)
                {
                    it = itemPos.FirstOrDefault();
                }
                else
                {
                    var posItemLst = _ticketService.getPosItemByRVCNo(RVCNo);
                    if (ItemCode == -1)
                    {
                        it = posItemLst.FirstOrDefault(x => x.CategoryID == -1);

                    }
                    else if (barCode != "")
                    {
                        it = posItemLst.FirstOrDefault(x => x.BarCode == barCode);
                    }
                    else
                    {
                        it = posItemLst.FirstOrDefault(x => x.ItemID == ItemCode);
                    }
                }
                if (it != null)
                {
                    if (sEmployeeID == 0)
                    {
                        sEmployeeID = q.EmployeeID ?? 0;
                        IsRequest = false;
                    }
                    int ChairCode = 0;
                    ChairCode = ChairCodes;
                    if (IsRequest == false)
                    {
                        var cks = (from a in Details
                                   where a.IsRequestTech == true && a.EmployeeID == EmployeeID && a.EmployeeID > 0
                                   select a).FirstOrDefault();
                        if (cks != null)
                        {
                            IsRequest = true;
                        }
                        else
                        {
                            IsRequest = false;
                        }
                    }
                    int NewEmployeePost = 1;
                    //try
                    //{
                    //    NewEmployeePost = await _ticketService.F_RDFCheckItemByEmployee(EmployeeID, ItemCode, RVCNo);
                    //}
                    //catch
                    //{
                    //    NewEmployeePost = 1;
                    //}
                    DateTime StartTime = q.StartTime.Value;
                    if (it.IsCombo ?? false == true)
                    {
                        var ckcb = (from a in Details
                                    where a.IDCombo == ItemCode
                                    select a).FirstOrDefault();
                        if (ckcb != null)
                        {
                            Resuilt = 0;
                        }
                        else { }
                    }
                    else
                    {

                        #region Post Manual
                        SPOS_APPOINTMENT_DETAIL dlt = new SPOS_APPOINTMENT_DETAIL();
                        dlt.StartTime = StartTime;
                        if (customPrice != 0)
                        {
                            dlt.Amount = customPrice;
                            if (isChangePrice && it.BasePrice != 0)
                            {
                                dlt.SlipAmount = customPrice - it.BasePrice;
                            }
                        }
                        else
                        {
                            dlt.Amount = it.BasePrice;
                        }
                        dlt.isPlus = it.isPlus ?? false;
                        dlt.AppointmentID = q.AppointmentID;
                        if (it.IsNotCountTurn == true)
                        {
                            dlt.Duration_Item = 0;
                        }
                        else
                        {
                            dlt.Duration_Item = int.Parse(it.Duration.HasValue ? it.Duration.Value.ToString() : "0");
                        }
                        dlt.IsRequestTech = IsRequest;
                        int Duration = 0;
                        int Type = 1;
                        if (customType != 0)
                        {
                            dlt.Type = customType;
                            dlt.Duration = 0;
                            dlt.Duration_Item = 0;
                            Type = customType;
                        }
                        else
                        {
                            if (it.IsService == true && lstDetailComBo == "")
                            {
                                dlt.Duration = it.Duration;
                                Duration = it.Duration.Value;
                                dlt.Duration_Item = it.Duration;

                                try
                                {
                                    dlt.TaxValue = 0;
                                }
                                catch
                                {
                                    dlt.TaxValue = 0;
                                }
                                dlt.Type = 1;
                                dlt.Tax4No = it.IsAllowCommission ?? false ? "1" : "0";
                                dlt.Tax4Num = it.IsPercentServiceCharge ?? false ? it.BasePrice * it.Cost / 100 : it.Cost;
                                Type = 1;
                            }
                            else if (it.IsRetail == true && (it.IsFee ?? false) == false)
                            {
                                dlt.Duration = 0;
                                dlt.Duration_Item = 0;
                                try
                                {
                                    dlt.TaxValue = 0;
                                }
                                catch
                                {
                                    dlt.TaxValue = 0;
                                }
                                dlt.Type = 2;
                                dlt.Tax4No = it.IsAllowCommission ?? false ? "1" : "0";
                                Type = 2;
                            }
                            else if (it.IsFee == true)
                            {
                                dlt.Duration = 0;
                                dlt.Duration_Item = 0;
                                dlt.TaxValue = 0;
                                dlt.Type = 4;
                                Type = 4;
                            }
                            else if (lstDetailComBo != "" && ItemCode == -1)
                            {
                                dlt.Duration = 0;
                                dlt.Duration_Item = 0;
                                try
                                {
                                    dlt.TaxValue = 0;
                                }
                                catch
                                {
                                    dlt.TaxValue = 0;
                                }
                                dlt.Type = 1;
                                Type = 1;
                                dlt.ItemName = lstDetailComBo;
                            }
                            else
                            {
                                dlt.Duration = it.Duration;
                                dlt.Duration_Item = it.Duration;
                                try
                                {
                                    dlt.TaxValue = 0;
                                }
                                catch
                                {
                                    dlt.TaxValue = 0;
                                }
                                dlt.Type = 1;
                                dlt.Tax4No = it.IsAllowCommission ?? false ? "1" : "0";
                                dlt.Tax4Num = it.IsPercentServiceCharge ?? false ? it.BasePrice * it.Cost / 100 : it.Cost;
                                Type = 1;
                            }
                        }


                        if (itemDuration != 0) { dlt.Duration = itemDuration; }
                        if (ProdCharge != 0)
                        {
                            dlt.CRVValue = ProdCharge;
                            dlt.PushingStatus = true;
                        }

                        dlt.EndTime = StartTime.AddMinutes(Duration);
                        dlt.ExtraDuration = 0;

                        dlt.ChairCode = ChairCode;
                        dlt.EmployeeID = EmployeeID;

                        if (ActiveWaitingList == 0)
                        {
                            dlt.Status = 8;
                        }
                        else
                        {
                            dlt.Status = status;
                        }
                        dlt.ItemID = it.ItemID;
                        dlt.Qty = 1;
                        dlt.IsCategory = false;
                        dlt.IsDelete = false;
                        dlt.IsDeleted = false;
                        dlt.OrgAptDetail = dlt.AppointmentDetailID;
                        dlt.IsFullTurn = true;
                        dlt.LastChange = DateTime.UtcNow;
                        dlt.RVCNo = RVCNo;
                        dlt.IDCombo = 0;
                        dlt.LastChangeTime = DateTime.UtcNow;
                        // Open Item Add Turn
                        if (Turn != 0)
                        {
                            dlt.Tax5Num = Math.Abs(Turn);
                        }
                        else
                        {
                            dlt.Tax5Num = 0;
                        }
                        try
                        {
                            if (name != "")
                            {
                                dlt.ItemName = name;
                            }
                            else
                            {
                                dlt.ItemName = it.ItemName.ToString();
                            }

                        }
                        catch
                        {
                            dlt.ItemName = "Custom Price";
                        }
                        Resuilt = _ticketService.Insert_SPOS_DETAIL(dlt);
                        #endregion
                    }
                }
                else
                {
                    Resuilt = 0;
                }
            }
            else
            {
                Resuilt = 0;
            }
            return Resuilt;
        }


        public async Task<ResultJs<List<AddListItemRsp>>> AddListItem(AddListItemReq[] model)
        {
            ResultJs<List<AddListItemRsp>> result = new ResultJs<List<AddListItemRsp>>();
            List<AddListItemServiceReq> modelService = new List<AddListItemServiceReq>();
            List<string> listRowidSuccess = new List<string>();
            result.status = 200;
            if (model.Length == 0)
            {
                return result;
            }
            int RVCno = 0;
            decimal OrgAptId = 0;
            var itemConst = model.FirstOrDefault();

            //_ticketService.CleanItemInTicket(itemConst.appID, itemConst.RVCNo);
            foreach (var item in model)
            {
                RVCno = item.RVCNo;
                OrgAptId = Convert.ToDecimal(item.appID);
                if (item.Action == "AddItem")
                {
                    int Dur = 0;
                    try
                    {
                        Dur = int.Parse(item.Dur);
                    }
                    catch { }
                    ApiResult res = await AddItemAsync(Convert.ToDecimal(item.EmployeeID), Convert.ToDecimal(item.appID), Convert.ToInt64(item.ItemCode), Convert.ToDecimal(item.Price), item.Status ?? 2, item.RVCNo, Turn: item.Turn ?? 0, ProdCharge: item.Cost ?? 0, name: item.ItemName ?? "", itemDuration: Dur, ChairCodes: item.byPass ?? false);
                    if (res.Status == false)
                    {
                        result.status = 500;
                        result.message = res.Message;
                    }
                    else
                    {
                        listRowidSuccess.Add(item.rowid);
                        decimal AptDetailId = (decimal)res.ExtraData;
                        if (item.ChildItem != null && item.ChildItem.Length > 0)
                        {
                            _ticketService.LoadBillNotAsync(RVCno, OrgAptId);
                            foreach (var itemChild in item.ChildItem)
                            {
                                listRowidSuccess.Add(itemChild.rowid);
                                if ((itemChild.Dur ?? "").Trim() == "DISITEM")
                                {
                                    bool salon = false, tech = false, salontech = true;
                                    if (itemChild.DiscountType == "salon")
                                    {
                                        salon = true;
                                        salontech = false;
                                    }
                                    else if (itemChild.DiscountType == "tech")
                                    {
                                        tech = true;
                                        salontech = false;
                                    }
                                    discountItem(item.RVCNo, Convert.ToDecimal(item.EmployeeID), AptDetailId, Convert.ToDecimal(itemChild.Price), true, salon, salontech, tech, packName: itemChild.ItemName);

                                }
                            }
                        }
                    }
                    //_ticketService.LoadBillNotAsync(RVCno, OrgAptId);
                }
                else if (item.Action == "UpdatePrice")
                {
                    listRowidSuccess.Add(item.rowid);
                    _ticketService.UpdatePrice(item.Target, Convert.ToDecimal(item.Price), item.RVCNo);
                }
                else if (item.Action == "RemoveData")
                {
                    listRowidSuccess.Add(item.rowid);
                    if (item.rowid.IndexOf("-TrnSeq") != -1)
                    {
                        decimal TrnSeq = Convert.ToDecimal(item.rowid.Replace("-TrnSeq", ""));
                        decimal appID = Convert.ToDecimal(item.appID);
                        if (item.ItemName == "DiscountItem")
                        {
                            _ticketService.RDVoidDiscountItem(item.RVCNo, TrnSeq, TrnSeq, appID);
                        }
                    }
                    if (item.rowid.IndexOf("-AppointmentDetailID") != -1)
                    {
                        decimal AppointmentDetailID = Convert.ToDecimal(item.rowid.Replace("-AppointmentDetailID", ""));
                        decimal appID = Convert.ToDecimal(item.appID);
                        List<RDTmpTrn> SPOSDetail = _ticketService.selectRDTmpTrn($" AppointmentId = '{appID}' and RVCNo = '{item.RVCNo}' and AppointmentDetailID = '{AppointmentDetailID}'");
                        RDTmpTrn discountItem = SPOSDetail.FirstOrDefault(x => x.ItemCode == "DiscountItem");
                        if (discountItem != null)
                        {
                            _ticketService.RDVoidDiscountItem(discountItem.RVCNo, discountItem.TrnSeq, discountItem.OrgTrnSeq ?? discountItem.TrnSeq, appID);
                        }
                        List<RDTmpTrn> lstExtra = SPOSDetail.Where(x => (x.ItemCode ?? "").Trim() == "ExtraItem").ToList();
                        _ticketService.RDVoidItem(item.RVCNo, AppointmentDetailID, appID, appID);
                    }
                }
                else if (item.Action == "changeTech")
                {
                    if (item.Dur == "CATEGORY")
                    {
                        var SPOS_DETAIL = _ticketService.Load_SPOS_APPOINTMENT_DETAIL(OrgAptId, item.RVCNo);
                        var SPOS_DETAIL_CATEGORY = SPOS_DETAIL.FirstOrDefault(x => x.IsCategory == true && x.AppointmentDetailID == item.Target);
                        var SPOS_DETAIL_NOT_CATEGORY = SPOS_DETAIL.FirstOrDefault(x => x.IsCategory != true && x.EmployeeID == Convert.ToDecimal(item.EmployeeID));
                    }
                    listRowidSuccess.Add(item.rowid);
                    string strresult = ChangeEmployeeByItem(item.RVCNo, item.Target, OrgAptId, Convert.ToDecimal(item.EmployeeID), false, item.byPass ?? false);
                    if (strresult != "success")
                    {
                        result.message = strresult;
                    }
                }
                else if (item.Action == "InsertDiscount")
                {
                    listRowidSuccess.Add(item.rowid);
                    bool salon = false, tech = false, salontech = true;
                    if (item.DiscountType == "salon")
                    {
                        salon = true;
                        salontech = false;
                    }
                    else if (item.DiscountType == "tech")
                    {
                        tech = true;
                        salontech = false;
                    }
                    discountItem(item.RVCNo, Convert.ToDecimal(item.EmployeeID), item.Target, Convert.ToDecimal(item.Price), true, salon, salontech, tech, packName: item.ItemName);
                }
            }
            result.data = new List<AddListItemRsp>();
            foreach (var item in listRowidSuccess)
            {
                AddListItemRsp tmpItem = new AddListItemRsp();
                tmpItem.rowid = item;
                result.data.Add(tmpItem);
            }
            _ticketService.LoadBillNotAsync(RVCno, OrgAptId);

            return result;

        }
        public ApiResult discountItem(int RVCNo, decimal EmployeeID, decimal AptDetailId, decimal DiscountValue, bool IsAmount, bool OnSalon, bool OnSalonTech, bool OnTech, string packName = "", bool IsFee = false)
        {
            var result = new ApiResult();
            try
            {
                //string sql = $@"P_DNControlItemDiscount  {RVCNo} ,'{packName}',{TrnSeq},{DiscountValue},{IsAmount},{OnSalon},{OnSalonTech},{OnTech},{IsFee},{EmployeeID}";

                int DataResuilt = _ticketService.P_DNControlItemDiscount(RVCNo, EmployeeID, AptDetailId, DiscountValue, IsAmount, OnSalon, IsFee, OnSalonTech, OnTech, packName);
                if (DataResuilt == 0)
                {
                    result.Status = true;
                    result.Message = "Success";
                }
                else if (DataResuilt == 1)
                {
                    result.Status = false;
                    result.Message = "Item Have Discount";
                }
                else if (DataResuilt == 2)
                {
                    result.Status = false;
                    result.Message = "Discount not invalid";
                }
                else if (DataResuilt == 3)
                {
                    result.Status = false;
                    result.Message = "Not fround Item Discount";
                }
                else if (DataResuilt == 4)
                {
                    result.Status = false;
                    result.Message = "Void  ticket discount before add discounted item";
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Error: " + ex.ToString();
            }

            return result;
        }
        public string ChangeEmployeeByItem(int RVCNo, decimal AppointmentDetailID, decimal OrgAppointment, decimal sEmployeeID, bool IsResetDuaration, bool byPass = false)
        {
            string Resuilt = "success";
            bool canDoService = true;
            string CRVValue = "NULL";
            var tnptrn = _ticketService.selectRDTmpTrn($"RVCNo = '{RVCNo}' and AppointmentID={OrgAppointment}");
            var ck = tnptrn.FirstOrDefault(x => x.AppointmentDetailID == AppointmentDetailID);
            if (ck != null)
            {
                if (Resuilt == "success")
                {
                    DateTime TicketDate = DateTime.UtcNow;

                    try
                    {

                        // Check Item Discount
                        //var q2 = _ticketService.check_Item_Discount(RVCNo, (ck.OrgTrnSeq ?? ck.TrnSeq));
                        //if (q2 != null)
                        //{
                        //    _ticketService.Update_RDDiscountByTech(sEmployeeID, RVCNo, (ck.OrgTrnSeq ?? ck.TrnSeq));
                        //}
                        if (ck != null)
                        {
                            try
                            {
                                TicketDate = ck.TrnTime.Value;
                            }
                            catch
                            {
                                TicketDate = DateTime.UtcNow;
                            }
                            DateTime CurrrentDate = DateTime.UtcNow;
                            TimeSpan day = Convert.ToDateTime(CurrrentDate) - Convert.ToDateTime(TicketDate.ToString("MM/dd/yyyy"));

                            if (Resuilt == "success")
                            {
                                decimal OldEmployeeID = 0;
                                try
                                {
                                    OldEmployeeID = ck.EmployeeID.HasValue == true ? ck.EmployeeID.Value : 0;
                                }
                                catch
                                {
                                    OldEmployeeID = 0;
                                }
                                int Status = 0;
                                try
                                {
                                    Status = ck.Status.Value;
                                }
                                catch
                                {
                                    Status = 0;
                                }

                                //Check No
                                decimal OrgCheckNo = 0;
                                decimal checkno = 0;
                                OrgCheckNo = ck.OrgCheckNo.Value;

                                checkno = ck.CheckNo.Value;

                                if (sEmployeeID != OldEmployeeID)
                                {
                                    decimal AptID = 0;
                                    AptID = ck.AppointmentID ?? 0;
                                    decimal AptDetaiID = 0;
                                    AptDetaiID = ck.AppointmentDetailID.HasValue == true ? ck.AppointmentDetailID.Value : 0;
                                    var sts = _ticketService.Load_SPOSAppointment(AptID, RVCNo);
                                    //decimal EmployeeRequest = 0;
                                    //bool IsRequest = false;
                                    //try
                                    //{
                                    //    EmployeeRequest = sts.EmployeeID ?? 0;
                                    //}
                                    //catch
                                    //{
                                    //    EmployeeRequest = 0;
                                    //}
                                    //if (sEmployeeID == EmployeeRequest && sEmployeeID > 1000 && EmployeeRequest > 1000)
                                    //{
                                    //    IsRequest = true;
                                    //}
                                    int Type = 0;
                                    try
                                    {
                                        Type = ck.Type.Value;
                                    }
                                    catch
                                    {
                                        Type = 0;
                                    }
                                    List<SPOS_APPOINTMENT_DETAIL> spodt = _ticketService.Load_SPOS_APPOINTMENT_DETAIL(AptID, RVCNo);

                                    var qdl = (from a in spodt
                                               where a.RVCNo == RVCNo && (a.OrgAptDetail == AptDetaiID || a.AppointmentDetailID == AptDetaiID)
                                               select a).FirstOrDefault();
                                    string Old = "";
                                    string ServiceName = "";

                                    if (qdl != null)
                                    {
                                        ServiceName = qdl.ItemName;
                                        long chairCode = 0;
                                        if (byPass) { chairCode = 1; }
                                        _ticketService.Update_After_ChangeEmployee(sEmployeeID, RVCNo, ck.OrgTrnSeq ?? ck.TrnSeq, ck.TrnSeq, CRVValue, chairCode, AptDetaiID, IsResetDuaration, false);
                                    }
                                    int AptSts = sts.AppointmentStatusID ?? 0;
                                    _ticketService.Delete_TipAdjust(checkno, RVCNo);
                                    Resuilt = "success";


                                }

                            }
                        }
                    }
                    catch
                    {
                        Resuilt = "Can Not Change";
                    }
                }
            }
            else
            {
                Resuilt = "Can Not Change";
            }


            return Resuilt;
        }
        public string PostGiftCard(DateTime? litmit, int Masterstore, int RVCNo, string SeriNumber, long ItemID, decimal Amount, decimal AptID, decimal EmpID, decimal SellID, decimal OrgAptId)
        {
            string check = _giftcardService.CheckSeriGiftCard(SeriNumber, EmpID, RVCNo);
            if (check != "Success" && check != "Tech Dont Have Permission")
            {
                return check;
            }
            else
            {
                if (check == "Tech Dont Have Permission")
                {
                    EmpID = 0;
                }
                decimal TrnSeq = _giftcardService.PostGiftCard(SeriNumber, ItemID, Amount, OrgAptId, AptID, EmpID, SellID, RVCNo);
                decimal IDGirftCard = InsGiftCard(litmit, Amount, SeriNumber, AptID, OrgAptId, TrnSeq, RVCNo, Masterstore);
                return check;
            }
        }

        public decimal InsGiftCard(DateTime? litmit, decimal Amount, string SeriCard, decimal AptID, decimal OrgAptID, decimal TrnSeq, int RVCNo, int Masterstore)
        {
            decimal IDGirftCard = _giftcardService.GetIDGiftCard(SeriCard, RVCNo, Masterstore);
            if (IDGirftCard == 0)
            {
                _giftcardService.InsertGiftCard(Amount, SeriCard, OrgAptID, TrnSeq, litmit, RVCNo);
                IDGirftCard = _giftcardService.GetIDGiftCard(SeriCard, RVCNo, Masterstore);
                _giftcardService.updateLimitGiftcard(litmit, IDGirftCard);
                _giftcardService.InsertFullGiftCard(Amount, IDGirftCard, AptID, OrgAptID, TrnSeq, RVCNo, Masterstore);
            }
            else
            {
                _giftcardService.updateLimitGiftcard(litmit, IDGirftCard);
                _giftcardService.InsertFullGiftCard(Amount, IDGirftCard, AptID, OrgAptID, TrnSeq, RVCNo, Masterstore);
            }
            return IDGirftCard;
        }

        public List<RDTmpTrn> getListTipOnTicket(decimal CheckNo, int RVCNo)
        {
            return _ticketService.selectRDTmpTrn($" RVCNo = {RVCNo} and CheckNo = {CheckNo}  and TrnCode=800");
        }

        public async Task<List<ApiResult>> GetTicketInfo(int RVCNo, decimal OrgAptId)
        {
            ApiResult res = new ApiResult();
            IEnumerable<SPOS_Appointment> temp = _ticketService.Load_Combine_SPOSAppointment(OrgAptId, RVCNo);
            res.Status = true;
            res.Message = "Load_Combine_SPOSAppointment";
            res.ExtraData = temp;

            //ApiResult res2 = new ApiResult();
            //res2.Status = true;
            //res2.Message = "LoadTicketTime";
            //List<RDAppointment_MapTime> dataTicktTime = _ticketService.LoadTicketTime(OrgAptId, RVCNo).ToList();
            //int minStatus = temp.Max(x => x.AppointmentStatusID ?? 0);
            //foreach (var item in dataTicktTime)
            //{
            //    item.CreateTimeUTC = item.CreateTimeUTC ?? DateTime.UtcNow;
            //    item.CreateTime = item.CreateTime ?? DateTime.Now;

            //    if (minStatus >= 2)
            //    {
            //        item.CheckInTimeUTC = item.CheckInTimeUTC ?? item.CreateTimeUTC;
            //        item.CheckInTime = item.CheckInTime ?? item.CreateTime;
            //    }
            //    if (minStatus >= 3)
            //    {
            //        item.StartTimeUTC = item.StartTimeUTC ?? item.CheckInTimeUTC;
            //        item.StartTime = item.StartTime ?? item.CheckInTime;
            //    }
            //    if (minStatus >= 7)
            //    {
            //        item.DoneTimeUTC = item.DoneTimeUTC ?? item.StartTimeUTC;
            //        item.DoneTime = item.DoneTime ?? item.StartTime;
            //    }
            //    if (minStatus == 7)
            //    {
            //        item.CloseTimeUTC = item.CloseTimeUTC ?? item.DoneTimeUTC;
            //        item.CloseTime = item.CloseTime ?? item.DoneTime;
            //    }
            //}
            //if (dataTicktTime.Count() == 0)
            //{
            //    RDAppointment_MapTime newItem = new RDAppointment_MapTime();
            //    newItem.CreateTimeUTC = newItem.CreateTimeUTC ?? DateTime.UtcNow;
            //    newItem.CreateTime = newItem.CreateTime ?? DateTime.Now;
            //    newItem.AppointmentID = OrgAptId;
            //    if (minStatus >= 2)
            //    {
            //        newItem.CheckInTimeUTC = newItem.CheckInTimeUTC ?? newItem.CreateTimeUTC;
            //        newItem.CheckInTime = newItem.CheckInTime ?? newItem.CreateTime;
            //    }
            //    if (minStatus >= 3)
            //    {
            //        newItem.StartTimeUTC = newItem.StartTimeUTC ?? newItem.CheckInTimeUTC;
            //        newItem.StartTime = newItem.StartTime ?? newItem.CheckInTime;
            //    }
            //    if (minStatus >= 7)
            //    {
            //        newItem.DoneTimeUTC = newItem.DoneTimeUTC ?? newItem.StartTimeUTC;
            //        newItem.DoneTime = newItem.DoneTime ?? newItem.StartTime;
            //    }
            //    if (minStatus == 7)
            //    {
            //        newItem.CloseTimeUTC = newItem.CloseTimeUTC ?? newItem.DoneTimeUTC;
            //        newItem.CloseTime = newItem.CloseTime ?? newItem.DoneTime;
            //    }
            //    dataTicktTime.Add(newItem);
            //}

            //res2.ExtraData = dataTicktTime;



            ApiResult res3 = new ApiResult();
            List<CUS_CUSTOMER> resultCustomerDto = new List<CUS_CUSTOMER>();
            foreach (var item in temp)
            {
                if ((item.CustomerID ?? 0) != 0)
                {
                    CUS_CUSTOMER temp3 = _clientService.Get(RVCNo, item.CustomerID ?? 0);
                    resultCustomerDto.Add(temp3);
                }
            }
            if (resultCustomerDto.Count() > 0)
            {
                CUS_CUSTOMER temp3 = _clientService.Get(RVCNo, temp.FirstOrDefault().CustomerID ?? 0);
                res3.Status = true;
                res3.Message = "CustomerDto";
                res3.ExtraData = resultCustomerDto;
            }

            List<ApiResult> finRes = new List<ApiResult>();
            finRes.Add(res);
            finRes.Add(res3);

            return finRes;
        }


        public GetBillPreviewV2 getBillPreviewV2(int RVCNo, decimal AptId)
        {
            var data = _ticketService.GetBillPreviewV2(RVCNo, AptId);
            return data;

        }
        public ResultJs<List<ResponseTerminal>> getResponseTerminalWithOutSignData(int RVCNo, decimal AptId)
        {
            ResultJs<List<ResponseTerminal>> result = new ResultJs<List<ResponseTerminal>>();
            List<ResponseTerminal> res = new List<ResponseTerminal>();
            res = _ticketService.getResponseTerminalWithOutSignData(RVCNo, AptId);
            result.data = res;
            result.status = 200;
            return result;
        }


        public async Task<ApiResult> doCloseBill(decimal CheckNo, int RVCNo, decimal SaleId)
        {
            ApiResult Resuilt = new ApiResult();
            try
            {
                Resuilt.Status = true;
                string sql = $@"select * from SPOS_APPOINTMENT with (nolock) where RVCNo={RVCNo} and CheckNo={CheckNo}";
                var SPOS = _ticketService.Load_SPOSAppointment(CheckNo, RVCNo);

                if (SPOS == null)
                {
                    Resuilt.Status = false;
                    Resuilt.Message = "Bill already closed";
                    return Resuilt;
                }
                decimal CustomerID = SPOS.CustomerID ?? 0;
                decimal AppointmentID = CheckNo;

                var trn = _ticketService.select_RDTmpTrn_By_OrgAppointment(CheckNo, RVCNo);
                decimal EmployeeID = 0;
                var checkemp = trn.Where(a => a.EmployeeID > 9999).ToList();
                if (checkemp.Count == 0)
                {
                    Resuilt.Status = false;
                    Resuilt.Message = "Tech supper admin can't close bill";
                    return Resuilt;
                }
                else
                {
                    EmployeeID = checkemp.FirstOrDefault().EmployeeID ?? 0;
                }
                var tc = trn.FirstOrDefault(a => (a.EmployeeID < 1000 || a.EmployeeID == null) && a.ItemCode.IsNumber());
                if (tc != null)
                {
                    Resuilt.Status = false;
                    Resuilt.Message = "Please Add Staff to Proceed";
                    return Resuilt;
                }

                if (Resuilt.Status == true)
                {
                    DateTime TicketDate = DateTime.UtcNow;
                    decimal OutStanding = OutStanding = trn?.Sum(x => x.BaseTTL) ?? 0;
                    if (OutStanding == 0)
                    {
                        await _ticketService.P_UpdateTimeBeforeCloseBill(RVCNo, EmployeeID, CheckNo);
                        try
                        {
                            try
                            {
                                await _ticketService.BeforeCloseBil(RVCNo, AppointmentID, CustomerID, CheckNo);
                            }
                            catch { }
                            await _ticketService.CloseBill(RVCNo, CheckNo);
                            await _ticketService.P_LogTicket("Close Bill", SaleId, AppointmentID, RVCNo);
                            await _ticketService.CloseBill_RunAfter(RVCNo, CheckNo);
                        }
                        catch (Exception e)
                        {
                            Resuilt.Status = false;
                            Resuilt.Message = e.Message;
                            Resuilt.ExtraData = e;
                            return Resuilt;
                        }
                    }
                    else
                    {
                        Resuilt.Status = false;
                        Resuilt.Message = "Outstanding not equals zero, can't close bill";
                        return Resuilt;
                    }
                }
                return Resuilt;
            }
            catch (Exception e)
            {
                Resuilt.Status = false;
                Resuilt.Message = e.Message;
                Resuilt.ExtraData = e;
                return Resuilt;
            }
        }


        public async Task<ApiResult> DoCancel(int RVCNo, string reasons, decimal AppointmentID, decimal CheckNo, decimal CancelBy)
        {
            // var data = new PosAutoContext(RVCNo);
            ApiResult result = new ApiResult();
            SPOS_Appointment spos = new SPOS_Appointment();
            if (CheckNo != 0)
            {
                var tmptrn = _ticketService.select_RDTmpTrn_By_OrgAppointment(CheckNo, RVCNo).Where(x => x.Status != 9).ToList();
                var lstspos = _ticketService.Load_SPOSAppointment(CheckNo, RVCNo);
                RDTmpTrn temp = tmptrn.FirstOrDefault(x => x.RVCNo == RVCNo && x.AppointmentID == AppointmentID && x.TrnCode == 100 && x.BaseSub > 0);
                if (temp == null)
                {
                    _ticketService.LoadBillNotAsync(RVCNo, AppointmentID);
                    lstspos = _ticketService.Load_SPOSAppointment(CheckNo, RVCNo);

                }
                spos = lstspos;
                if (spos != null)
                {
                    result = await DoLoopVoidCancel(tmptrn, spos, temp?.TrnSeq ?? 0, RVCNo, reasons, CancelBy);
                }
                else
                {
                    if (AppointmentID > 0)
                    {
                        _ticketService.Cancel(CancelBy, AppointmentID, reasons, RVCNo);
                        result.Status = true;
                        result.Message = "Cancel Ticket Success";
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "AppointmentID Not Found";
                    }

                }
            }
            return result;
        }

        public async Task<ApiResult> DoLoopVoidCancel(IEnumerable<RDTmpTrn> lsttrn, SPOS_Appointment tmpspon, decimal trnseq, int RVCNo, string reasons, decimal saleid)
        {
            int result = 0;
            decimal CustomerID = tmpspon?.CustomerID ?? 0;
            ApiResult res = new ApiResult();
            RDTmpTrn temp = lsttrn.FirstOrDefault(x => x.TrnSeq == trnseq);
            if (temp != null && temp.CheckNo == temp.OrgCheckNo)
            {
                string resDelpay = await DeleteAllPayment(lsttrn, temp.CheckNo ?? 0, RVCNo);
                if (resDelpay != "true")
                {
                    res.Status = false;
                    res.Message = resDelpay;
                    return res;
                }
            }
            else
            {
                string resDelpay = await DeleteAllPayment(lsttrn, tmpspon.CheckNo ?? 0, RVCNo);
                if (resDelpay != "true")
                {
                    res.Status = false;
                    res.Message = resDelpay;
                    return res;
                }
            }
            await _ticketService.Cancel(saleid, tmpspon.CheckNo ?? tmpspon.AppointmentID, reasons, RVCNo);

            res.Status = true;
            res.Message = "Successfull";
            return res;
        }

        public async Task<string> DeleteAllPayment(IEnumerable<RDTmpTrn> temp, decimal CheckNo, int RVCNo)
        {
            var checkCreditPayment = temp.FirstOrDefault(x => x.RVCNo == RVCNo && x.CheckNo == CheckNo && x.TrnCode == 400);
            if (checkCreditPayment != null)
            {
                return "Ticket Have CreditCard Payment";
            }
            var checkGiftcard = temp.FirstOrDefault(x => (x.ItemCode ?? "").Trim() == "-333");
            if (checkGiftcard != null)
            {
                return "Ticket Have Giftcard";
            }
            try
            {
                //GiftCard Payment,CashPayment,VoidCompPromotion
                var tmptrn = temp.Where(x => x.TrnCode != 800 && x.TrnCode != 400).ToList();
                //Parallel.ForEach(tmptrn, (it) =>
                //await Task.WhenAll(tmptrn.Select(it => PaymentEvent.RemoveTrnTmpAsync(RVCNo, it.TrnSeq, it)));
                foreach (var it in tmptrn)
                {
                    ApiResult result = await PaymentEvent.RemoveTrnTmpAsync(RVCNo, it.TrnSeq, it);
                    if (result.Status == false)
                    {
                        return result.Message;
                    }
                }



                //Discount AllBill
                bool isSuccess = PaymentEvent.VoidTotalDiscount(CheckNo, RVCNo);
                if (!isSuccess)
                {
                    return "Void Discount Fail";
                }
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
