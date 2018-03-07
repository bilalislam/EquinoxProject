using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Repository.EventSourcing;
using Nest;

namespace Equinox.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _bus;

        private readonly ElasticClient _elasticClient;
        public ProductService(IMapper mapper,
                                 IProductRepository productRepository,
                                 IMediatorHandler bus,
                                 IEventStoreRepository eventStoreRepository,
                                 ElasticClient elasticClient)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _elasticClient = elasticClient;
        }

        public IEnumerable<ProductViewModel> Search(string searchKey, int page)
        {
            var result = _elasticClient.Search<Product>(x => x
                                .Query(q => q.QueryString(m => m.Query($"{searchKey}*")
                                .Fields(f => f.Field(fl => fl.Name))))
                                .From(page - 1)
                                .Size(10));

            return _mapper.Map<IEnumerable<ProductViewModel>>(result.Documents);
        }


        public ProductViewModel GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
        }

        public void Register(ProductViewModel productViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewProductCommand>(productViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(ProductViewModel productViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProductCommand>(productViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void LoadFromDb()
        {
            SearchHelper.Initialize(_elasticClient);
            List<Product> products = new List<Product>();
            products.AddRange(_productRepository.GetAll());
            var request = new BulkDescriptor();
            foreach (var entity in products){
                request
                    .Index<Product>(op => op
                        .Id(entity.Id)
                        .Index(SearchHelper.PRODUCT_INDEX)
                        .Document(entity));
            }

            _elasticClient.Bulk(request);
        }
    }
}
