using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Equinox.WebApi.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService,
        INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product-management")]
        public IActionResult Get()
        {
            return Response(_productService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var productViewModel = _productService.GetById(id);

            return Response(productViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "")]
        [Route("product-management")]
        public IActionResult Post([FromBody]ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(productViewModel);
            }

            _productService.Register(productViewModel);

            return Response(productViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "")]
        [Route("product-management")]
        public IActionResult Put([FromBody]ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(productViewModel);
            }

            _productService.Update(productViewModel);

            return Response(productViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "")]
        [Route("product-management")]
        public IActionResult Delete(Guid id)
        {
            _productService.Remove(id);

            return Response();
        }
    }
}
