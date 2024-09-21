using AnnouncementBoard.Models;
using CsvHelper;
using System.Globalization;

public class UserService
{
    private readonly string filePath = "Data/users.csv";

    public List<User> GetAllUsers()
    {
        var users = new List<User>();

        if (!File.Exists(filePath))
            return users;

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            users = csv.GetRecords<User>().ToList();
        }

        return users;
    }

    public User GetUserByUsername(string username)
    {
        var users = GetAllUsers();
        return users.FirstOrDefault(u => u.Username == username);
    }

    public void AddUser(User user)
    {
        var users = GetAllUsers();
        users.Add(user);
        SaveUsers(users);
    }

    public void SaveUsers(List<User> users)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(users);
        }
    }
}