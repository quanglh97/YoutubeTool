using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Selenium.WebDriver.UndetectedChromeDriver;
using Selenium.Extensions;

namespace YoutubeTool
{
    public class DownloadHelper
    {
        // Hàm lõi thực thi yt-dlp ngầm
        // Nhận vào URL, thư mục lưu và một đối tượng tiến trình progress để đẩy log về Form
        public static Task<bool> RunYtdlpAsync(string url, string outputDirectory, IProgress<string> progress)
        {
            var tcs = new TaskCompletionSource<bool>();

            Process startInfo = new Process();
            startInfo.StartInfo.FileName = "yt-dlp";
            startInfo.StartInfo.Arguments = $"--concurrent-fragments 10 -o \"{outputDirectory}\\%(uploader)s - %(title)s.%(ext)s\" \"{url}\"";
            // Thêm --downloader aria2 và cấu hình chia nhỏ file thành 16 luồng tải cùng lúc
            //startInfo.StartInfo.Arguments = $"-o \"{outputDirectory}\\%(uploader)s - %(title)s.%(ext)s\" --downloader aria2 --downloader-args \"aria2c:-x 16 -s 16\" \"{url}\"";

            startInfo.StartInfo.UseShellExecute = false;
            startInfo.StartInfo.CreateNoWindow = true;
            startInfo.StartInfo.RedirectStandardOutput = true;
            startInfo.StartInfo.RedirectStandardError = true;

            // Đẩy log về UI thông qua progress.Report()
            startInfo.OutputDataReceived += (sender, e) => {
                if (e.Data != null) progress?.Report("   " + e.Data);
            };
            startInfo.ErrorDataReceived += (sender, e) => {
                if (e.Data != null) progress?.Report("   [Error] " + e.Data);
            };

            startInfo.EnableRaisingEvents = true;
            startInfo.Exited += (sender, e) =>
            {
                tcs.SetResult(startInfo.ExitCode == 0);
                startInfo.Dispose();
            };

            try
            {
                startInfo.Start();
                startInfo.BeginOutputReadLine();
                startInfo.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                progress?.Report($"[Lỗi Hệ Thống] Không thể gọi yt-dlp: {ex.Message}");
                tcs.SetResult(false);
            }

            return tcs.Task;
        }

