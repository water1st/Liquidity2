using Liquidity2.UI.Components.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Liquidity2.UI.Components.Renderer
{
    public class RendererFactory : IRendererFactory
    {
        private readonly IServiceProvider _provider;

        public RendererFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<IRender> Create()
        {
            return _provider.GetServices<IRender>();
        }
    }
}
