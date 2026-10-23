namespace LeAn_Day2.Models
{
    /// <summary>
    /// Model dữ liệu sinh viên dùng cho ví dụ minh họa Partial View và View Component
    /// </summary>
    public class StudentInfo
    {
        public string StudentId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public double Gpa { get; set; }

        public string Classification => Gpa switch
        {
            >= 3.6 => "Xuất sắc",
            >= 3.2 => "Giỏi",
            >= 2.5 => "Khá",
            _ => "Trung bình"
        };
    }
}