        public static List<TikTokMedia> ExtractChannelVideos(string channelUrl, IProgress<string> progress, int scrollNum, out int openedPid)
        {
            var mediaList = new List<TikTokMedia>();
            var uniqueVideoIds = new HashSet<string>();
            openedPid = 0;
            progress?.Report("Đang mở trình duyệt Chrome thực tế từ hệ thống chống Captcha...");

            // 1. Tự động gọi CMD mở một cửa sổ Chrome thực độc lập qua cổng Debug 9222
            // Phương pháp này tạo ra một trình duyệt 100% người dùng thật, giúp loại bỏ hoàn toàn Captcha vòng tròn
            try
            {
                Process startChrome = new Process();
                startChrome.StartInfo.FileName = "cmd.exe";
                // Lưu dữ liệu vào một thư mục tạm để không ảnh hưởng đến trình duyệt Chrome bạn đang dùng hàng ngày
                startChrome.StartInfo.Arguments = "/c start chrome.exe --remote-debugging-port=9222 --user-data-dir=\"D:\\TEMP_CHROME_DRIVER\"";
                startChrome.StartInfo.CreateNoWindow = true;
                startChrome.StartInfo.UseShellExecute = false;
                startChrome.Start();
                // LƯU LẠI MÃ PID CỦA TIẾN TRÌNH CHROME NÀY
                openedPid = startChrome.Id;
            }
            catch (Exception ex)
            {
                
                progress?.Report($"[Lỗi] Không thể mở Chrome hệ thống: {ex.Message}");
                return mediaList;
            }

            Thread.Sleep(3000); // Đợi 3 giây để cửa sổ Chrome thực khởi động xong hoàn toàn

            // 2. Cấu hình Selenium thuần kết nối trực tiếp vào cổng 9222 vừa mở
            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9222";
            // 2. KHÓA KHÔNG CHO HIỆN CỬA SỔ CMD CỦA CHROMEDRIVER (Thêm đoạn này vào)
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; // Ép ẩn hoàn toàn cửa sổ đen CMD hệ thống

            // Khởi tạo ChromeDriver cốt lõi (Không dùng bất kỳ thư viện mở rộng nào khác để tránh lỗi Assembly)
            using (IWebDriver driver = new ChromeDriver(service, options))
            {
                try
                {
                    progress?.Report($"Đang kết nối tới kênh: {channelUrl}");
                    driver.Navigate().GoToUrl(channelUrl);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    wait.Until(d => d.FindElements(By.XPath("//a[contains(@href, '/video/')]")).Count > 0);

                    // ====================================================================
                    // GIAI ĐOẠN 1: CUỘN XUỐNG ĐÁY TRANG ĐỂ ÉP TIẾN TRÌNH RENDER 100% ẢNH THẬT
                    // ====================================================================
                    progress?.Report("Bắt đầu lướt kênh để tải danh sách (Vui lòng đợi)...");

                    long lastHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");
                    int noNewScrollStreak = 0;

                    while (noNewScrollStreak < scrollNum) // Cuộn đến khi không thể dài thêm trang được nữa
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        Thread.Sleep(2000); // Khoảng nghỉ ngắn để TikTok tải thêm khối HTML mới

                        long newHeight = (long)((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight");
                        if (newHeight == lastHeight)
                        {
                            noNewScrollStreak++;
                        }
                        else
                        {
                            noNewScrollStreak = 0;
                            lastHeight = newHeight;
                        }
                    }

                    // Cuộn ngược lên đầu trang rồi cuộn từ từ xuống lại để kích hoạt luồng tải lười (Lazy Load) đổ dữ liệu thật
                    progress?.Report("Đang tối ưu hóa bộ render hiển thị hình ảnh...");
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
                    Thread.Sleep(1000);

                    int totalHeight = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript("return document.body.scrollHeight"));
                    int currentScroll = 0;

                    // Cuộn giật cấp (mỗi lần 600 pixel) để ép các ô đi qua tầm nhìn màn hình render ảnh thật
                    while (currentScroll < totalHeight)
                    {
                        currentScroll += 600;
                        ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollTo(0, {currentScroll});");
                        Thread.Sleep(150); // Lướt nhanh qua
                    }

                    // ====================================================================
                    // GIAI ĐOẠN 2: DỪNG LẠI VÀ TIẾN HÀNH TRÍCH XUẤT BẮT LINK ĐỒNG LOẠT
                    // ====================================================================
                    progress?.Report("Đã tải xong toàn bộ trang web. Bắt đầu trích xuất dữ liệu sạch...");

                    ReadOnlyCollection<IWebElement> videoElements = driver.FindElements(By.XPath("//a[contains(@href, '/video/')]"));
                    progress?.Report($"Tìm thấy tổng cộng {videoElements.Count} khối thẻ video HTML.");

                    foreach (var element in videoElements)
                    {
                        try
                        {
                            string videoUrl = element.GetAttribute("href");
                            if (string.IsNullOrEmpty(videoUrl)) continue;

                            // Tách chuỗi lấy ID video để lọc trùng
                            string videoId = videoUrl.Split(new string[] { "/video/" }, StringSplitOptions.None)[1].Split('?')[0];

                            if (!uniqueVideoIds.Contains(videoId))
                            {
                                string thumbnailUrl = "";

                                // Chiến lược quét đa tầng lọc bỏ hoàn toàn mã Base64 trống
                                try
                                {
                                    // 1. Kiểm tra thuộc tính giấu ảnh dự phòng nâng cao (Thường chứa link thật kể cả khi chưa render)
                                    var imgEl = element.FindElement(By.XPath(".//img"));
                                    string dataSrc = imgEl.GetAttribute("data-src");
                                    string posterSrc = imgEl.GetAttribute("poster");
                                    string normalSrc = imgEl.GetAttribute("src");

                                    if (!string.IsNullOrEmpty(dataSrc) && !dataSrc.StartsWith("data:image"))
                                    {
                                        thumbnailUrl = dataSrc;
                                    }
                                    else if (!string.IsNullOrEmpty(posterSrc) && !posterSrc.StartsWith("data:image"))
                                    {
                                        thumbnailUrl = posterSrc;
                                    }
                                    else if (!string.IsNullOrEmpty(normalSrc) && !normalSrc.StartsWith("data:image"))
                                    {
                                        thumbnailUrl = normalSrc;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        // 2. Thử tìm trong cấu trúc thẻ source avif/webp
                                        var sourceElement = element.FindElement(By.XPath(".//picture/source"));
                                        string srcset = sourceElement.GetAttribute("srcset");
                                        if (!string.IsNullOrEmpty(srcset) && !srcset.StartsWith("data:image"))
                                        {
                                            thumbnailUrl = srcset.Split(' ')[0];
                                        }
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            // 3. Xử lý ảnh nền CSS của thể loại Slide ảnh (Photo mode)
                                            var divStyle = element.FindElement(By.XPath(".//div[contains(@style, 'background-image')]"));
                                            string styleAttr = divStyle.GetAttribute("style");
                                            if (styleAttr.Contains("url(\""))
                                            {
                                                thumbnailUrl = styleAttr.Split(new string[] { "url(\"" }, StringSplitOptions.None)[1].Split('"')[0];
                                            }
                                        }
                                        catch { }
                                    }
                                }

                                // Làm sạch dữ liệu đầu ra: Nếu vẫn dính ảnh rác 1x1 Base64 thì xóa hẳn
                                if (!string.IsNullOrEmpty(thumbnailUrl) && thumbnailUrl.StartsWith("data:image"))
                                {
                                    thumbnailUrl = "";
                                }

                                mediaList.Add(new TikTokMedia
                                {
                                    VideoUrl = videoUrl,
                                    ThumbnailUrl = thumbnailUrl
                                });

                                uniqueVideoIds.Add(videoId);
                            }
                        }
                        catch { continue; }
                    }
                }
                catch (Exception ex)
                {
                    progress?.Report($"[Lỗi Quét] Hệ thống gặp sự cố: {ex.Message}");
                }
                finally
                {
                    try
                    {
                        driver.Close();
                        driver.Quit();
                    }
                    catch { }
                }
            }
            progress?.Report($"Quét hoàn tất! Tổng cộng thu về {mediaList.Count} cặp dữ liệu.");
            
            return mediaList;
        }

        // HÀM MỚI: Hỏi trước yt-dlp tên file gốc sẽ được xuất ra là gì
        public static async Task<string> GetYtdlpFilenameAsync(string url, string outputDirectory)
        {
            try
            {
                Process startInfo = new Process();
                startInfo.StartInfo.FileName = "yt-dlp";
                // Tham số --get-filename ép yt-dlp chỉ in ra chuỗi tên file dự kiến, không tải dữ liệu
                startInfo.StartInfo.Arguments = $"--get-filename -o \"{outputDirectory}\\%(uploader)s - %(title)s.%(ext)s\" \"{url}\"";

                startInfo.StartInfo.UseShellExecute = false;
                startInfo.StartInfo.CreateNoWindow = true;
                startInfo.StartInfo.RedirectStandardOutput = true;

                startInfo.Start();
                string outputFilename = await startInfo.StandardOutput.ReadToEndAsync();
                startInfo.WaitForExit();

                if (!string.IsNullOrWhiteSpace(outputFilename))
                {
                    // Lọc bỏ ký tự xuống dòng dư thừa từ console trả về
                    return outputFilename.Trim();
                }
            }
            catch { }
            return null;
        }


    }

    public class TikTokMedia
    {
        public string VideoUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
