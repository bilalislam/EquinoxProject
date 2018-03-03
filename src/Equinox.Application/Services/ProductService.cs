using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;
using Nest;

namespace Equinox.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        private readonly ElasticClient _elasticClient;
        public ProductService(IMapper mapper,
                                 IProductRepository productRepository,
                                 IMediatorHandler bus,
                                 IEventStoreRepository eventStoreRepository,
                                 ElasticClient elasticClient)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _elasticClient = elasticClient;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return _productRepository.GetAll().ProjectTo<ProductViewModel>();
        }


        public ProductViewModel GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
        }

        public void Register(ProductViewModel productViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewProductCommand>(productViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(ProductViewModel productViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProductCommand>(productViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
