using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Ticketing_System.Data;
using Ticketing_System.Services;

namespace Ticketing_System.Pages.Concert
{
    public class SummaryModel : PageModel
    {
        public IEnumerable<Data.Concert>? Concerts { get; set; }

        private IConcertService _concertService;


        public SummaryModel(IConcertService concertService)
        {
            _concertService = concertService;
        }
        public void OnGet()
        {
            Concerts = _concertService.GetAllUpcoming();
        }

        public IActionResult OnPost(int concertId)
        {
            return RedirectToPage("Details", new { Id = concertId });
        }
    }
}
