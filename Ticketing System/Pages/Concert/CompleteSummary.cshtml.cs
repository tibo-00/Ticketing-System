using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticketing_System.Services;

namespace Ticketing_System.Pages.Concert
{
    public class CompleteSummaryModel : PageModel
    {
        public IEnumerable<Data.Concert>? Concerts { get; set; }

        private IConcertService _concertService;


        public CompleteSummaryModel(IConcertService concertService)
        {
            _concertService = concertService;
        }
        public void OnGet()
        {
            Concerts = _concertService.GetAll();
        }

        public IActionResult OnPost(int concertId)
        {
            return RedirectToPage("Details", new { Id = concertId });
        }
    }
}
