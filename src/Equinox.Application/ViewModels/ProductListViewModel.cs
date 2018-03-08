using System;
using System.Collections.Generic;
using Equinox.Application.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; }
    public int TotalCount { get; set; }

    public string SearchKey { get; set; }
}