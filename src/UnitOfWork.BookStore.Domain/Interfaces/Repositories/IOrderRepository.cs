using UnitOfWork.BookStore.Domain.Entities;

namespace UnitOfWork.BookStore.Domain.Interfaces.Repositories;

public interface IOrderRepository : IDisposable
{
    Task CreateOrder(Order order);
}