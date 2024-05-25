﻿using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;

namespace f00die_finder_be.Services.ReservationService
{
    public interface IReservationService
    {
        Task<CustomResponse<List<ReservationDto>>> GetReservationsOfRestaurantAsync(FilterReservationsOfRestaurantDto filterReservationOfRestaurantDto, int pageSize, int pageNumber);
        Task<CustomResponse<ReservationDetailDto>> AddAsync(ReservationAddDto reservationAddDto);
        Task<CustomResponse<ReservationDetailDto>> GetReservationByIdAsync(Guid reservationId);
        Task<CustomResponse<ReservationDetailDto>> UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus);
    }
}