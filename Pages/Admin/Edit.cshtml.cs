using AnnouncementBoard.Models;
using AnnouncementBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T.R.Sub.Pages.Admin
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AnnouncementService _service;

        [BindProperty]
        public Announcement Announcement { get; set; } = new Announcement();

        public EditModel()
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

        public IActionResult OnPost(List<IFormFile> attachments)
        {
            // 获取现有的公告
            var existingAnnouncement = _service.GetById(Announcement.Id);
            if (existingAnnouncement == null)
            {
                return RedirectToPage("Index");
            }

            // 更新公告信息
            existingAnnouncement.Title = Announcement.Title;
            existingAnnouncement.Content = Announcement.Content;
            existingAnnouncement.IsTop = Announcement.IsTop;

            // 处理附件上传
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
                        existingAnnouncement.Attachments = file.FileName;
                    }
                }
            }

            _service.Update(existingAnnouncement);

            return RedirectToPage("Index");
        }
    }
}