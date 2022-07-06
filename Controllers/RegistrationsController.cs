using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRecords.Models;

namespace StudentRecords.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly StudentContext _context;

        public RegistrationsController(StudentContext context)
        {
            _context = context;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            var studentContext = _context.Registrations.Include(r => r.Batch).Include(r => r.Course);
            return View(await studentContext.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Batch)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Nic,CourseId,BatchId,TelphoneNo")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id", registration.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", registration.CourseId);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id", registration.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", registration.CourseId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Nic,CourseId,BatchId,TelphoneNo")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batches, "Id", "Id", registration.BatchId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", registration.CourseId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Batch)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registrations == null)
            {
                return Problem("Entity set 'StudentContext.Registrations'  is null.");
            }
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
          return (_context.Registrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
