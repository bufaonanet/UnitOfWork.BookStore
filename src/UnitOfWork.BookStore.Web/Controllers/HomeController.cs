using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UnitOfWork.BookStore.Domain.Interfaces.Repositories;
using UnitOfWork.BookStore.Domain.Interfaces;
using UnitOfWork.BookStore.Web.Models;
using UnitOfWork.BookStore.Domain.Entities;

namespace UnitOfWork.BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly IOrderRepository _orderRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _uow;

        public HomeController(IOrderRepository orderRepository, 
                              IStockRepository stockRepository, 
                              IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateOrder()
        {
            var order = new Order(1, 1);
            var item1 = new OrderItem(1, 1, 1, 74.99m);
            var item2 = new OrderItem(1, 2, 2, 430.82m);
            order.AddItem(item1);
            order.AddItem(item2);

            try
            {

                await _orderRepository.CreateOrder(order);
                await _stockRepository.UpdateStockByOrder(order);
                await _uow.Commit();
                TempData["Success"] = "Success!";
            }
            catch (Exception)
            {
                await _uow.Rollback();
                TempData["Error"] = "Error!";
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}