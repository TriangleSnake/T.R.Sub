using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AnnouncementBoard.Pages.Admin
{
    [Authorize]
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
            Announcements = _service.GetAll();
        }
    }
}