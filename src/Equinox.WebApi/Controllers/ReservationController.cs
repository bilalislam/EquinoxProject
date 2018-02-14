using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.WebApi.Controllers
{
    [Authorize]
    public class ReservationController : ApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService,
                                  INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management")]
        public IActionResult Get()
        {
            return Response(_reservationService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var reservationViewModel = _reservationService.GetById(id);

            return Response(reservationViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "")]
        [Route("reservation-management")]
        public IActionResult Post([FromBody]ReservationViewModel reservationViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(reservationViewModel);
            }

            _reservationService.Register(reservationViewModel);

            return Response(reservationViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "")]
        [Route("reservation-management")]
        public IActionResult Put([FromBody]ReservationViewModel reservationViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(reservationViewModel);
            }

            _reservationService.Update(reservationViewModel);

            return Response(reservationViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "")]
        [Route("reservation-management")]
        public IActionResult Delete(Guid id)
        {
            _reservationService.Remove(id);

            return Response();
        }
    }
}
