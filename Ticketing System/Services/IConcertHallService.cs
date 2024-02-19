using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public interface IConcertHallService
    {
        IEnumerable<ConcertHall> GetAll();
        ConcertHall GetById(int id);
    }
}
