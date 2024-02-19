namespace Ticketing_System.Data
{
    public class Concert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ChildPrice { get; set; }
        public decimal AdultPrice { get; set; }
        public ICollection<Performance> Performances { get; set; }
    }
}
