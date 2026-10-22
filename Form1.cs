using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WindowsAPICodePack.Dialogs;
// Đảm bảo có chính xác 2 dòng này

namespace YoutubeTool
{
    public partial class Form1 : Form
    {
        private int chromeProcessId = 0;
        int gStatusRun = -1;
        int gDownloadThreadNum = 1;
        int gScrollNum = 1;
        string[] gLinks = new string[0];
        private System.ComponentModel.BindingList<VideoRowModel> gridDataList = new System.ComponentModel.BindingList<VideoRowModel>();
        public Form1()
        {
            InitializeComponent();
            LogDataGridView.AutoGenerateColumns = false; // Tắt tự tạo cột lộn xộn
            LogDataGridView.AllowUserToAddRows = false;  // Không hiện dòng trống dưới cùng
            LogDataGridView.RowHeadersVisible = false;
            // Ánh xạ các cột có sẵn trên giao diện của bạn
            LogDataGridView.Columns["STT"].DataPropertyName = "STT";
            LogDataGridView.Columns["Tick"].DataPropertyName = "Tick";
            LogDataGridView.Columns["Root"].DataPropertyName = "Nguon";
            LogDataGridView.Columns["Url"].DataPropertyName = "Url";
            LogDataGridView.Columns["Status"].DataPropertyName = "Status";

            // Ánh xạ cột hình ảnh mới thêm của bạn
            LogDataGridView.Columns["ThumNail"].DataPropertyName = "ThumNail";

            // CẤU HÌNH QUAN TRỌNG: Ép ảnh tự co giãn vừa vặn, không bị vỡ khung ô của bảng
            if (LogDataGridView.Columns["ThumNail"] is DataGridViewImageColumn imgCol)
            {
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            // Đặt chiều cao mặc định cho các dòng rộng hơn một chút để dễ nhìn thấy ảnh
            //LogDataGridView.RowTemplate.Height = 60;

            LogDataGridView.DataSource = gridDataList;

            LogDataGridView.ContextMenuStrip = tickMenu;
            LogDataGridView.CellMouseDown += LogDataGridView_CellMouseDown;
            LogDataGridView.DataSource = gridDataList;

        }

        private void ChooseSaveFolderBtn_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo hộp thoại chọn file/thư mục thế hệ mới
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                // 2. CẤU HÌNH QUAN TRỌNG: Ép hộp thoại chỉ cho phép chọn Thư mục (Folder), không chọn File
                dialog.IsFolderPicker = true;

                // Thiết lập tiêu đề cho cửa sổ chọn
                dialog.Title = "Chọn thư mục lưu video tải về";

                // Đặt thư mục xuất hiện mặc định ban đầu là thư mục Downloads của máy tính
                dialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                // Cho phép hiển thị đầy đủ các thư mục ẩn hoặc hệ thống nếu cần
                dialog.AddToMostRecentlyUsedList = false;
                dialog.AllowNonFileSystemItems = false;
                dialog.EnsureFileExists = true;
                dialog.EnsurePathExists = true;
                dialog.EnsureReadOnly = false;

                // 3. Hiển thị hộp thoại lên màn hình
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // Gán đường dẫn thư mục người dùng vừa chọn vào ô TextBox hiển thị trên giao diện của bạn
                    SaveFolderLinkTBox.Text = dialog.FileName;
                }
            }
        }

        // Hàm bổ trợ để ghi Log lên giao diện một cách an toàn (Thread-safe)
        // Hàm AppendLog lúc này cực kỳ ngắn gọn
        private void AppendLog(string text)
        {
            if (listBoxLog.Items.Count > 5000) listBoxLog.Items.Clear();
            string logLine = $"[{DateTime.Now:HH:mm:ss}] {text}";

            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(new Action<string>(AppendLog), text);
            }
            else
            {
                // 1. Thêm dòng log vào ListBox như bình thường
                listBoxLog.Items.Add(logLine);

                // 2. TỰ ĐỘNG TÍNH TOÁN KÍCH THƯỚC ĐỂ HIỆN THANH CỦON NGANG
                using (Graphics g = listBoxLog.CreateGraphics())
                {
                    // Đo độ dài thực tế của dòng chữ dựa trên Font đang dùng của ListBox (đơn vị: Pixel)
                    int textWidth = (int)g.MeasureString(logLine, listBoxLog.Font).Width;

                    // Nếu dòng chữ dài hơn giới hạn cuộn ngang hiện tại, cập nhật lại giới hạn mới
                    if (textWidth > listBoxLog.HorizontalExtent)
                    {
                        // Cộng thêm 20-30 pixel trừ hao khoảng trống ở cuối dòng cho đẹp
                        listBoxLog.HorizontalExtent = textWidth + 25;
                    }
                }

                // 3. Tự động cuộn dọc xuống dòng cuối cùng
                listBoxLog.TopIndex = listBoxLog.Items.Count - 1;

                // Tối ưu bộ nhớ: xóa bớt nếu quá 1000 dòng
                if (listBoxLog.Items.Count > 1000)
                {
                    listBoxLog.Items.RemoveAt(0);
                }
            }
        }

        private async void RunPauseBtn_Click(object sender, EventArgs e)
        {
            gDownloadThreadNum = (int)DownloadThreadUD.Value;
            if (gStatusRun == 0 || gStatusRun == -1)
            {
                RunPauseBtn.Text = "PAUSE";
                gStatusRun = 1;
                /////
                ///
                //string[] links = txtLinks.Lines;
                string outputFolder = SaveFolderLinkTBox.Text;

                //if (gLinks.Length == 0 || string.IsNullOrWhiteSpace(RootLinkTBox.Text)) return;

                //btnDownload.Enabled = false;
                AppendLog("=== BẮT ĐẦU TẢI HÀNG LOẠT ===");

                var downloadProgress = new Progress<string>(text => AppendLog(text));
                SemaphoreSlim semaphore = new SemaphoreSlim(gDownloadThreadNum);

                // Tạo danh sách các Task xử lý tải
                var tasks = new List<Task>();

                for (int i = 0; i < gridDataList.Count; i++)
                {
                    var row = gridDataList[i];
                    if (!row.Tick) continue;

                    int rowIndex = i; // Khóa chỉ số dòng tránh lỗi bất đồng bộ

                    // Tạo một Task xử lý độc lập cho từng video
                    Task downloadTask = Task.Run(async () =>
                    {
                        // Đợi đến lượt nếu đang có 3 luồng chạy rồi
                        await semaphore.WaitAsync();

                        try
                        {
                            UpdateRowStatus(rowIndex, "Đang tải...");
                            AppendLog($"[Luồng Tải] Bắt đầu xử lý dòng số {row.STT}: {row.Url}");

                            // 1. Lấy thông tin tên gốc của yt-dlp
                            string expectedVideoPath = await DownloadHelper.GetYtdlpFilenameAsync(row.Url, outputFolder);

                            // 2. Gọi tiến trình yt-dlp chạy song song dưới nền
                            bool downloadSuccess = await DownloadHelper.RunYtdlpAsync(row.Url, outputFolder, downloadProgress);

                            if (downloadSuccess)
                            {
                                // 3. Xuất file ảnh Thumbnail từ Grid
                                if (row.ThumNail != null && !string.IsNullOrEmpty(expectedVideoPath))
                                {
                                    try
                                    {
                                        string fileNameWithoutExt = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileNameWithoutExtension(expectedVideoPath));
                                        Image img = await DownloadImageAsync(row.ThumNail);
                                        img.Save(fileNameWithoutExt + ".png", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    catch { }
                                }

                                UpdateRowStatus(rowIndex, "Thành công");
                                AppendLog($"[Thành Công] Đã tải xong dòng {row.STT}");
                            }
                            else
                            {
                                UpdateRowStatus(rowIndex, "Thất bại");
                                AppendLog($"[Thất Bại] Lỗi tải dòng {row.STT}");
                            }
                        }
                        finally
                        {
                            // Giải phóng một vị trí trong bộ nhớ đệm để cho phép video tiếp theo vào hàng đợi tải
                            semaphore.Release();
                        }
                    });

                    tasks.Add(downloadTask);
                }

                // Chờ cho đến khi TẤT CẢ các luồng tải chạy xong hoàn toàn
                await Task.WhenAll(tasks);


                AppendLog("=== ĐÃ HOÀN THÀNH TẤT CẢ ===");
                //btnDownload.Enabled = true;
            }
            else if (gStatusRun == 1)
            {
                RunPauseBtn.Text = "RUN";
                gStatusRun = 0;
            }
        }

        // Hàm phụ hỗ trợ cập nhật trạng thái an toàn đa luồng trên DataGridView
        private void UpdateRowStatus(int rowIndex, string statusText)
        {
            if (LogDataGridView.InvokeRequired)
            {
                LogDataGridView.Invoke(new Action(() => UpdateRowStatus(rowIndex, statusText)));
            }
            else
            {
                gridDataList[rowIndex].Status = statusText;
                LogDataGridView.InvalidateRow(rowIndex); // Buộc dòng này vẽ lại để hiển thị chữ mới
            }
        }

        // Hàm tách lấy mã ID độc nhất của video để đặt tên file ảnh không bị trùng lặp
        private string ExtractVideoId(string url)
        {
            try
            {
                if (url.Contains("/video/"))
                {
                    string[] parts = url.Split(new string[] { "/video/" }, StringSplitOptions.None);
                    string idPart = parts[1];
                    if (idPart.Contains("?"))
                    {
                        idPart = idPart.Split('?')[0];
                    }
                    return idPart.Trim(new char[] { '/' });
                }
            }
            catch { }
            return Guid.NewGuid().ToString().Substring(0, 8); // Trả về chuỗi ngẫu nhiên nếu không tách được
        }


        private async void StopBtn_Click(object sender, EventArgs e)
        {
            gDownloadThreadNum = (int)DownloadThreadUD.Value;
            /////
            ///
            //string[] links = txtLinks.Lines;
            string outputFolder = SaveFolderLinkTBox.Text;

            //if (gLinks.Length == 0 || string.IsNullOrWhiteSpace(RootLinkTBox.Text)) return;

            //btnDownload.Enabled = false;
            AppendLog("=== BẮT ĐẦU TẢI HÀNG LOẠT ===");

            var downloadProgress = new Progress<string>(text => AppendLog(text));
            SemaphoreSlim semaphore = new SemaphoreSlim(gDownloadThreadNum);

            // Tạo danh sách các Task xử lý tải
            var tasks = new List<Task>();

            for (int i = 0; i < gridDataList.Count; i++)
            {
                var row = gridDataList[i];
                if (!row.Tick) continue;

                int rowIndex = i; // Khóa chỉ số dòng tránh lỗi bất đồng bộ

                // Tạo một Task xử lý độc lập cho từng video
                Task downloadTask = Task.Run(async () =>
                {
                    // Đợi đến lượt nếu đang có 3 luồng chạy rồi
                    await semaphore.WaitAsync();

                    try
                    {
                        UpdateRowStatus(rowIndex, "Đang tải thummnails...");
                        AppendLog($"[Luồng Tải] Bắt đầu xử lý dòng số {row.STT}: {row.Url}");

                        // 1. Lấy thông tin tên gốc của yt-dlp
                        string expectedVideoPath = await DownloadHelper.GetYtdlpFilenameAsync(row.Url, outputFolder);
                        // 3. Xuất file ảnh Thumbnail từ Grid
                        if (row.ThumNail != null && !string.IsNullOrEmpty(expectedVideoPath))
                        {
                            try
                            {
                                string fileNameWithoutExt = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileNameWithoutExtension(expectedVideoPath));
                                Image img = await DownloadImageAsync(row.ThumNail);
                                img.Save(fileNameWithoutExt + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                            catch { }
                        }
                        UpdateRowStatus(rowIndex, "Thành công thumnails");
                        AppendLog($"[Thành Công] Đã tải xong dòng {row.STT}");
                    }
                    finally
                    {
                        // Giải phóng một vị trí trong bộ nhớ đệm để cho phép video tiếp theo vào hàng đợi tải
                        semaphore.Release();
                    }
                });

                tasks.Add(downloadTask);
            }

            // Chờ cho đến khi TẤT CẢ các luồng tải chạy xong hoàn toàn
            await Task.WhenAll(tasks);


            AppendLog("=== ĐÃ HOÀN THÀNH TẤT CẢ ===");
            //btnDownload.Enabled = true;


        }

        private async void CheckRootLinkBtn_Click(object sender, EventArgs e)
        {
            string inputLink = RootLinkTBox.Text.Trim();
            if (string.IsNullOrEmpty(inputLink)) return;

            CheckRootLinkBtn.Enabled = false;
            gridDataList.Clear();

            var progressIndicator = new Progress<string>(text => AppendLog(text));

            // 1. Quét lấy danh sách link thô từ Selenium Helper của bạn
            List<TikTokMedia> rawDataList = await Task.Run(() =>
                DownloadHelper.ExtractChannelVideos(inputLink, progressIndicator, gScrollNum, out chromeProcessId)
            );

            if (rawDataList != null && rawDataList.Count > 0)
            {
                AppendLog($"Đã quét xong. Đang thực hiện tải ảnh ThumNail hiển thị lên bảng Grid...");
                string sourceName = inputLink.Contains("douyin.com") ? "Douyin" : "TikTok";
                int index = 1;

                foreach (var item in rawDataList)
                {
                    // 2. Tải ngầm hình ảnh thu nhỏ từ đường dẫn URL
                    //Image imgThumb = await DownloadImageAsync(item.ThumbnailUrl);

                    // 3. Đẩy đầy đủ cấu trúc dữ liệu bao gồm cả hình ảnh lên bảng
                    gridDataList.Add(new VideoRowModel
                    {
                        STT = index,
                        Tick = true,
                        Nguon = sourceName,
                        Url = item.VideoUrl,
                        ThumNail = item.ThumbnailUrl, // Nạp đối tượng Image thực tế vào đây
                        //ThumNail = null, // Tạm thời để trống ảnh để giao diện xuất hiện ngay lập tức
                        Status = "Sẵn sàng"
                    });
                    index++;

                }

                //_ = Task.Run(async () =>
                //{
                //    for (int i = 0; i < rawDataList.Count; i++)
                //    {
                //        string thumbUrl = rawDataList[i].ThumbnailUrl;
                //        Image img = await DownloadImageAsync(thumbUrl);

                //        if (img != null && i < gridDataList.Count)
                //        {
                //            // Cập nhật ảnh vào danh sách, DataGridView sẽ tự động đổi từ ô trống thành ảnh thật
                //            this.Invoke(new Action(() => {
                //                gridDataList[i].ThumNail = img;
                //                LogDataGridView.InvalidateRow(i); // Ép dòng này vẽ lại để hiển thị ảnh ngay lập tức
                //            }));
                //        }
                //    }
                //});

                AppendLog($"Đã hiển thị toàn bộ {rawDataList.Count} video kèm ảnh Thumbnail lên bảng!");
            }
            else
            {
                AppendLog("Không lấy được dữ liệu video nào.");
            }

            // === GỌI LỆNH TẮT ĐÚNG CỬA SỔ CHROME CỦA TOOL ===
            AppendLog("Đang đóng cửa sổ Chrome gỡ lỗi của tool...");
            KillToolChromeOnly(chromeProcessId);

            CheckRootLinkBtn.Enabled = true;
        }

        private async Task<System.Drawing.Image> DownloadImageAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;

            // Sửa lỗi mã hóa ký tự đặc biệt của liên kết ảnh CDN (&amp; -> &)
            url = WebUtility.HtmlDecode(url).Trim();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Giả lập trình duyệt để tránh bị CDN TikTok chặn
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
                    client.DefaultRequestHeaders.Add("Accept", "image/avif,image/webp,image/apng,image/*,*/*;q=0.8");

                    if (url.Contains("tiktokcdn.com"))
                        client.DefaultRequestHeaders.Add("Referer", "https://tiktok.com");

                    // 1. Tải mảng byte ảnh gốc từ máy chủ
                    byte[] imageBytes = await client.GetByteArrayAsync(url);

                    using (MemoryStream msInput = new MemoryStream(imageBytes))
                    {
                        // 2. Sử dụng bộ giải mã hệ thống của Windows để đọc ảnh .webp/.avif
                        BitmapDecoder decoder = BitmapDecoder.Create(
                            msInput,
                            BitmapCreateOptions.None,
                            BitmapCacheOption.OnLoad
                        );

                        BitmapSource bitmapSource = decoder.Frames[0];

                        // 3. Chuyển đổi BitmapSource của hệ thống sang Bitmap của WinForms để hiển thị lên bảng Grid
                        using (MemoryStream msOutput = new MemoryStream())
                        {
                            PngBitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                            encoder.Save(msOutput);
                            msOutput.Position = 0;

                            return System.Drawing.Image.FromStream(msOutput);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi nếu link ảnh bị hết hạn token bảo mật
                AppendLog($"   [Lỗi Tải Ảnh]: {ex.Message}");
                return null;
            }
        }
        // Hàm nâng cấp: Chỉ tắt đúng tiến trình có mã PID được chỉ định
        private void KillToolChromeOnly(int pid)
        {
            if (pid == 0) return; // Nếu không mở được thì bỏ qua

            try
            {
                // Lấy chính xác tiến trình đang chạy dựa vào mã PID lưu trữ
                Process toolProcess = Process.GetProcessById(pid);

                if (toolProcess != null && !toolProcess.HasExited)
                {
                    toolProcess.Kill(); // Chỉ đóng đúng cửa sổ này
                    toolProcess.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Đôi khi chrome tự sinh thêm tiến trình con, ta quét thêm lớp bảo vệ theo cổng 9222
                try
                {
                    Process[] chromeProcesses = Process.GetProcessesByName("chrome");
                    foreach (var p in chromeProcesses)
                    {
                        // Kiểm tra nếu tiến trình con đó cùng nhóm quản lý hoặc mở từ profile tạm của tool
                        if (p.StartInfo.Arguments != null && p.StartInfo.Arguments.Contains("--remote-debugging-port=9222"))
                        {
                            p.Kill();
                        }
                    }
                }
                catch { }
            }
        }

        private void tíchChọnTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataList.Count == 0) return;

            foreach (var row in gridDataList)
            {
                row.Tick = true; // Bật checkbox
            }

            LogDataGridView.Refresh(); // Vẽ lại bảng để cập nhật màn hình
            AppendLog("Đã tích chọn toàn bộ các video trong danh sách.");
        }

        private void bỏTíchTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataList.Count == 0) return;

            foreach (var row in gridDataList)
            {
                row.Tick = false; // Tắt checkbox
            }

            LogDataGridView.Refresh();
            AppendLog("Đã hủy chọn toàn bộ các video trong danh sách.");
        }

        private void đảoNgượcVùngChọnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataList.Count == 0) return;

            foreach (var row in gridDataList)
            {
                row.Tick = !row.Tick; // Phép phủ định đảo bit liên tục
            }

            LogDataGridView.Refresh();
            AppendLog("Đã đảo ngược trạng thái tích chọn trên bảng.");
        }

        private void xóaToànBộDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng có dữ liệu không trước khi hỏi xóa
            if (gridDataList.Count == 0) return;

            // Hiển thị một hộp thoại cảnh báo xác nhận trước khi xóa (Tránh việc người dùng bấm nhầm)
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sạch toàn bộ video đang hiển thị trên bảng không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            gridDataList.Clear();

            AppendLog("Đã dọn dẹp sạch sẽ toàn bộ danh sách trên bảng DataGridView.");
        }

        private void LogDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            // Kiểm tra xem có đúng là bấm chuột phải (Right Click) hay không
            // Đồng thời đảm bảo click trúng hàng chứa dữ liệu hợp lệ (e.RowIndex >= 0)
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Tùy chọn 1: Nếu bạn muốn người dùng click chuột phải vào BẤT KỲ CỘT NÀO trên hàng đó đều hiện menu:
                LogDataGridView.ClearSelection();
                LogDataGridView.Rows[e.RowIndex].Selected = true; // Focus dòng hiện tại

                /* 
                // Tùy chọn 2: Nếu bạn chỉ muốn hiện menu khi họ click ĐÚNG vào cột có tên "Tick"
                if (dgvVideos.Columns[e.ColumnIndex].Name == "Tick")
                {
                    dgvVideos.ClearSelection();
                    dgvVideos.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    dgvVideos.ContextMenuStrip.Hide(); // Ẩn menu đi nếu bấm sang cột khác
                }
                */
            }
        }
    }
}
