using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public class ConcertHallService : IConcertHallService
    {
        private readonly AppDbContext _context;

        public ConcertHallService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ConcertHall> GetAll()
        {
            return _context.ConcertHall.ToList();
        }

        public ConcertHall GetById(int id)
        {
            return _context.ConcertHall.Find(id);
        }
    }
}
