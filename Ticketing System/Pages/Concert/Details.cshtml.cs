using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ticketing_System.Data;
using Ticketing_System.Services;

namespace Ticketing_System.Pages.Concert
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public Input InputModel { get; set; }

        public Data.Concert? Concert { get; set; }
        public IEnumerable<Performance> Performances { get; set; }
        public IEnumerable<ConcertHall> ConcertHalls { get; set; }

        private IConcertService _concertService;
        private IConcertHallService _concertHallService;
        private IPerformanceService _performanceService;


        public DetailsModel( IConcertService concertService, IConcertHallService concertHallService, IPerformanceService performanceService)
        {
            _concertService = concertService;
            _concertHallService = concertHallService;
            _performanceService = performanceService;
        }

        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                Concert = _concertService.GetById(Id);
                if (Concert == null)
                {
                    return NotFound();
                }
                Performances = _performanceService.GetAllByConcertIdOrdered(Id);
                ConcertHalls = _concertHallService.GetAll();
            }
            return Page();
        }

        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid)
            {
                if (_concertService.CheckDay((DateTime)InputModel.StartTime, Id))
                {
                    ModelState.AddModelError(string.Empty, $"There arlready is a performance ont that day!");
                    return PreparePage();
                }
                if (_performanceService.CheckHall((DateTime)InputModel.StartTime, (int)InputModel.SelectedConcertHall))
                {
                    ModelState.AddModelError(string.Empty, $"There arlready is a performance ont that day in that hall!");
                    return PreparePage();
                }
                Data.Performance performance = new Data.Performance();
                performance.StartTime = (DateTime)InputModel.StartTime;
                performance.ConcertHallId = (int)InputModel.SelectedConcertHall;
                performance.ConcertId = Id;

                _performanceService.Create(performance);
            }

            return PreparePage();
        }

        private IActionResult PreparePage()
        {
            Concert = _concertService.GetById(Id);
            Performances = _performanceService.GetAllByConcertIdOrdered(Id);
            ConcertHalls = _concertHallService.GetAll();
            return Page();
        }

        public IActionResult OnPostDelete()
        {
            _concertService.DeleteById(Id);

            return RedirectToPage("Summary");
        }

        public IActionResult OnPostBook(int performanceId)
        {
            return RedirectToPage("Reservation", new { Id = performanceId });
        }

        public IActionResult OnPostRemove(int performanceId)
        {
            _performanceService.DeleteById(performanceId);
            return PreparePage();
        }

        public class Input
        {
            [Required(ErrorMessage = "Choosing a Start Time is required!")]
            [Display(Name = "Start Time")]
            public DateTime? StartTime { get; set; }
            [Required(ErrorMessage = "Choosing a ConcertHall is required!")]
            [Display(Name = "Concert Hall")]
            public int? SelectedConcertHall { get; set; }
        }
    }
}
