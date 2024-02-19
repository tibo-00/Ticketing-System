using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }
        public int AmountByPerformanceId(int id)
        {
            return _context.Reservation.Where(x => x.PerformanceId == id).Sum(x => x.NumberOfAdults + x.NumberOfChildren);
        }

        public int Create(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
            return reservation.Id;
        }

        public decimal GetTotalPrice(int numberOfAdults, decimal adultPrice, int numberOfChildren, decimal childPrice)
        {
            return ((numberOfAdults * adultPrice) + (numberOfChildren * childPrice));
        }
    }
}
