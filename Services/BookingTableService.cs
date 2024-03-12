﻿using Microsoft.Extensions.Configuration;
using POSWindowFormAPI.Controllers;
using POSWindowFormAPI.Data.Constants;
using POSWindowFormAPI.Data.Repositories;
using POSWindowFormAPI.Data.Repositories.Interfaces;
using POSWindowFormAPI.Models;
using POSWindowFormAPI.Models.Request;
using POSWindowFormAPI.Services.Interfaces;
using System.Data;
using System.Diagnostics.Contracts;

namespace POSWindowFormAPI.Services
{
    public class BookingTableService : IBookingTableService
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingTableRepository _bookingTableRepository;
        private readonly ITrackingRepository _trackingRepository;
        private readonly IConfiguration _configuration;

        public BookingTableService(ILogger<BookingController> logger,
                    IBookingTableRepository bookingTableRepository,
                    ITrackingRepository trackingRepository,
                    IConfiguration configuration)
        {
            _logger = logger;
            _bookingTableRepository = bookingTableRepository;
            _trackingRepository = trackingRepository;
            _configuration = configuration;
        }

        public async Task<string> GetAllBookings()
        {
            return await _bookingTableRepository.GetAllBookings(DateTime.Now.ToString("MM/dd/yyyy"));
        }

        public string CreateBooking(BookingTableDetail bookingTableDetail)
        {
            //Add tracking
            Tracking tracking = new Tracking
            {
                ActionId = Guid.NewGuid().ToString(),
                Username = bookingTableDetail.Username,
                ActionType = TrackingConstant.CREATE_BOOKING,
                ActionDetail = $"Create new booking - {bookingTableDetail.BookingId}",
                Date = DateTime.Now.ToString("G"),
                Status = string.Empty
            };
            if (_bookingTableRepository.CreateBooking(bookingTableDetail) == BookingTableConstant.RESULT_SUCCESS)
            {
                tracking.Status = TrackingConstant.SUCCESS;
                _trackingRepository.AddTracking(tracking);
            }

            else
            {
                tracking.Status = TrackingConstant.FAILED;
                _trackingRepository.AddTracking(tracking);
            }
            return tracking.Status;

        }
        public void UpdateBooking(string name, BookingTableDetail bookingTableDetail)
        {
            _bookingTableRepository.UpdateBooking(name, bookingTableDetail);

        }
        public void DeleteBooking(BookingTableRequest bookingTableRequest)
        {
            _bookingTableRepository.DeleteBooking(bookingTableRequest);

        }
        public bool GetByName(string name)
        {
            return true;
        }

        public string AddAnniversaryType(AnniversaryType anniversaryType)
        {
            //Add tracking
            Tracking tracking = new Tracking
            {
                ActionId = Guid.NewGuid().ToString(),
                Username = "Logged Username",
                ActionType = TrackingConstant.CREATE_BOOKING,
                ActionDetail = $"Create new Anniversary Type - {anniversaryType.TypeID}: {anniversaryType.TypeName}",
                Date = DateTime.Now.ToString("G"),
                Status = string.Empty
            };

            if (_bookingTableRepository.AddAnniversaryType(anniversaryType) == BookingTableConstant.RESULT_SUCCESS)
            {
                tracking.Status = TrackingConstant.SUCCESS;
                _trackingRepository.AddTracking(tracking);
            }

            else
            {
                tracking.Status = TrackingConstant.FAILED;
                _trackingRepository.AddTracking(tracking);
            }
            return tracking.Status;
        }
    }
}
