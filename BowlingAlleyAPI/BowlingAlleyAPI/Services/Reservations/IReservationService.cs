using BowlingAlleyAPI.Models;

namespace BowlingAlleyAPI.Services.Reservations
{
    public interface IReservationService
    {
        Task<IResult> GetAllReservations();

        Task<IResult> AddReservation(Reservation reservation);

        Task<IResult> GetReservationsByUserId(Guid userId);
    }
}
