using LeAn_day3.Models;

namespace LeAn_day3.Data
{
    public class SeedData
    {
        public static List<Category> categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Điện thoại",
                Description = "Điện thoại di động"
            },
            new Category()
            {
                Id = 2,
                Name = "Máy tính",
                Description = "Máy tính và thiết bị hiển thị"
            },
            new Category()
            {
                Id = 3,
                Name = "Phụ kiện",
                Description = "Phụ kiện điện tử"
            }
        };

        public static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Điện thoại Alpha", Price = 12500000m, Description = "Điện thoại màn hình 6.1 inch", CategoryId = 1 },
            new Product() { Id = 2, Name = "Điện thoại Beta", Price = 18900000m, Description = "Điện thoại bộ nhớ 256 GB", CategoryId = 1 },
            new Product() { Id = 3, Name = "Điện thoại Gamma", Price = 7200000m, Description = "Điện thoại pin dung lượng cao", CategoryId = 1 },
            new Product() { Id = 4, Name = "Điện thoại Delta", Price = 9900000m, Description = "Điện thoại hỗ trợ kết nối 5G", CategoryId = 1 },
            new Product() { Id = 5, Name = "Laptop văn phòng 14 inch", Price = 15900000m, Description = "Laptop dùng cho học tập và văn phòng", CategoryId = 2 },
            new Product() { Id = 6, Name = "Laptop hiệu năng cao", Price = 24500000m, Description = "Laptop phục vụ thiết kế và xử lý đồ họa", CategoryId = 2 },
            new Product() { Id = 7, Name = "Laptop gọn nhẹ 13 inch", Price = 21900000m, Description = "Laptop mỏng nhẹ, thuận tiện di chuyển", CategoryId = 2 },
            new Product() { Id = 8, Name = "Màn hình máy tính 27 inch", Price = 5690000m, Description = "Màn hình độ phân giải Full HD", CategoryId = 2 },
            new Product() { Id = 9, Name = "Tai nghe không dây", Price = 1250000m, Description = "Tai nghe Bluetooth có micro", CategoryId = 3 },
            new Product() { Id = 10, Name = "Chuột máy tính không dây", Price = 390000m, Description = "Chuột không dây dùng cho laptop", CategoryId = 3 },
            new Product() { Id = 11, Name = "Bàn phím cơ", Price = 1490000m, Description = "Bàn phím cơ kết nối USB", CategoryId = 3 },
            new Product() { Id = 12, Name = "Bộ sạc nhanh 65W", Price = 890000m, Description = "Bộ sạc nhanh dùng cho điện thoại và máy tính bảng", CategoryId = 3 }
        };
    }
}
