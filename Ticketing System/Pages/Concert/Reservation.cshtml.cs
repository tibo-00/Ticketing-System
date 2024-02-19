using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Ticketing_System.Data;
using Ticketing_System.Services;
using static Ticketing_System.Pages.Concert.CreateModel;

namespace Ticketing_System.Pages.Concert
{
    public class ReservationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public Input InputModel { get; set; }

        public Data.Performance? Performance { get; set; }
        public IEnumerable<Performance> Performances { get; set; }
        public IEnumerable<ConcertHall> ConcertHalls { get; set; }
        private IPerformanceService _performanceService;
        private IReservationService _reservationService;

        public ReservationModel(IPerformanceService performanceService, IReservationService reservationService)
        {
            _performanceService = performanceService;
            _reservationService = reservationService;
        }

        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                Performance = _performanceService.GetByIdLazy(Id);
                if (Performance == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            Performance = _performanceService.GetByIdLazy(Id);
            if (!ModelState.IsValid )
            {
                return Page();
            }
            if (Performance.ConcertHall.NumberOfSeats < (_reservationService.AmountByPerformanceId(Id) + InputModel.NumberOfChildren + InputModel.NumberOfAdults))
            {
                ModelState.AddModelError(string.Empty, $"There are not enough seats available anymore. Total seats: {Performance.ConcertHall.NumberOfSeats}, Available seats: {Performance.ConcertHall.NumberOfSeats - _reservationService.AmountByPerformanceId(Id)}");
                return Page();
            }

            Reservation reservation = new Reservation();
            reservation.FirstName = InputModel.FirstName;
            reservation.LastName = InputModel.LastName;
            reservation.NumberOfChildren = InputModel.NumberOfChildren;
            reservation.NumberOfAdults = InputModel.NumberOfAdults;
            reservation.TotalPrice = _reservationService.GetTotalPrice(InputModel.NumberOfAdults, Performance.Concert.AdultPrice, InputModel.NumberOfChildren, Performance.Concert.ChildPrice);
            reservation.PerformanceId = Id;

            _reservationService.Create(reservation);

            return RedirectToPage("Confirmation");
        }

        public class Input
        {
            [Required(ErrorMessage = "FirstName is required!")]
            [MaxLength(100)]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "FirstName is required!")]
            [MaxLength(100)]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Number of adults is required!")]
            [Range(0, int.MaxValue, ErrorMessage = "Number of adults must be equal to or greater than zero.")]
            [Display(Name = "Number of adults")]
            public int NumberOfAdults { get; set; }
            [Required(ErrorMessage = "Number of children is required!")]
            [Range(0, int.MaxValue, ErrorMessage = "Number of children must be equal to or greater than zero.")]
            [Display(Name = "Number of children")]
            public int NumberOfChildren { get; set; }
        }
    }
}
