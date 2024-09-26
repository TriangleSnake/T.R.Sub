using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace T.R.Sub.Pages.Admin
{
    [Authorize] // 確保只有管理員可訪問
    public class DebugModel : PageModel
    {
        // 用於顯示資源使用率的屬性
        public ResourceUsageModel ResourceUsage { get; set; }

        // Data 目錄的路徑
        private readonly string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        public void OnGet()
        {
            ResourceUsage = GetResourceUsage();
        }

        // 處理生成系統結構的 POST 請求
        public IActionResult OnPostGenerateStructure()
        {
            var rootPath = Directory.GetCurrentDirectory(); // 或指定特定的根目錄
            var tree = GenerateDirectoryTree(rootPath);

            // 確保 Data 目錄存在
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            var filePath = Path.Combine(dataDirectory, "SystemStructure.txt");
            System.IO.File.WriteAllText(filePath, tree, Encoding.UTF8);

            TempData["Message"] = "系統結構已生成並保存至 /Data/SystemStructure.txt";
            return RedirectToPage();
        }


        // 處理重啟系統的 POST 請求
        public IActionResult OnPostRestart()
        {
            // 注意：此操作將終止應用程序，具體重啟方式取決於您的部署環境
            // 在 IIS 中，應用程序會自動重新啟動
            // 在 Docker 中，可能需要外部腳本或監控工具來重啟容器
            // 在自托管環境中，建議使用外部服務管理器來控制重啟

            // 以下代碼將終止應用程序進程
            Process.GetCurrentProcess().Kill();

            // 這行代碼不會被執行，因為進程已終止
            return RedirectToPage();
        }

        // 方法：獲取資源使用率
        private ResourceUsageModel GetResourceUsage()
        {
            var process = Process.GetCurrentProcess();
            var totalRam = process.WorkingSet64 / (1024 * 1024); // 以 MB 為單位
            var cpuUsage = GetCpuUsage();

            return new ResourceUsageModel
            {
                RamUsageMB = totalRam,
                CpuUsagePercent = cpuUsage
            };
        }

        // 方法：獲取 CPU 使用率（跨平台）
        private double GetCpuUsage()
        {
            var process = Process.GetCurrentProcess();
            double cpuUsedMs = process.TotalProcessorTime.TotalMilliseconds;
            double elapsedMs = (DateTime.UtcNow - process.StartTime.ToUniversalTime()).TotalMilliseconds;
            if (elapsedMs > 0)
            {
                double cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * elapsedMs);
                return Math.Round(cpuUsageTotal * 100, 2);
            }
            return 0;
        }

        // 方法：生成目錄結構樹
        private string GenerateDirectoryTree(string rootPath, string indent = "")
        {
            var sb = new StringBuilder();
            var directoryInfo = new DirectoryInfo(rootPath);

            sb.AppendLine($"{indent}{directoryInfo.Name}/");

            foreach (var dir in directoryInfo.GetDirectories())
            {
                sb.Append(GenerateDirectoryTree(dir.FullName, indent + "    "));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                sb.AppendLine($"{indent}    {file.Name}");
            }

            return sb.ToString();
        }

        // 模型：資源使用率
        public class ResourceUsageModel
        {
            public double RamUsageMB { get; set; }
            public double CpuUsagePercent { get; set; }
        }
    }
}