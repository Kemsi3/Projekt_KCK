using BowlingAlleyAPI.Data;
using BowlingAlleyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingAlleyAPI.Services.Alleys
{
    public class AlleyService : IAlleyService
    {
        public readonly DataContext _context;

        public AlleyService(DataContext context)
        {
            _context = context;
        }


        public async Task<IResult> GetAvailableAlleys(DateTime startDate, DateTime endDate)
        {
            List<Alley> availableAlleys = new List<Alley>();

            foreach (Alley a in _context.Alleys)
            {
                if (!(_context.Reservations.Any(reservation => reservation.AlleyId == a.AlleyId && !(reservation.EndDate < startDate || reservation.StartDate > endDate))))
                {
                    availableAlleys.Add(a);
                }
            }
            return Results.Ok(availableAlleys);
        }

        public async Task<IResult> GetAllAlleys()
        {
            return Results.Ok(await _context.Alleys.ToListAsync());
        }



        public bool validateDates(DateTime reservedDateStart, DateTime reservedDateEnd, DateTime requestedDateStart, DateTime requestedDateEnd)
        {
            return ((reservedDateEnd < requestedDateStart || (reservedDateStart > requestedDateEnd)) && requestedDateStart < requestedDateEnd);
        }
    }
}
