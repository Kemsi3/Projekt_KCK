namespace BowlingAlleyAPI.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AlleyId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
