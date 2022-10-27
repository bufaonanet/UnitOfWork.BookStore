using UnitOfWork.BookStore.Domain.Entities;

namespace UnitOfWork.BookStore.Domain.Interfaces.Repositories;

public interface IStockRepository : IDisposable
{
    Task UpdateStockByOrder(Order order);
}

