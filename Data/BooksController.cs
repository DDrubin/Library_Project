using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using System.Diagnostics;
using Library.Data;
using System.Dynamic;
using System;
using System.Security.Claims;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
// https://www.googleapis.com/books/v1/volumes?q=Subject:ART&key=AIzaSyBaSYsg_m-ZiP0HzGaezNy6UBq6Hc8GsvM
namespace Library
{// TODO: work around security, roles, views. Make view more adaptible
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO: make default index of list with small number of user added books
        // TODO: make links to views with googleApi and local DB
        // TODO: make registration avaliable for googleApi   11/11/2021

        // GET: Books 
        public async Task<IActionResult> Index()
        {
            var mc = new GoogleBookApiService();
           // var book = await _context.Books.ToListAsync();
          //  var googleApi = mc.Search(0);
           // var tuple = new Tuple<List<Models.Book>, List<Models.Book>>(book, googleApi);
            return View(mc.Search(0));
        }
        // TODO: make Registered view with description from another model like in Book/Register view
        // GET: Registered books for user
        [Authorize]
        public async Task<IActionResult> RegisteredIndex()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.Reservations.Where(t => t.UserID == userId).ToListAsync());

        }
       
        // GET: Books/Register
        [Authorize]
        public async Task<IActionResult> Register(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            //Declare models for Tuple
            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            var bookReservations = new BookReservation();
           
            if (book == null)
            {
                return NotFound();
            }
            var tuple = new Tuple<Models.Book, Models.BookReservation>(book, bookReservations);
            return View(tuple);
            

        }

        // POST: Books/Register

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("BookID", Prefix = "Item2")] BookReservation bookReservations)
        {
            //Adding to Registered DB
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                DateTime localDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    bookReservations.ReservationDate = localDate;
                    bookReservations.UserID = userId;

                    _context.Add(bookReservations);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bookReservations);
        }


        // Books/Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,PublishDate,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Author,PublishDate,Description")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
