using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace LeAn_Day2.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string name,int ID = 1)
        {
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");  //can giai thich
            ViewBag.Message = "Hello " + name;
            ViewData["ID"] = ID;
            return View();
        }

        /// <summary>
        /// Action Demo so sánh trực quan giữa Partial View và View Component
        /// </summary>
        public IActionResult Demo()
        {
            // 1. Chuẩn bị dữ liệu cho Partial View (BẮT BUỘC vì Partial View không tự sinh dữ liệu được)
            var sampleStudent = new LeAn_Day2.Models.StudentInfo
            {
                StudentId = "SV099",
                FullName = "Hoàng Minh Tuấn",
                Major = "Công nghệ Thông tin",
                Gpa = 3.92
            };

            // 2. Chú ý: Ta KHÔNG cần chuẩn bị dữ liệu cho TopStudentsViewComponent ở đây,
            // vì ViewComponent tự có logic C# và tự lấy dữ liệu độc lập!

            return View(sampleStudent);
        }
    }
}
