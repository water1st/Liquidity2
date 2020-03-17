using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Liquidity2.Extensions.Data.LocalStorage
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
