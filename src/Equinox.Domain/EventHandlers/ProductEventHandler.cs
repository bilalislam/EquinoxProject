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
            System.Console.WriteLine("update product from elastic");
        }

        public void Handle(ProductRemovedEvent notification)
        {
            System.Console.WriteLine("remove product from elastic");
        }

        public void Handle(ProductRegisteredEvent notification)
        {
            System.Console.WriteLine("create product from elastic");
        }
    }
}