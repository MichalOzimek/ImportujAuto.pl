using ImportCars.Data;
using ImportCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace ImportCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuctionsController : Controller
    {
        private readonly Context _context;

        public AuctionsController(Context context)
        {
            _context = context;
        }

        // GET: Admin/Auctions
        public async Task<IActionResult> Index()
        {
              return _context.Auctions != null ? 
                          View(await _context.Auctions.ToListAsync()) :
                          Problem("Entity set 'Context.Auctions'  is null.");
        }

        // GET: Admin/Auctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auctions = await _context.Auctions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctions == null)
            {
                return NotFound();
            }

            return View(auctions);
        }

        // GET: Admin/Auctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Auctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EndDate,ProductionYear,EngineType,EngineCapacity,Url,Price")] Auctions auctions, List<IFormFile> images)
        {
            // Sprawdzam czy zdjęcie nie ma złego rozszerzenia
            bool fileValid = ValidateFiles(images, ModelState);
            if (!fileValid)
            {
                return View(auctions);
            }

            if (ModelState.IsValid)
            {
                _context.Add(auctions);
                await _context.SaveChangesAsync();

                // Zdjęcia
                List<Images> savedPhotos = await Helper.SavePhotosAsync(auctions.Id, images, nameof(auctions), nameof(Images.AuctionId), _context);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Logowanie błędów ModelState
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // Tutaj możesz zrobić coś z błędem, np. zalogować go
                        Console.WriteLine($"Błąd walidacji: {error.ErrorMessage}");
                    }
                }

                return View(auctions);
            }
        }

        // GET: Admin/Auctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auctions = await _context.Auctions.FindAsync(id);
            if (auctions == null)
            {
                return NotFound();
            }

            await _context.Entry(auctions).Collection(x => x.Images).LoadAsync();

            return View(auctions);
        }

        // POST: Admin/Auctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EndDate,ProductionYear,EngineType,EngineCapacity,Url,Price")] Auctions auctions, List<IFormFile> images, string? selectedPhotosIds)
        {
            // Sprawdzam czy zdjęcie nie ma złego rozszerzenia
            bool fileValid = ValidateFiles(images, ModelState);
            if (!fileValid)
                return View(auctions);

            if (id != auctions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Zdjęcia
                    List<Images> savedPhotos = await Helper.SavePhotosAsync(auctions.Id, images, nameof(auctions), nameof(Images.AuctionId), _context);

                    _context.Update(auctions);
                    await _context.SaveChangesAsync();

                    //usuawnie zdjęć
                    await Helper.RemovePhotosAsync<Images>(_context, selectedPhotosIds, "auctions", x => x.AuctionId);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionsExists(auctions.Id))
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
            else
            {
                // Wyświetl błędy ModelState w konsoli lub zapisz je gdzieś w celu analizy
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            return View(auctions);
        }

        // GET: Admin/Auctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Auctions == null)
            {
                return NotFound();
            }

            var auctions = await _context.Auctions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auctions == null)
            {
                return NotFound();
            }

            return View(auctions);
        }

        // POST: Admin/Auctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Auctions == null)
            {
                return Problem("Entity set 'Context.Auctions'  is null.");
            }


            var auctions = _context.Auctions.Include(n => n.Images)
                        .FirstOrDefault(n => n.Id == id);

            if (auctions != null)
            {
                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                if (auctions.Images != null)
                {
                    _context.Images.RemoveRange(auctions.Images);

                    foreach (var item in _context.Images.Where(x => x.Id == auctions.Id))
                    {
                        FileInfo file = new FileInfo(rootPath + item.Path.Replace("~", ""));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }

                _context.Auctions.Remove(auctions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuctionsExists(int id)
        {


          return (_context.Auctions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public bool ValidateFiles(List<IFormFile> photos, ModelStateDictionary modelState)
        {
            //walidacja po stronie serwera, jeśli chcesz dodać wiecej rozszerzenie zmień również liste rozszerzeń w pliku admin-panel.js
            bool filesValid = true;

            foreach (var item in photos)
            {
                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(item.FileName.ToLower())))
                {
                    modelState.AddModelError("photoExtError", "Akceptowane są tylko pliki .png .jpeg .jpg");
                    filesValid = false;
                }
            }

            return filesValid;
        }

        private async Task<List<Images>> SavePhotosAsync(int imageId, List<IFormFile> photos)
        {
            List<Images> savedPhotos = new List<Images>();
            if (photos != null)
            {
                foreach (var formFile in photos)
                {
                    Random random = new Random();
                    string randomString = new string(Enumerable.Range(0, 10).Select(i => (char)('a' + random.Next(26))).ToArray());
                    string? formFileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName?.Trim('"');
                    string filePath = "auction_" + imageId + "_image_" + randomString + Path.GetExtension(formFileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "projects", filePath);

                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    Images photo = new Images("~/img/auctions/" + filePath)
                    {
                        AuctionId = imageId
                    };
                    _context.Images.Add(photo);
                    await _context.SaveChangesAsync();

                    savedPhotos.Add(photo);
                }
            }
            return savedPhotos;
        }
    }
}
