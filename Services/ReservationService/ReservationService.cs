using AutoMapper.QueryableExtensions;
using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReservationService
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IUserService _userService;
        public ReservationService(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = userService;
        }

        public async Task<CustomResponse<ReservationDetailDto>> AddAsync(ReservationAddDto reservationAddDto)
        {
            var restaurantQuery = (await _unitOfWork.GetQueryableAsync<Restaurant>()).Include(r => r.Owner);
            var restaurant = await restaurantQuery.FirstOrDefaultAsync(r => r.Id == reservationAddDto.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var reservation = _mapper.Map<Reservation>(reservationAddDto);
            if (reservation.CustomerEmail is null || String.IsNullOrEmpty(reservation.CustomerEmail))
            {
                var user = await _userService.InternalGetUserByIdAsync(_currentUserService.UserId);
                reservation.CustomerEmail = user.Email;
            }

            reservation.RestaurantId = reservationAddDto.RestaurantId;
            reservation.UserId = _currentUserService.UserId;
            reservation.ReservationStatus = ReservationStatus.Pending;

            restaurant.ReservationCount++;

            await _unitOfWork.AddAsync(reservation);
            await _unitOfWork.UpdateAsync(restaurant);

            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"reservations-restaurant-owner-{restaurant.OwnerId}");
            await _cacheService.RemoveAsync($"reservations-customer-{_currentUserService.UserId}");

            var data = await (await _unitOfWork.GetQueryableAsync<Reservation>())
                .Where(r => r.Id == reservation.Id)
                .ProjectTo<ReservationDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            await _cacheService.SetAsync($"reservation-{reservation.Id}", data);

            await _mailService.SendEmailAsync(restaurant.Owner.Email,
                                            MailConsts.NewReservationNotification.Subject,
                                            MailConsts.NewReservationNotification.Body,
                                            new {
                                                CustomerName = reservation.CustomerName,
                                                ReservationTime = reservation.ReservationTime,
                                            });

            return new CustomResponse<ReservationDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<ReservationCustomerDto>>> GetMyReservations(FilterReservationDto filterReservationDto, int pageSize, int pageNumber)
        {
            var cacheKey = $"reservations-customer-{_currentUserService.UserId}";
            var userReservations = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
                return await reservationQuery
                    .Include(r => r.Restaurant)
                    .ThenInclude(r => r.Images)
                    .Where(r => r.UserId == _currentUserService.UserId)
                    .ToListAsync();
            });
            var userReservationsQuery = userReservations.AsQueryable();
            if (filterReservationDto.ReservationStatus != null)
            {
                userReservationsQuery = userReservationsQuery.Where(r => r.ReservationStatus == filterReservationDto.ReservationStatus);
            }
            if (filterReservationDto.ReservationTime != null)
            {
                userReservationsQuery = userReservationsQuery.Where(r => r.ReservationTime.Date == filterReservationDto.ReservationTime.Value.Date);
            }

            int totalItems = userReservationsQuery.Count();
            var items = userReservationsQuery
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReservationCustomerDto>(r))
                .ToList();

            return new CustomResponse<List<ReservationCustomerDto>>
            {
                Data = items,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                    TotalCount = totalItems
                }
            };
        }

        public async Task<CustomResponse<ReservationDetailDto>> GetReservationByIdAsync(Guid reservationId)
        {
            var data = await _cacheService.GetOrCreateAsync($"reservation-{reservationId}", async () =>
            {
                var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
                var reservationDetail = await reservationQuery
                    .ProjectTo<ReservationDetailDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(r => r.Id == reservationId);

                return reservationDetail;
            });

            return new CustomResponse<ReservationDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<ReservationOwnerDto>>> GetReservationsOfMyRestaurantAsync(FilterReservationDto filter, int pageSize, int pageNumber)
        {
            var cacheKey = $"reservations-restaurant-owner-{_currentUserService.UserId}";
            var restaurantReservations = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
                return await reservationQuery
                    .Include(r => r.Restaurant)
                    .Where(r => r.Restaurant.OwnerId == _currentUserService.UserId)
                    .ToListAsync();
            });
            var restaurantReservationsQuery = restaurantReservations.AsQueryable();
            if (filter is not null)
            {
                if (filter.ReservationStatus != null)
                {
                    restaurantReservationsQuery = restaurantReservationsQuery.Where(r => r.ReservationStatus == filter.ReservationStatus);
                }
                if (filter.ReservationTime != null)
                {
                    restaurantReservationsQuery = restaurantReservationsQuery.Where(r => r.ReservationTime.Date == filter.ReservationTime.Value.Date);
                }
                if (filter.SearchValue != null)
                {
                    restaurantReservationsQuery = restaurantReservationsQuery.Where(r => r.CustomerName.Contains(filter.SearchValue) || r.CustomerEmail.Contains(filter.SearchValue) || r.CustomerPhone.Contains(filter.SearchValue));
                }
            }

            int totalItems = restaurantReservationsQuery.Count();
            var items = restaurantReservationsQuery
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReservationOwnerDto>(r))
                .ToList();

            return new CustomResponse<List<ReservationOwnerDto>>
            {
                Data = items,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                    TotalCount = totalItems
                }
            };
        }

        public async Task<CustomResponse<ReservationDetailDto>> UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            if (_currentUserService.Role == Role.Customer && reservationStatus != ReservationStatus.Cancelled)
            {
                throw new BadRequestException("You can only cancel your reservation");
            }

            if (_currentUserService.Role == Role.RestaurantOwner && reservationStatus != ReservationStatus.Denied && reservationStatus != ReservationStatus.Confirmed)
            {
                throw new BadRequestException("You can only change status to Confirmed or Denied");
            }

            var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
            var reservation = await reservationQuery.Include(r => r.Restaurant).FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                throw new NotFoundException();
            }

            reservation.ReservationStatus = reservationStatus;
            await _unitOfWork.UpdateAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"reservations-restaurant-owner-{reservation.Restaurant.OwnerId}");
            await _cacheService.RemoveAsync($"reservation-{reservationId}");
            await _cacheService.RemoveAsync($"reservations-customer-{reservation.UserId}");

            var data = await (await _unitOfWork.GetQueryableAsync<Reservation>())
                .Where(r => r.Id == reservationId)
                .ProjectTo<ReservationDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            await _cacheService.SetAsync($"reservation-{reservation.Id}", data);

            switch (reservationStatus)
            {
                case ReservationStatus.Confirmed:
                    await _mailService.SendEmailAsync(
                        reservation.CustomerEmail,
                        MailConsts.ReservationConfirmedNotification.Subject,
                        MailConsts.ReservationConfirmedNotification.Body,
                        new
                        {
                            RestaurantName = reservation.Restaurant.Name,
                            ReservationTime = reservation.ReservationTime,
                        });
                    break;
                case ReservationStatus.Denied:
                    await _mailService.SendEmailAsync(
                        reservation.CustomerEmail,
                        MailConsts.ReservationDeniedNotification.Subject,
                        MailConsts.ReservationDeniedNotification.Body,
                        new
                        {
                            RestaurantName = reservation.Restaurant.Name,
                            ReservationTime = reservation.ReservationTime,
                        });
                    break;
            }

            return new CustomResponse<ReservationDetailDto>
            {
                Data = data
            };
        }
    }
}
