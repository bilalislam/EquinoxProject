using Equinox.Application.Interfaces;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Site.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(INotificationHandler<DomainNotification> notifications,
            IProductService productService) : base(notifications)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product/list-all")]
        public IActionResult Index()
        {
            return View();
        }
    }
}