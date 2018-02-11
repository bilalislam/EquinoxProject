using Equinox.Application.Interfaces;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.UI.Site.Controllers
{
    [Authorize]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(INotificationHandler<DomainNotification> notifications,
            IReservationService reservationService) : base(notifications)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation/list-all")]
        public IActionResult Index()
        {
            return View();
        }
    }
}