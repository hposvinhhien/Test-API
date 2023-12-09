using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Table
{
    public class ResponseTerminal
    {
        public decimal ID { get; set; }
        public string RefId { get; set; }
        public string RegisterId { get; set; }
        public string InvNum { get; set; }
        public string ResultCode { get; set; }
        public string RespMSG { get; set; }
        public string Message { get; set; }
        public string AuthCode { get; set; }
        public string PNRef { get; set; }
        public string PaymentType { get; set; }
        public string ExtData { get; set; }
        public byte[] Sign { get; set; }
        public string Token { get; set; }
        public string TransType { get; set; }
        public decimal TrnSeq { get; set; }
        public decimal CheckNo { get; set; }
        public Nullable<DateTime> CreationDate { get; set; }
        public string CardType { get; set; }
        public string AcntLast4 { get; set; }
        public string Name { get; set; }
        public Nullable<int> BatchNum { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Tip { get; set; }
        public Nullable<int> TransNum { get; set; }
        public Nullable<bool> IsBatch { get; set; }
        public Nullable<decimal> StaffId { get; set; }
        public Nullable<decimal> AptId { get; set; }
        public string EntryType { get; set; }
        public string EMVData { get; set; }
        public string AID { get; set; }
        public string AppName { get; set; }
        public string TVR { get; set; }
        public string TSI { get; set; }
        public string TerminalName { get; set; }
        public Nullable<bool> IsVoided { get; set; }
        public int RVCNo { get; set; }
        public string MerchantID { get; set; }
        public int? CreditType { get; set; }
        public bool? IsPending { get; set; }
        public string TransCode { get; set; }
        public decimal? TerminalId { get; set; }

        [NotMapped]
        public Nullable<decimal> TotalAmt { get; set; }
        public bool? IsTip { get; set; }
        public decimal? RemainTip { get; set; }
        public bool? IsDivideTip { get; set; }
        public string Hash { get; set; }
        public string ApprovalDate { get; set; }
        public bool? IsPaymentOnline { get; set; }
        public string ApprovalCode { get; set; }
        public string OrderId { get; set; }
        public string Currency { get; set; }
        public string BatchCode { get; set; }
        public string BatchId { get; set; }
        public string TransactionIdentifier { get; set; }
        public string EcrTransID { get; set; }
        public string OrigECRRefNum { get; set; }
        [NotMapped]
        public string NotMappedMessage { get; set; }
        [NotMapped]
        public decimal CreateBy { get; set; }
    }
}
