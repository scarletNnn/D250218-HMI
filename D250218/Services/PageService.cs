﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;

namespace D250218.Services;

public class PageService : INavigationViewPageProvider
{
    private readonly IServiceProvider _serviceProvider;

    public PageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object? GetPage(Type pageType)
    {
        if (!typeof(object).IsAssignableFrom(pageType))
        {
            throw new InvalidOperationException("The page should be a WPF control.");
        }

        return _serviceProvider.GetService(pageType) as object;
    }
}
