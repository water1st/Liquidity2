using Liquidity2.Service.Market;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.SelfSelect
{
    class SelfSelectService : ISelfSelectService
    {
        private readonly IMarketService _marketService;

        public SelfSelectService(IMarketService marketService)
        {
            _marketService = marketService;
        }

        public Task AddMarkSymbol(string symbol)
        {
            throw new System.NotImplementedException();
        }

        public async Task GetAllTickers()
        {
            await _marketService.GetAllTickers();
        }

        public Task GetMarkSymbol()
        {
            //throw new System.NotImplementedException();
            return null;
        }

        public Task RemoveMarkSymbol(string symbol)
        {
            throw new System.NotImplementedException();
        }

        public async Task SubscribeTickerData()
        {
            await _marketService.SubscribeTickerData();
        }
    }
}
