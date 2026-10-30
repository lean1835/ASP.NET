using LeAn_day3.Data;
using LeAn_day3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeAn_day3.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index() //khi action hoạt động thì sẽ gọi và trả về view tương ứng
        {
            var _categories = SeedData.categories;
            return View(_categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details([FromRoute] int id)
        {
            //lấy ra category có id tương ứng từ danh sách truyền vào từ SeedData
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound(); //nếu không tìm thấy category thì trả về NotFound
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Category category)
        {
            if(ModelState.IsValid)
            {
                //lay max id trong danh sach categories
                int maxId = SeedData.categories.Max(c => c.Id);
                //set id cho category moi bang maxId + 1
                category.Id = maxId + 1;
                //thêm category mới vào danh sách categories
                SeedData.categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit([FromRoute] int id)
        {
            //lấy ra category có id tương ứng từ danh sách truyền vào từ SeedData
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound(); //nếu không tìm thấy category thì trả về NotFound
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Category category)
        {
            var existingCategory = SeedData.categories.FirstOrDefault(c => c.Id == category.Id);
            if(existingCategory == null)
            {
                return NotFound();
            }
            // Cập nhật thông tin của category tồn tại
            if(ModelState.IsValid)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            var existingCategory = SeedData.categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            SeedData.categories.Remove(existingCategory);
            return RedirectToAction(nameof(Index));
        }
    }
}
