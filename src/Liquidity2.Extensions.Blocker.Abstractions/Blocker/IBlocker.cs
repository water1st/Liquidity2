﻿using System.Threading.Tasks;

namespace Liquidity2.Extensions.Blocker
{
    public interface IBlocker
    {
        void Block(Task task);
        TResult Block<TResult>(Task<TResult> task);
    }
}
