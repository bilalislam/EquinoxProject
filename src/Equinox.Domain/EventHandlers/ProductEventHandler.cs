using Equinox.Domain.Events;
using MediatR;

namespace Equinox.Domain.EventHandlers
{
    public class ProductEventHandler :
        INotificationHandler<ProductRegisteredEvent>,
        INotificationHandler<ProductRemovedEvent>,
        INotificationHandler<ProductUpdatedEvent>
    {

        public void Handle(ProductUpdatedEvent notification)
        {

        }

        public void Handle(ProductRemovedEvent notification)
        {

        }

        public void Handle(ProductRegisteredEvent notification)
        {

        }
    }
}