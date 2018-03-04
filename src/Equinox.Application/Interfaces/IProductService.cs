using System;
using System.Collections.Generic;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        void Register(ProductViewModel model);
        IEnumerable<ProductViewModel> Search(string searchKey, int page);
        ProductViewModel GetById(Guid id);
        void Update(ProductViewModel model);
        void Remove(Guid id);
    }
}
