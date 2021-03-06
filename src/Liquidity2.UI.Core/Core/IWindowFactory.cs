﻿using System;
using System.Windows;

namespace Liquidity2.UI.Core
{
    public interface IWindowFactory
    {
        TWindow Create<TWindow>() where TWindow : Window;
        object Create(Type type);
    }
}
