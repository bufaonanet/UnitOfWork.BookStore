using UnitOfWork.BookStore.Data.EFCore.Context;
using UnitOfWork.BookStore.Domain.Entities;
using UnitOfWork.BookStore.Domain.Interfaces.Repositories;

namespace UnitOfWork.BookStore.Data.EFCore.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private bool _disposed = false;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }

    ~OrderRepository()
    {
        Dispose();
    }

    public async Task CreateOrder(Order order)
    {
        await _context.Order.AddAsync(order);
    }

    public void Dispose()
    {
        if(!_disposed) _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
