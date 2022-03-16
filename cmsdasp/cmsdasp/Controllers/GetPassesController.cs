using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cmsdasp.Data;
using cmsdasp.Models;

namespace cmsdasp.Controllers
{

    //public class GetPassesController : Controller
    //{
    //    private readonly ApplicationDbContext _context;

    //    public GetPassesController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: GetPasses
    //    public async Task<IActionResult> Index()
    //    {
    //        return View(await _context.GetPasses.ToListAsync());
    //    }

    //    [HttpGet]
    //    public  IActionResult Index2()
    //    {
    //        return Json(_context.GetPasses.ToList());
    //    }
    //    // GET: GetPasses/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var getPass = await _context.GetPasses
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (getPass == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(getPass);
    //    }

    //    // GET: GetPasses/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: GetPasses/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,Name,Address,Phonenumber,Destination,Purpose,Vcardno,Cardtype,Createdby,Cardissuer")] GetPass getPass)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(getPass);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(getPass);
    //    }

    //    // GET: GetPasses/Edit/5
    //public async Task<IActionResult> Edit(int? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var getPass = await _context.GetPasses.FindAsync(id);
    //    if (getPass == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(getPass);
    //}

    //// POST: GetPasses/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phonenumber,Destination,Purpose,Vcardno,Cardtype,Createdby,Cardissuer")] GetPass getPass)
    //{
    //    if (id != getPass.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(getPass);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!GetPassExists(getPass.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(getPass);
    //}

    //    // GET: GetPasses/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var getPass = await _context.GetPasses
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (getPass == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(getPass);
    //    }

    //    // POST: GetPasses/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var getPass = await _context.GetPasses.FindAsync(id);
    //        _context.GetPasses.Remove(getPass);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool GetPassExists(int id)
    //    {
    //        return _context.GetPasses.Any(e => e.Id == id);
    //    }
    //}
    public class GetpassesController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public GetPass GetPass { get; set; }
        public GetpassesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View(_db.GetPasses.ToList());
        }
        public IActionResult Upsert(int? id)
        {
            GetPass = new GetPass();
            if (id == null)
            {
                //create
                return View(GetPass);
            }
            //update
            GetPass = _db.GetPasses.FirstOrDefault(u => u.Id == id);
            if (GetPass == null)
            {
                return NotFound();
            }
            return View(GetPass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (GetPass.Id == 0)
                {
                    //create
                    _db.GetPasses.Add(GetPass);
                }
                else
                {
                    _db.GetPasses.Update(GetPass);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(GetPass);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.GetPasses.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.GetPasses.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.GetPasses.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
        ////////////////////////////////////////////////////////
        /// <summary>
        private bool GetPassExists(int id)
            {
               return _db.GetPasses.Any(e => e.Id == id);
           }
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getPass = await _db.GetPasses.FindAsync(id);
            if (getPass == null)
            {
                return NotFound();
            }
            return View(getPass);
        }

        //    // POST: GetPasses/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phonenumber,Destination,Purpose,Vcardno,Cardtype,Createdby,Cardissuer")] GetPass getPass)
        {
            if (id != getPass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(getPass);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GetPassExists(getPass.Id))
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
            return View(getPass);
        }





    }

   







}
