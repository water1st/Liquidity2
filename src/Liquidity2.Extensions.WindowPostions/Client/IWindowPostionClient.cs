using Liquidity2.Extensions.Data.CRUD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.WindowPostions.Client
{
    public interface IWindowPostionClient : IRepository<WindowPostionPersistentObject, string>
    {
        Task<IEnumerable<WindowPostionPersistentObject>> GetByType(string typeName);
    }
}
