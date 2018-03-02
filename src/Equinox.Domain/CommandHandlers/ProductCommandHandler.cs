using System;
using System.Linq;
using System.Collections.Generic;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using MediatR;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.CommandHandlers
{
    public class ProductCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewProductCommand>,
        INotificationHandler<UpdateProductCommand>,
        INotificationHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductCommandHandler(IProductRepository productRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _productRepository = productRepository;
        }

        public void Handle(RegisterNewProductCommand message)
        {
            Validate(message);
            var product = new Product(Guid.NewGuid(), message.Name);
            _productRepository.Add(product);
            if (Commit())
                RaiseEvent(new ProductRegisteredEvent(product.Id, product.Name));
        }

        public void Handle(UpdateProductCommand message)
        {
            Validate(message);
            var product = new Product(message.Id, message.Name, message.LastUpdateDate);
            _productRepository.Update(product);
            if (Commit())
                RaiseEvent(new ProductUpdatedEvent(product.Id, product.Name, product.LastUpdateDate.Value));
        }

        public void Handle(RemoveProductCommand message)
        {
            Validate(message);
            _productRepository.Remove(message.Id);
            if (Commit())
                RaiseEvent(new ProductRemovedEvent(message.Id));
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}