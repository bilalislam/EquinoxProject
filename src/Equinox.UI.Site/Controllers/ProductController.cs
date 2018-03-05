using System;
using System.Net.Http;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Site.Controllers
{
    [Route("product")]
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
        [Route("list-all")]
        [Route("/")]
        public IActionResult Index()
        {
            return View(_productService.Search(string.Empty, 1));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("search")]
        public IActionResult Index(string searchKey)
        {
            return View(_productService.Search(string.Empty, 1));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Create(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _productService.Register(model);

            if (IsValidOperation())
                ViewBag.Sucesso = "Product Registered!";

            return View(model);
        }

        [HttpGet]
        [Route("edit-product/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null){
                return NotFound();
            }

            var model = _productService.GetById(id.Value);

            if (model == null){
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [Route("edit-product/{id:guid}")]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _productService.Update(model);

            if (IsValidOperation())
                ViewBag.Sucesso = "Product Updated!";

            return View(model);
        }

        [HttpGet]
        [Route("remove-product/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null){
                return NotFound();
            }

            var model = _productService.GetById(id.Value);

            if (model == null){
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Route("remove-product/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _productService.Remove(id);

            if (!IsValidOperation()) return View(_productService.GetById(id));

            ViewBag.Sucesso = "Product Removed!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("product-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _productService.GetById(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}