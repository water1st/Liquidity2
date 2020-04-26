using Liquidity2.Extensions.WindowPostions.Client;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.WindowPostions
{
    internal class WindowPostionsService : IWindowPostionsService
    {
        private readonly IWindowPostionClient client;
        private readonly IWindowPostionMapper mapper;
        private readonly IMemoryCache cache;

        public WindowPostionsService(IWindowPostionClient client, IWindowPostionMapper mapper, IMemoryCache cache)
        {
            this.client = client;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<WindowPostion> GetFirstPostionInQueue(Type windowType)
        {
            string key = $"{nameof(WindowPostionsService)}:{windowType.FullName}";
            var queue = await cache.GetOrCreateAsync(key, async entry =>
             {
                 var datas = await GetPostion(windowType);
                 var result = new ConcurrentQueue<WindowPostion>();
                 if (datas != null)
                 {
                     foreach (var data in datas)
                     {
                         result.Enqueue(data);
                     }
                 }

                 return result;
             });

            if (queue.TryDequeue(out var result))
                return result;
            else
                return null;
        }

        public Task<WindowPostion> GetFirstPostionInQueue<TWindowType>()
        {
            return GetFirstPostionInQueue(typeof(TWindowType));
        }

        public async Task<IEnumerable<WindowPostion>> GetPostion(Type windowType)
        {
            var pos = await client.GetByType(windowType.FullName);
            return pos.Select(po => mapper.Map(po));
        }

        public async Task<WindowPostion> GetPostion(Guid id)
        {
            var po = await client.GetById(id.ToString());
            return mapper.Map(po);
        }

        public Task<IEnumerable<WindowPostion>> GetPostion<TWindowType>()
        {
            var type = typeof(TWindowType);
            return GetPostion(type);
        }
    }
}
