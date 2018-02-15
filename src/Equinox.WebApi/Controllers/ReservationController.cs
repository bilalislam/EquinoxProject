using System;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        [Route("reservation-management/get-all-days")]
        public IActionResult GetAllDays(int day)
        {
            return Response(_reservationService.GetAllByDay(DateTime.Today.AddDays(day)));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management/get-available-days")]
        public IActionResult GetAvailableDays(int day)
        {
            return Response(_reservationService.GetAvailableDays(DateTime.Today.AddDays(day)));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management/find-table")]
        public IActionResult FindTable(int day, int partyOfSize, string time)
        {
            return Response(_reservationService.FindTable(day, partyOfSize, time));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var reservationViewModel = _reservationService.GetById(id);

            return Response(reservationViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reservation-management/{day}")]
        public IActionResult Get(int day)
        {
            var date = DateTime.Today.AddDays(day);
            var reservationViewModel = _reservationService.GetReservationByDay(date);

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
