namespace Ticketing_System.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfAdults { get; set; }
        public decimal TotalPrice { get; set; }
        public int PerformanceId { get; set; }
        public Performance Performance { get; set; }
    }
}
