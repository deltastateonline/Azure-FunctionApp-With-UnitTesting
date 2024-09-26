using Deltastateonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfi_test03.Interfaces
{
    public interface IShippingNoticeProvider
    {
        public Task<List<ShippingNotice>> GetShippingNoticeListAsync();
    }
}
