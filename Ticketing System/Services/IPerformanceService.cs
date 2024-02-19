using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public interface IPerformanceService
    {
        IEnumerable<Performance> GetAllByConcertIdOrdered(int id);
        int Create(Performance performance);
        Performance GetById(int id);
        Performance GetByIdLazy(int id);
        void DeleteById(int id);
        bool CheckHall(DateTime date, int id);
    }
}
