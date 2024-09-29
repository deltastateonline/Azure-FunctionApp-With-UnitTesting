using Deltastateonline.Models;
using System.Reflection.Metadata.Ecma335;
using tfi_test03.Interfaces;

namespace tfi_test03.Providers
{
    public class ShippingNoticeProvider : IShippingNoticeProvider
    {
        IShippingNoticeRepoWrapper _shippingNoticeRepoWrapper;
        public ShippingNoticeProvider(IShippingNoticeRepoWrapper shippingNoticeRepoWrapper) { 
            _shippingNoticeRepoWrapper = shippingNoticeRepoWrapper;
        }

        public async Task<ShippingNotice?> GetShippingNotice(string ShipmentId)
        {
           return _shippingNoticeRepoWrapper.ShippingNotices.FirstOrDefault(x=>x.ShipmentId.Equals(ShipmentId));
        }

        public async Task<List<ShippingNotice>> GetShippingNoticeListAsync()
        {            
            return _shippingNoticeRepoWrapper.ShippingNotices;
        }
    }
}