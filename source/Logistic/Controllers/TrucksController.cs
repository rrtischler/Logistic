using Logistic.Business;
using Logistic.Data;
using Logistic.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Logistic.Controllers
{
  public class TrucksController : Controller
  {
    private readonly LogisticContext _context;

    public TrucksController(LogisticContext context)
    {
      _context = context;
    }

    // GET: Trucks
    public async Task<IActionResult> Index()
    {
      return View(await _context.Trucks.ToListAsync());
    }

    // GET: Trucks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Truck truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
      if (truck == null)
      {
        return NotFound();
      }

      return View(truck);
    }

    // GET: Trucks/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Trucks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Model,ManufacturedYear,ModelYear")] Truck truck)
    {
      if (ModelState.IsValid && VerifyInsertUpdate(truck))
      {
        _context.Add(truck);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(truck);
    }

    // GET: Trucks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Truck truck = await _context.Trucks.FindAsync(id);
      if (truck == null)
      {
        return NotFound();
      }
      return View(truck);
    }

    // POST: Trucks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Model,ManufacturedYear,ModelYear")] Truck truck)
    {
      if (id != truck.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid && VerifyInsertUpdate(truck))
      {
        try
        {
          _context.Update(truck);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!TruckExists(truck.Id))
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
      return View(truck);
    }

    // GET: Trucks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      Truck truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
      if (truck == null)
      {
        return NotFound();
      }

      return View(truck);
    }

    // POST: Trucks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      Truck truck = await _context.Trucks.FindAsync(id);
      _context.Trucks.Remove(truck);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool TruckExists(int id)
    {
      return _context.Trucks.Any(e => e.Id == id);
    }


    [AcceptVerbs("Get", "Post")]
    public IActionResult VerifyManufacturedYear(int ManufacturedYear)
    {
      if (!TruckBusiness.ManufacturedYearValidation(ManufacturedYear))
        return Json("Manufactured year must be the present year.");

      return Json(true);
    }

    [AcceptVerbs("Get", "Post")]
    public IActionResult VerifyModelYear(int ModelYear)
    {
      if (!TruckBusiness.ModelYearValidation(ModelYear))
        return Json("Model year must be the present or next year.");

      return Json(true);
    }

    private bool VerifyInsertUpdate(Truck truck)
    {
      JsonResult resultManufecturedYear = VerifyManufacturedYear(truck.ManufacturedYear) as JsonResult;
      if (!resultManufecturedYear.Value.Equals(true))
        ModelState.AddModelError("ManufacturedYear", resultManufecturedYear.Value.ToString());

      JsonResult resultModelYear = VerifyModelYear(truck.ModelYear) as JsonResult;
      if (!resultModelYear.Value.Equals(true))
        ModelState.AddModelError("ModelYear", resultModelYear.Value.ToString());

      return ModelState.ErrorCount == 0;
    }
  }
}
