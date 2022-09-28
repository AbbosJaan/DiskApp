using DiskApp.Areas.Identity.Data;
using DiskApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiskApp.Controllers
{
    [Authorize]
    public class DiskController : Controller
    {

        private readonly AppDbContext _appDbContext;
        public DiskController(AppDbContext applicationDbContext)
        {
            this._appDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _appDbContext.Disks.ToListAsync());
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(DiskModel model)
        {
            var disk = new Disk()
            {
                Id = model.Id,
                Quantiy = model.Quantiy,
                Price = model.Price,
                Title = model.Title

            };

            await _appDbContext.Disks.AddAsync(disk);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var disk = await _appDbContext.Disks.FirstOrDefaultAsync(x => x.Id == id);

            if (disk != null)
            {
                var viewModel = new DiskModel()
                {
                    Id = disk.Id,
                    Quantiy = disk.Quantiy,
                    Title = disk.Title,
                    Price = disk.Price

                };
                return await Task.Run(() => View("Details", viewModel));
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
       


        [HttpPost]
        public async Task<IActionResult> Edit(DiskModel model)
        {
            var disk = await _appDbContext.Disks.FindAsync(model.Id);
            if (disk != null)
            {
                disk.Quantiy = model.Quantiy;
                disk.Title = model.Title;
                disk.Price = model.Price;

                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpPost]

        public async Task<IActionResult> Delete(DiskModel model)
        {
            var disk = await _appDbContext.Disks.FindAsync(model.Id);
            if (disk != null)
            {
                _appDbContext.Disks.Remove(disk);
                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        
    }
}
