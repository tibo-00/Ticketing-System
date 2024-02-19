using Ticketing_System.Data;

namespace Ticketing_System.Services
{
    public interface IReservationService
    {
        int AmountByPerformanceId(int id);
        int Create(Reservation reservation);
        decimal GetTotalPrice(int numberOfAdults , decimal adultPrice, int numberOfChildren, decimal childPrice);
    }
}
