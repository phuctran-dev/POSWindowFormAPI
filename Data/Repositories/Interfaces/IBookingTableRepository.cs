﻿using POSWindowFormAPI.Models;
using POSWindowFormAPI.Models.Request;

namespace POSWindowFormAPI.Data.Repositories.Interfaces
{
    public interface IBookingTableRepository
    {
        public Task<string> GetAllBookings(string today);
        public string CreateBooking(BookingTableDetail bookingTableDetail);

        public string UpdateBooking(string name, BookingTableDetail bookingTableDetail);
        public string DeleteBooking(BookingTableRequest bookingTableRequest);
        public bool GetBookingByUsername(string username);
        public bool GetBookingByName(string name);

        public string AddAnniversaryType(AnniversaryType anniversaryType);
        public string GetAnniversaryTypes();
        public string GetAnniversaryTypeByName(string typeName);

    }
}
