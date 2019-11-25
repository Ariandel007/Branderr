using Brander.Data;
using Brander.Models;
using Brander.Models.ViewModels;
using Brander.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Brander.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public OrderDetailsCart detailCart { get; set; }

        public CartController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {

            detailCart = new OrderDetailsCart()
            {
                Order = new Models.Order()
            };

            detailCart.Order.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.Game = await _db.Game.FirstOrDefaultAsync(m => m.Id == list.GameId);

                detailCart.Order.OrderTotal = detailCart.Order.OrderTotal + (list.Game.Price * list.Count);
                list.Game.Description = SD.ConvertToRawHtml(list.Game.Description);
                if (list.Game.Description.Length > 100)
                {
                    list.Game.Description = list.Game.Description.Substring(0, 99) + "...";
                }
            }
            detailCart.Order.OrderTotalOriginal = detailCart.Order.OrderTotal;


            return View(detailCart);

        }


        public async Task<IActionResult> Summary()
        {

            detailCart = new OrderDetailsCart()
            {
                Order = new Models.Order()
            };

            detailCart.Order.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = await _db.ApplicationUser.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();
            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }

            foreach (var list in detailCart.listCart)
            {
                list.Game = await _db.Game.FirstOrDefaultAsync(m => m.Id == list.GameId);
                detailCart.Order.OrderTotal = detailCart.Order.OrderTotal + (list.Game.Price * list.Count);

            }
            detailCart.Order.OrderTotalOriginal = detailCart.Order.OrderTotal;
            //detailCart.Order.PickupName = applicationUser.Name;
            //detailCart.Order.PhoneNumber = applicationUser.PhoneNumber;
            detailCart.Order.OrderDate = DateTime.Now;




            return View(detailCart);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            detailCart.listCart = await _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value).ToListAsync();


            detailCart.Order = new Models.Order();

            detailCart.Order.PaymentStatus = SD.PaymentStatusPending;
            detailCart.Order.OrderDate = DateTime.Now;
            detailCart.Order.UserId = claim.Value;

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            _db.Order.Add(detailCart.Order);
            await _db.SaveChangesAsync();

            detailCart.Order.OrderTotalOriginal = 0;


            foreach (var item in detailCart.listCart)
            {
                item.Game = await _db.Game.FirstOrDefaultAsync(m => m.Id == item.GameId);
                OrderDetails orderDetails = new OrderDetails
                {
                    OrderId = detailCart.Order.Id,
                    Name = item.Game.Name,
                    Price = item.Game.Price,
                    Count = item.Count,
                    //KeyId = item.KeyId,


                };
                detailCart.Order.OrderTotalOriginal += orderDetails.Count * orderDetails.Price;
                _db.OrderDetails.Add(orderDetails);

            }

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {

            }
            else
            {
                detailCart.Order.OrderTotal = detailCart.Order.OrderTotalOriginal;
            }

            _db.ShoppingCart.RemoveRange(detailCart.listCart);
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, 0);
            await _db.SaveChangesAsync();

            //stripe/////////////////////
            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(detailCart.Order.OrderTotal * 100),
                Currency = "usd",
                Description = "Order ID : " + detailCart.Order.Id,
                Source = stripeToken

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.BalanceTransactionId == null)
            {
                detailCart.Order.PaymentStatus = SD.PaymentStatusRejected;
            }
            else
            {
                detailCart.Order.TransactionId = charge.BalanceTransactionId;
            }

            if (charge.Status.ToLower() == "succeeded")
            {
                
                detailCart.Order.PaymentStatus = SD.PaymentStatusApproved;
                //detailCart.Order.Status = SD.StatusSubmitted;
            }
            else
            {
                detailCart.Order.PaymentStatus = SD.PaymentStatusRejected;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> Plus(int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            cart.Count += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                _db.ShoppingCart.Remove(cart);
                await _db.SaveChangesAsync();

                var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);
            }
            else
            {
                cart.Count -= 1;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);

            _db.ShoppingCart.Remove(cart);
            await _db.SaveChangesAsync();

            var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShoppingCartCount, cnt);


            return RedirectToAction(nameof(Index));
        }

    }
}
