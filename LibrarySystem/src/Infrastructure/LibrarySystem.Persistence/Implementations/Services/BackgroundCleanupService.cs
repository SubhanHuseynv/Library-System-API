using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Persistence.Implementations.Services
{
    internal class BackgroundCleanupService : IBackgroundCleanupService
    {
        private readonly IOrderRepository _repository;

        public BackgroundCleanupService(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task CleanupOrders()
        {
            DateTime today = DateTime.UtcNow.Date;

            IEnumerable<Order> orders = await _repository.GetAllAsync(
                filters: [o => o.CreatedAt <= today]
                );
            
            foreach(var order in orders)
            {
                _repository.Delete(order);
            }

            await _repository.SaveChangesAsync();
        }
    }
}
