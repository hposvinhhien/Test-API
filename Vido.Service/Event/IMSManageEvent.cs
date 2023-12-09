using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Extensions.Helper;
using Vido.Core.Service;
using Vido.Model.Model.Comon;

namespace Vido.Core.Event
{
    public class IMSManageEvent
    {
        private readonly IIMSManageService _imsService;
        public IMSManageEvent(IIMSManageService imsService)
        {
            _imsService = imsService;
        }
        public rsData ResgisStore(string sName, string sPhone, string sEmail, string sPass)
        {
            rsData result = new rsData();
            sPass = EncryptHelper.CreateMD5(sPass);
            int newRVCNo = _imsService.ResgisStore(sName, sPhone, sEmail, sPass);
            result.status = 200;
            result.data = newRVCNo;
            result.error_message = "Create Store Success";
            return result;
        }
    }
}
