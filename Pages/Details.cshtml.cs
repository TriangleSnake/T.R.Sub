using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T.R.Sub.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly AnnouncementService _service;

        public Announcement Announcement { get; set; } = new Announcement();

        public DetailsModel()
        {
            _service = new AnnouncementService();
        }

        public IActionResult OnGet(int id)
        {
            Announcement = _service.GetById(id);
            if (Announcement == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}