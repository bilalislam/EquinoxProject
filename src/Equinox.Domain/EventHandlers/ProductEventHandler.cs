using System;
using System.Threading.Tasks;
using Equinox.Domain.Events;
using Equinox.Domain.Models;
using MediatR;
using Nest;

namespace Equinox.Domain.EventHandlers
{
    public class ProductEventHandler :
        INotificationHandler<ProductRegisteredEvent>,
        INotificationHandler<ProductRemovedEvent>,
        INotificationHandler<ProductUpdatedEvent>
    {
        private readonly ElasticClient _elasticClient;

        public ProductEventHandler(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            SearchHelper.Initialize(_elasticClient);
        }

        public void Handle(ProductUpdatedEvent notification)
        {
            var response = _elasticClient.Update(DocumentPath<ProductUpdatedEvent>
                .Id(notification.Id),
                u => u
                    .Index("alias-product")
                    .Type("product")
                    .DocAsUpsert(true)
                    .Doc(notification));
        }

        public void Handle(ProductRemovedEvent notification)
        {
            var response = _elasticClient.Delete<ProductRemovedEvent>(notification.Id, d => d
                                        .Index("alias-product")
                                        .Type("product"));
        }

        public void Handle(ProductRegisteredEvent notification)
        {
            _elasticClient.Index(notification, i => i
                             .Index("alias-product")
                             .Type("product")
                             .Id(notification.Id));
        }
    }
}