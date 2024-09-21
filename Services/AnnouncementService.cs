using AnnouncementBoard.Models;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
namespace AnnouncementBoard.Services
{
    public class AnnouncementService
    {
        private readonly string filePath = "Data/announcements.csv";

        public List<Announcement> GetAll()
        {
            var announcements = new List<Announcement>();

            if (!File.Exists(filePath))
                return announcements;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                announcements = csv.GetRecords<Announcement>().ToList();
            }

            return announcements;
        }

        public void SaveAll(List<Announcement> announcements)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(announcements);
            }
        }

        public void Add(Announcement announcement)
        {
            var announcements = GetAll();
            announcement.Id = announcements.Any() ? announcements.Max(a => a.Id) + 1 : 1;
            announcements.Add(announcement);
            SaveAll(announcements);
        }

        public void Update(Announcement announcement)
        {
            var announcements = GetAll();
            var index = announcements.FindIndex(a => a.Id == announcement.Id);
            if (index >= 0)
            {
                announcements[index] = announcement;
                SaveAll(announcements);
            }
        }

        public void Delete(int id)
        {
            var announcements = GetAll();
            var announcement = announcements.FirstOrDefault(a => a.Id == id);
            
            if (announcement != null)
            {
                var Path = "wwwroot/uploads/" + announcement.Attachments;
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
                announcements.Remove(announcement);
                SaveAll(announcements);
            }
        }

        public Announcement GetById(int id)
        {
            var announcements = GetAll();
            return announcements.FirstOrDefault(a => a.Id == id);
        }
    }
}
