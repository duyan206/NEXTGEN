using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ktr_GiuaKy.Data;
using Ktr_GiuaKy.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ktr_GiuaKy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ComputerStoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ComputerStoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Client: List of Products (Grid Card)
        public async Task<IActionResult> Index(string searchString, int? brandId)
        {
            var query = _context.Products.Include(p => p.Brand).Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString) 
                    || (p.ShortDescription != null && p.ShortDescription.Contains(searchString)) 
                    || (p.Brand != null && p.Brand.Name.Contains(searchString)));
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId.Value);
            }

            var products = await query.ToListAsync();
            ViewBag.Brands = await _context.Brands.ToListAsync();
            ViewBag.SearchString = searchString;
            ViewBag.SelectedBrandId = brandId;
            return View(products);
        }

        // Client: Product Detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // Admin: Product Dashboard (Table List)
        public async Task<IActionResult> AdminList(string searchString)
        {
            var query = _context.Products.Include(p => p.Brand).Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString) 
                    || (p.Brand != null && p.Brand.Name.Contains(searchString)) 
                    || (p.Sku != null && p.Sku.Contains(searchString)));
            }

            var products = await query.ToListAsync();
            ViewBag.SearchString = searchString;
            return View(products);
        }

        // Admin: Create GET
        public async Task<IActionResult> Create()
        {
            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View();
        }

        // Admin: Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BrandId,CategoryId,Price,Quantity,ShortDescription,FullDescription")] Product product, IFormFile? imageFile)
        {
            // Remove automatic properties and navigation properties from validation check
            ModelState.Remove(nameof(product.Sku));
            ModelState.Remove(nameof(product.Slug));
            ModelState.Remove(nameof(product.Brand));
            ModelState.Remove(nameof(product.Category));

            if (ModelState.IsValid)
            {
                // Generate a SKU
                var brand = await _context.Brands.FindAsync(product.BrandId);
                string brandPrefix = brand != null ? brand.Name.Substring(0, Math.Min(3, brand.Name.Length)).ToUpper() : "GEN";
                product.Sku = $"{brandPrefix}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

                // Generate a Slug
                product.Slug = product.Name.ToLower()
                    .Replace(" ", "-")
                    .Replace("/", "-")
                    .Replace("\\", "-")
                    .Replace("\"", "")
                    .Replace("'", "")
                    .Trim();
                product.Slug += "-" + Guid.NewGuid().ToString().Substring(0, 4);

                product.Status = "active";
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                // Handle Image Upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    product.MainImage = "/uploads/" + uniqueFileName;
                }
                else
                {
                    // Fallback default image
                    product.MainImage = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?q=80&w=600&auto=format&fit=crop";
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminList));
            }

            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Admin: Edit GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Admin: Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sku,Name,Slug,BrandId,CategoryId,Price,Quantity,ShortDescription,FullDescription,MainImage,Status,CreatedAt")] Product product, IFormFile? imageFile)
        {
            if (id != product.Id) return NotFound();

            ModelState.Remove(nameof(product.Brand));
            ModelState.Remove(nameof(product.Category));

            if (ModelState.IsValid)
            {
                try
                {
                    product.UpdatedAt = DateTime.Now;

                    // Handle Image Upload if a new file is uploaded
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        product.MainImage = "/uploads/" + uniqueFileName;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(AdminList));
            }

            ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Admin: Delete POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminList));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
