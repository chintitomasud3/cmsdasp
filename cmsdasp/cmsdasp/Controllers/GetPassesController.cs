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
    public class GetPassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetPassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GetPasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetPasses.ToListAsync());
        }

        public  IActionResult Index2()
        {
            return Json(_context.GetPasses.ToList());
        }
        // GET: GetPasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getPass = await _context.GetPasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (getPass == null)
            {
                return NotFound();
            }

            return View(getPass);
        }

        // GET: GetPasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GetPasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phonenumber,Destination,Purpose,Vcardno,Cardtype,Createdby,Cardissuer")] GetPass getPass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(getPass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(getPass);
        }

        // GET: GetPasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getPass = await _context.GetPasses.FindAsync(id);
            if (getPass == null)
            {
                return NotFound();
            }
            return View(getPass);
        }

        // POST: GetPasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(getPass);
                    await _context.SaveChangesAsync();
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

        // GET: GetPasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getPass = await _context.GetPasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (getPass == null)
            {
                return NotFound();
            }

            return View(getPass);
        }

        // POST: GetPasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var getPass = await _context.GetPasses.FindAsync(id);
            _context.GetPasses.Remove(getPass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GetPassExists(int id)
        {
            return _context.GetPasses.Any(e => e.Id == id);
        }
    }
}
