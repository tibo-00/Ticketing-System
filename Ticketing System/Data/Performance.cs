namespace Ticketing_System.Data
{
    public class Performance
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int ConcertHallId { get; set; }
        public ConcertHall ConcertHall { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
