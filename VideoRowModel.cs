using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTool
{
    class VideoRowModel
    {
        public int STT { get; set; }
        public bool Tick { get; set; } // Kiểu bool tương thích với cột Checkbox
        public string Nguon { get; set; } // Lưu chữ "TikTok" hoặc "Douyin"
        public string Url { get; set; }   // Đường dẫn video
        public string ThumNail { get; set; } // Thuộc tính lưu ảnh thực tế để hiển thị lên bảng
        public string Status { get; set; } // Trạng thái: "Sẵn sàng", "Đang tải", "Thành công", "Lỗi"
    }
}
