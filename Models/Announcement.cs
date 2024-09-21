using System;
using System.Collections.Generic;

namespace AnnouncementBoard.Models
{
    public class Announcement
    {
        public int Id { get; set; } // 公告編號
        public string Title { get; set; } // 標題
        public string Content { get; set; } // 內容
        public bool IsTop { get; set; } // 是否置頂
        public DateTime PublishDate { get; set; } // 發布日期
        public string Attachments { get; set; } // 附件列表
        public string Author { get; set; } = "";// 作者
        public Announcement()
        {
            PublishDate = DateTime.Now;
        }
    }
}