﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLoan.Data;
using eLoan.Models;

namespace eLoan.Controllers
{
    public class LoginsController : Controller
    {
        private readonly eLoanContext _context;

        public LoginsController(eLoanContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Login.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.email_address == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("email_address,password,customer_ID")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("email_address,password,customer_ID")] Login login)
        {
            if (id != login.email_address)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.email_address))
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
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.email_address == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var login = await _context.Login.FindAsync(id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(string id)
        {
            return _context.Login.Any(e => e.email_address == id);
        }
    }
}