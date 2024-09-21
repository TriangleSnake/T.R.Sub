using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace T.R.Sub.Pages.Admin
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly AnnouncementService _service;

        public Announcement Announcement { get; set; } = new Announcement();

        public DeleteModel()
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

        public IActionResult OnPost(int id)
        {
            _service.Delete(id);
            return RedirectToPage("Index");
        }
    }
}