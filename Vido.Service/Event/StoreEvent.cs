using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Core.Service;

namespace Vido.Core.Event
{
    public class StoreEvent
    {
        private readonly IStoreService _storeService;
        public StoreEvent(IStoreService storeService)
        {
            _storeService = storeService;
        }

    }
}
