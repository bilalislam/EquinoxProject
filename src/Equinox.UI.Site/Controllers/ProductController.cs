using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Site.Controllers
{
    [Route("product")]
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(INotificationHandler<DomainNotification> notifications,
            IProductService productService, IHttpContextAccessor httpContextAccessor) : base(notifications)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            Initialize();
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/")]
        [Route("list-all")]
        public IActionResult Index([FromQuery]string searchKey, [FromQuery]int page)
        {
            return View(ReturnModel(searchKey, page));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("search")]
        public IActionResult Index(ProductViewModel model)
        {
            return View(ReturnModel(model.Name));
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

        private void Initialize()
        {
            if (_httpContextAccessor.HttpContext.Session.GetInt32("count") == null)
            {
                _productService.LoadFromDb();
                _httpContextAccessor.HttpContext.Session.SetInt32("count", 0);
            }
        }

        private ProductListViewModel ReturnModel(string searchKey, int page = 0)
        {
            SearchResponse result = _productService.Search(searchKey, page);
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = result.Documents,
                TotalCount = Convert.ToInt32(Math.Ceiling(result.Total / 10.0)),
                SearchKey = searchKey
            };

            return model;
        }
    }
}