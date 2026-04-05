using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CetStudentBook.Data;
using CetStudentBook.Models;

namespace CetStudentBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(int? categoryId)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();
            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId.Value);
            var books = query.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(book);
            }

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = _context.Books.Find(id.Value);
            if (book == null) return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(book);
            }

            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = _context.Books.Find(id.Value);
            if (book == null) return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}