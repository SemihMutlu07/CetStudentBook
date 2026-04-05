using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CetStudentBook.Data;
using CetStudentBook.Models;

namespace CetStudentBook.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public ActionResult Index()
        {
            var items = _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == GetUserId())
                .ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult Add(int bookId)
        {
            var userId = GetUserId();
            var existing = _context.CartItems
                .FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = 1
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var item = _context.CartItems.Find(id);
            if (item != null && item.UserId == GetUserId())
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Buy()
        {
            var userId = GetUserId();
            var cartItems = _context.CartItems
                .Include(c => c.Book)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
                return RedirectToAction("Index");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = cartItems.Sum(c => c.Book.Price * c.Quantity),
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    BookId = c.BookId,
                    ProductName = c.Book.Name,
                    Quantity = c.Quantity,
                    Price = c.Book.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Index", "Order");
        }
    }
}
