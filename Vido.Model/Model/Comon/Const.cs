using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vido.Model.Model.Comon
{
    public static class Const
    {
        public const int Limit = 50;

        public static string POS_CONNECTION_STRING = "";

        public const string NOTIFY_REDEEM = "NotifyRedeem";
        public const string QRCode = "QRCode";

        public const string KEY_CRYTO = "IDKEYAIDIKEYCT";

        public const string SESSION_TOKEN_REQUEST = "Token_Request";
        public const string SESSION_AUTHORIZATION = "Authorization";
        public const string SESSION_EMPLOYEEID = "EmployeeId";
        public const string SESSION_RVCNO = "rvcNo";

        public const int timeStepHour = 1;
        public class Path
        {
            public static string COMPLAIN = "upload/complains/{0}";//customerid
            public static string AVATAR = "upload/Avatar";//customerid
            public static string CUS_INFO = "upload/info";//customerid
            public static string BANNER = "upload/banner";//customerid
            public static string BANNER_FILES = "upload/banner/files";//customerid

            public static string NOTIFICATION = "upload/notifications";//customerid
            public static string PROMOTION = "upload/promotions";//customerid

            public static string IMG_SHIP = "upload/ship";
            public static string ICON = "upload/icons";

            public static string MESSAGE = "upload/messages/{0}";
            public static string IMAGE_TECH_FULL = "upload/employee/full/";
            public static string IMAGE_TECH = "upload/employee/";
            public static string IMAGE_TECH_24 = "upload/employee/24/";
            public const string IMAGE_CLIENT = "upload/clients";
            public static string IMAGE_GIFT_CARD = "upload/giftcard";
            public static string IMAGE_CATEGORIES = "upload/categories/";
            public static string IMAGE_LOGO = "upload/logostore";
            public static string TECH_SIGN = "upload/techsign/";
        }

    }
}
