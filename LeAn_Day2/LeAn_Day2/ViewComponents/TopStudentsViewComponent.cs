using Microsoft.AspNetCore.Mvc;
using LeAn_Day2.Models;

namespace LeAn_Day2.ViewComponents
{
    /// <summary>
    /// VIEW COMPONENT: TopStudentsViewComponent
    /// - Kế thừa từ class ViewComponent của ASP.NET Core MVC.
    /// - Hoạt động độc lập như một "Mini Controller": có logic C# riêng,
    ///   có thể inject dịch vụ qua constructor (DI) và tự truy vấn dữ liệu.
    /// - Controller của trang cha KHÔNG CẦN biết hay chuẩn bị dữ liệu cho component này.
    /// </summary>
    public class TopStudentsViewComponent : ViewComponent
    {
        // Giả lập danh sách sinh viên (trong thực tế có thể inject DbContext để query)
        private static readonly List<StudentInfo> _databaseStudents = new()
        {
            new StudentInfo { StudentId = "SV001", FullName = "Nguyễn Văn An", Major = "Công nghệ Thông tin", Gpa = 3.85 },
            new StudentInfo { StudentId = "SV002", FullName = "Trần Thị Bích", Major = "Kỹ thuật Phần mềm", Gpa = 3.75 },
            new StudentInfo { StudentId = "SV003", FullName = "Lê Hoàng Cường", Major = "Hệ thống Thông tin", Gpa = 3.65 },
            new StudentInfo { StudentId = "SV004", FullName = "Phạm Mai Dung", Major = "Khoa học Máy tính", Gpa = 3.40 },
            new StudentInfo { StudentId = "SV005", FullName = "Vũ Quốc Đạt", Major = "An toàn Thông tin", Gpa = 3.20 }
        };

        /// <summary>
        /// Phương thức thực thi chính của ViewComponent (tương đương Action trong Controller)
        /// </summary>
        /// <param name="topCount">Số lượng sinh viên muốn lấy</param>
        public async Task<IViewComponentResult> InvokeAsync(int topCount = 3)
        {
            // Tự xử lý logic nghiệp vụ độc lập: sắp xếp theo GPA giảm dần và lấy topCount
            var topStudents = _databaseStudents
                .OrderByDescending(s => s.Gpa)
                .Take(topCount)
                .ToList();

            // Giả lập xử lý bất đồng bộ (async/await)
            await Task.Delay(10);

            // Trả về view mặc định Default.cshtml của ViewComponent
            return View(topStudents);
        }
    }
}
