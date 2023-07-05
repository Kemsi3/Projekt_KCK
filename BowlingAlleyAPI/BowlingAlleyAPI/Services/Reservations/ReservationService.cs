using BowlingAlleyAPI.Data;
using BowlingAlleyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingAlleyAPI.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        public readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context;
        }

        public async Task<IResult> AddReservation(Reservation reservation)
        {
            foreach (Reservation r in _context.Reservations.Where(r => r.AlleyId == reservation.AlleyId))
            {
                if (!validateDates(r.StartDate, r.EndDate, reservation.StartDate, reservation.EndDate))
                    return Results.BadRequest();
            }

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return Results.Created($"/reservation/{reservation.ReservationId}", reservation);
        }


        public async Task<IResult> GetAllReservations()
        {
            return Results.Ok(await _context.Reservations.Where(r => r.IsDeleted == false).ToListAsync());
        }

        public async Task<IResult> GetReservationsByUserId(Guid userId)
        {
            return Results.Ok(await _context.Reservations.Where(r => r.IsDeleted == false && r.UserId==userId).ToListAsync());
        }


        public bool validateDates(DateTime reservedDateStart, DateTime reservedDateEnd, DateTime requestedDateStart, DateTime requestedDateEnd)
        {
            return ((reservedDateEnd < requestedDateStart || (reservedDateStart > requestedDateEnd)) && requestedDateStart < requestedDateEnd);
        }
    }
}
