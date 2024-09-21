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
            _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "announcements.csv");
        }
        private readonly string _dataFilePath;


        public IActionResult OnGetExport()
        {
            if (!System.IO.File.Exists(_dataFilePath))
            {
                // 如果文件不存在，可以返回错误消息或创建一个空文件
                return NotFound("文件不存在");
            }

            var fileBytes = System.IO.File.ReadAllBytes(_dataFilePath);

            return File(fileBytes, "text/csv", "announcements.csv");
        }
        public void OnGet()
        {
            Announcements = _service.GetAll();
        }
    }
}