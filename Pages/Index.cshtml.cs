using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AnnouncementBoard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AnnouncementService _service;

        public List<Announcement> Announcements { get; set; }

        public IndexModel()
        {
            _service = new AnnouncementService();
        }

        public void OnGet()
        {
            Announcements = _service.GetAll()
                .OrderByDescending(a => a.IsTop)
                .ThenByDescending(a => a.PublishDate)
                .ToList();
        }
    }
}