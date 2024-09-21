using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T.R.Sub.Pages.Admin
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AnnouncementService _service;

        [BindProperty]
        public Announcement Announcement { get; set; }

        public CreateModel()
        {
            _service = new AnnouncementService();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(List<IFormFile> attachments)
        {
            // 處理附件上傳
            if (attachments != null && attachments.Count > 0)
            {
                var uploadPath = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(uploadPath, file.FileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(stream);
                        }
                        Announcement.Attachments = file.FileName;
                    }
                }
            }

            _service.Add(Announcement);

            return RedirectToPage("./Index");
        }
    }
}