using System;
using System.Collections.Generic;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        void Register(ProductViewModel model);
        SearchResponse Search(string searchKey, int page = 0);
        ProductViewModel GetById(Guid id);
        void Update(ProductViewModel model);
        void Remove(Guid id);

        void LoadFromDb();
    }
}
