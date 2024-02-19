using Microsoft.EntityFrameworkCore;
using System;
using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly AppDbContext _context;

        public PerformanceService(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckHall(DateTime date, int id)
        {
            Performance performance = _context.Performance
                                                    .Include(perf => perf.ConcertHall)
                                                    .Where(perf => perf.ConcertHall.Id == id && perf.StartTime.Date == date.Date)
                                                    .FirstOrDefault();
            return performance != null;
        }

        public int Create(Performance performance)
        {
            _context.Performance.Add(performance);
            _context.SaveChanges();
            return performance.Id;
        }

        public void DeleteById(int id)
        {
            Performance toRemove = _context.Performance.Find(id);
            _context.Remove(toRemove);
            _context.SaveChanges();
        }

        public IEnumerable<Performance> GetAllByConcertIdOrdered(int id)
        {
            return _context.Performance.Where(x => x.ConcertId == id).OrderBy(x => x.StartTime).ToList();
        }

        public Performance GetById(int id)
        {
            return _context.Performance.Find(id);
        }

        public Performance GetByIdLazy(int id)
        {
            return _context.Performance.Include(x => x.Concert).Include(x => x.ConcertHall).FirstOrDefault(x => x.Id == id);
        }
    }
}
