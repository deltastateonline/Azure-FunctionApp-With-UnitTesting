using Deltastateonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfi_test03.Interfaces
{
    public interface IShippingNoticeRepoWrapper
    {
        List<ShippingNotice> ShippingNotices { get; }
    }
}
