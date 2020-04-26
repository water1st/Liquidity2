using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liquidity2.Extensions.Data.CRUD
{
    public interface IRepository<TPersistentObject, TPrimarykey>
        where TPrimarykey : IEquatable<TPrimarykey>
    {
        Task<TPersistentObject> GetById(TPrimarykey id);
        Task<IEnumerable<TPersistentObject>> GetAll();


        Task Add(TPersistentObject persistentObject);
        Task Update(TPersistentObject persistentObject);
        Task Delete(TPrimarykey id);
        Task Delete(TPersistentObject persistentObject);
    }

    public interface IRepository<TPersistentObject> : IRepository<TPersistentObject, Guid> { }
}
