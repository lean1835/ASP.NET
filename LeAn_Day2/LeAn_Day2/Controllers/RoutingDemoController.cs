using Microsoft.AspNetCore.Mvc;

namespace LeAn_Day2.Controllers
{
    /// <summary>
    /// Controller minh họa sự khác nhau giữa Conventional Routing và Attribute Routing
    /// </summary>
    public class RoutingDemoController : Controller
    {
        // -------------------------------------------------------------
        // TRANG TỔNG QUAN
        // -------------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }

        // -------------------------------------------------------------
        // VÍ DỤ 1: CONVENTIONAL ROUTING (Định tuyến theo quy ước)
        // -------------------------------------------------------------
        // Action này KHÔNG có attribute [Route].
        // Nó được định tuyến bởi cấu hình trong Program.cs:
        // pattern: "{controller=Home}/{action=Index}/{id?}"
        //
        // URL truy cập:
        // - /RoutingDemo/Standard
        // - /RoutingDemo/Standard/42
        // -------------------------------------------------------------
        public IActionResult Standard(int? id)
        {
            ViewBag.RouteType = "Conventional Routing (Theo quy ước Program.cs)";
            ViewBag.MatchedPattern = "{controller=Home}/{action=Index}/{id?}";
            ViewBag.RequestedUrl = HttpContext.Request.Path;
            ViewBag.ParamId = id.HasValue ? id.Value.ToString() : "(Không truyền id)";
            ViewBag.Explanation = "Đường dẫn được framework tự động khớp theo cặp TênController/TênAction/ThamSố.";

            return View("RouteResult");
        }

        // -------------------------------------------------------------
        // VÍ DỤ 2: ATTRIBUTE ROUTING (Đổi URL thân thiện, tùy biến)
        // -------------------------------------------------------------
        // Thuộc tính [Route] ghi đè quy ước mặc định.
        // URL truy cập BÂY GIỜ LÀ:
        // - /dinh-tuyen-tuy-bien
        // (Nếu gõ /RoutingDemo/Custom sẽ bị 404 vì Attribute Route đã thay thế hoàn toàn)
        // -------------------------------------------------------------
        [Route("dinh-tuyen-tuy-bien")]
        public IActionResult Custom()
        {
            ViewBag.RouteType = "Attribute Routing (Tùy biến URL trực tiếp)";
            ViewBag.MatchedPattern = "[Route(\"dinh-tuyen-tuy-bien\")]";
            ViewBag.RequestedUrl = HttpContext.Request.Path;
            ViewBag.ParamId = "N/A";
            ViewBag.Explanation = "URL này hoàn toàn độc lập với tên Controller và Action method, giúp tạo URL ngắn gọn hoặc chuẩn SEO.";

            return View("RouteResult");
        }

        // -------------------------------------------------------------
        // VÍ DỤ 3: ATTRIBUTE ROUTING CÓ RÀNG BUỘC (Constraints & Slug)
        // -------------------------------------------------------------
        // [Route("san-pham/{id:int}/{slug?}")]
        // - {id:int}: Ràng buộc kiểu dữ liệu - BẮT BUỘC phải là số nguyên.
        // - {slug?}: Tham số tùy chọn (có dấu ? ở cuối).
        //
        // URL hợp lệ:
        // - /san-pham/105/laptop-dell
        // - /san-pham/105
        //
        // URL KHÔNG hợp lệ (trả về lỗi 404):
        // - /san-pham/chu-cai-khong-phai-so (vì vi phạm ràng buộc :int)
        // -------------------------------------------------------------
        [Route("san-pham/{id:int}/{slug?}")]
        public IActionResult Product(int id, string? slug)
        {
            ViewBag.RouteType = "Attribute Routing có Ràng buộc (Route Constraints)";
            ViewBag.MatchedPattern = "[Route(\"san-pham/{id:int}/{slug?}\")]";
            ViewBag.RequestedUrl = HttpContext.Request.Path;
            ViewBag.ParamId = id.ToString();
            ViewBag.Slug = string.IsNullOrEmpty(slug) ? "(Không có slug)" : slug;
            ViewBag.Explanation = $"Nhận diện thành công ID là số: {id} và slug là: '{slug}'. Nếu thay ID bằng chữ cái, hệ thống sẽ trả về lỗi 404 ngay lập tức.";

            return View("RouteResult");
        }
    }
}
