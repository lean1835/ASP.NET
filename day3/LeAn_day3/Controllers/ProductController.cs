using LeAn_day3.Data;
using LeAn_day3.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeAn_day3.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(string? searchString, string? sortOrder, int pageNumber = 1)
        {
            int pageSize = 5;
            var products = SeedData.products.AsEnumerable();

            // Tìm kiếm theo tên sản phẩm hoặc danh mục
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(p =>
                {
                    var category = SeedData.categories
                        .FirstOrDefault(c => c.Id == p.CategoryId);

                    return p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        || (category != null
                            && category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                });
            }

            // Sắp xếp theo tên hoặc giá sản phẩm
            if (sortOrder == "name_desc")
            {
                products = products.OrderByDescending(p => p.Name);
            }
            else if (sortOrder == "price")
            {
                products = products.OrderBy(p => p.Price);
            }
            else if (sortOrder == "price_desc")
            {
                products = products.OrderByDescending(p => p.Price);
            }
            else
            {
                products = products.OrderBy(p => p.Name);
            }

            // Phân trang danh sách sản phẩm
            int totalItems = products.Count();
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            var pagedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var productViewModels = new List<ProductViewModel>();

            foreach (var product in pagedProducts)
            {
                var category = SeedData.categories
                    .FirstOrDefault(c => c.Id == product.CategoryId);

                var productViewModel = new ProductViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    CategoryName = category?.Name
                };

                productViewModels.Add(productViewModel);
            }

            ViewBag.SearchString = searchString;
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSort = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.PriceSort = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(productViewModels);
        }

        public ActionResult Details([FromRoute] int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var category = SeedData.categories
                .FirstOrDefault(c => c.Id == product.CategoryId);

            var productViewModel = new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price,
                CategoryName = category?.Name
            };

            return View(productViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = SeedData.categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Product product)
        {
            if (!SeedData.categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Please select a valid category.");
            }

            if (ModelState.IsValid)
            {
                product.Id = SeedData.products
                    .Select(p => p.Id)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                SeedData.products.Add(product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = SeedData.categories;
            return View(product);
        }

        public ActionResult Edit([FromRoute] int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = SeedData.categories;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Product product)
        {
            var existingProduct = SeedData.products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            if (!SeedData.categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Please select a valid category.");
            }

            if (ModelState.IsValid)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = SeedData.categories;
            return View(product);
        }

        public ActionResult Delete([FromRoute] int id)
        {
            var product = SeedData.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = SeedData.categories;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            var existingProduct = SeedData.products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            SeedData.products.Remove(existingProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}