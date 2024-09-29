using Deltastateonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfi_test03.Interfaces;

namespace tfi_test03.Providers
{
    public class ShippingNoticeRepoWrapper : IShippingNoticeRepoWrapper
    {
        public List<ShippingNotice> ShippingNotices => ShippingNoticeRepo.ShippingNotices;
    }
}
