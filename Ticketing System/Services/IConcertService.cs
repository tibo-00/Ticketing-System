using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public interface IConcertService
    {
        IEnumerable<Concert> GetAll();
        IEnumerable<Concert> GetAllUpcoming();
        int Create(Concert concert);
        Concert GetById(int id);
        void DeleteById(int id);
        bool CheckDay(DateTime day, int ConcertId);
    }
}
