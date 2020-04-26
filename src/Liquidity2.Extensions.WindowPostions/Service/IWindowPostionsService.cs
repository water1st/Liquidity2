using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.WindowPostions
{
    public interface IWindowPostionsService
    {
        Task<WindowPostion> GetPostion(Guid id);
        Task<IEnumerable<WindowPostion>> GetPostion(Type windowType);
        Task<IEnumerable<WindowPostion>> GetPostion<TWindowType>();

        Task<WindowPostion> GetFirstPostionInQueue(Type windowType);
        Task<WindowPostion> GetFirstPostionInQueue<TWindowType>();
    }
}
