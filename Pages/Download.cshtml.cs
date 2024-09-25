using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO.Compression;

public class DownloadModel : PageModel
{
    public IActionResult OnGet(string filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            filename = ""; // 防止文件名为空
        }
        
        var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data/");
        var downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Downloads/");

        var directoryName = dataDirectory + Path.GetFileNameWithoutExtension(filename);
        var zipFilePath = downloadDirectory + Guid.NewGuid() + ".zip";
        
        try{
            Directory.CreateDirectory(directoryName);
            System.IO.File.Copy(Path.Combine(dataDirectory,filename), Path.Combine(directoryName,filename), true);   
        }catch{}  //防止重複新增資料夾

        ZipFile.CreateFromDirectory(directoryName, zipFilePath);
        
        return PhysicalFile(zipFilePath, "application/zip", filename + ".zip");
    }
}