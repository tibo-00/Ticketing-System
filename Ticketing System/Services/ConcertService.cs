using Microsoft.EntityFrameworkCore;
using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public class ConcertService : IConcertService
    {
        private readonly AppDbContext _context;

        public ConcertService(AppDbContext context)
        {
            _context = context;
        }
        public void DeleteById(int id)
        {
            Concert toRemove = _context.Concert.Find(id);
            _context.Remove(toRemove);
            _context.SaveChanges();
        }

        public IEnumerable<Concert> GetAll()
        {
            return _context.Concert.ToList();
        }

        public Concert GetById(int id)
        {
            return _context.Concert.Find(id);
        }

        public int Create(Concert concert)
        {
            _context.Concert.Add(concert);
            _context.SaveChanges();
            return concert.Id;
        }

        public IEnumerable<Concert> GetAllUpcoming()
        {
            return _context.Concert
                        .Include(x => x.Performances)
                        .Where(concert => concert.Performances.Any(performance => performance.StartTime > DateTime.Now))
                        .ToList();
        }

        public bool CheckDay(DateTime day, int ConcertId)
        {
            Concert concert = _context.Concert
                .Include(x => x.Performances)
                .Where(concert => concert.Id == ConcertId && concert.Performances.Any(performance => performance.StartTime.Date == day.Date))
                .FirstOrDefault();
            return concert != null;
        }
    }
}
