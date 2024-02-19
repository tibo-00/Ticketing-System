using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Ticketing_System.Data;
using Ticketing_System.Services;

namespace Ticketing_System.Pages.Concert
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Input InputModel { get; set; }

        private IConcertService _concertService;

        public CreateModel(IConcertService concertService)
        {
            _concertService = concertService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Data.Concert concert = new Data.Concert();
                concert.Name = InputModel.Name;
                concert.Description = InputModel.Description;
                concert.ChildPrice = (decimal)InputModel.ChildPrice;
                concert.AdultPrice = (decimal)InputModel.AdultPrice;

                int id = _concertService.Create(concert);

                return RedirectToPage("Details", new { Id = id }); 
            }

            return Page();
        }

        public class Input
        {
            [Required(ErrorMessage = "Name is required!")]
            [MaxLength(100)]
            public string Name { get; set; }
            [Required(ErrorMessage = "Description is required!")]
            public string Description { get; set; }
            [Required(ErrorMessage = "Price for adults is required!")]
            [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price must be equal to or greater than zero.")]
            [Display(Name = "Price Adults")]
            public decimal? AdultPrice { get; set; }
            [Required(ErrorMessage = "Price for children is required!")]
            [Range(0, (double)decimal.MaxValue, ErrorMessage = "Price must be equal to or greater than zero.")]
            [Display(Name = "Price Children")]
            public decimal? ChildPrice { get; set; }
        }
    }
}
