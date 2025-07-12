using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionMVC.Data;
using PetAdoptionMVC.Models;
using System.Security.Claims;

namespace PetAdoptionMVC.Controllers
{
    public class PetAdsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PetAdsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
            
        

        // GET: PetAds
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetAds.ToListAsync());
        }

        // GET: PetAds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petAd = await _context.PetAds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petAd == null)
            {
                return NotFound();
            }

            return View(petAd);
        }

        // GET: PetAds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetAds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Category,PostedDate,ImageFile")] PetAd petAd, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string FileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(wwwRootPath + "/images/", FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                petAd.ImagePath = "/images/" + FileName;
            }
            petAd.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(petAd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(petAd);
        }


        // POST: PetAds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImagePath")] PetAd petAd, IFormFile? ImageFile)
        {
            if (id != petAd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(wwwRootPath + "/images/", FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    petAd.ImagePath = "/images/" + FileName;
                }

                try
                {
                    _context.Update(petAd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PetAds.Any(e => e.Id == petAd.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(petAd);
        }

        // GET: PetAds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petAd = await _context.PetAds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petAd == null)
            {
                return NotFound();
            }

            return View(petAd);
        }

        // POST: PetAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petAd = await _context.PetAds.FindAsync(id);
            if (petAd != null)
            {
                _context.PetAds.Remove(petAd);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetAdExists(int id)
        {
            return _context.PetAds.Any(e => e.Id == id);
        }
    }
}
