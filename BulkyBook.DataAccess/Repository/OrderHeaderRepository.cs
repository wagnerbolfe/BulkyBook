using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders?.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders?.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void StartStripePaymentID(int id, string sessionId)
        {
            var orderFromDb = _db.OrderHeaders?.FirstOrDefault(u => u.Id == id);

            if (orderFromDb != null)
            {
                orderFromDb.PaymentDate = DateTime.Now;
                orderFromDb.SessionId = sessionId;
            }
        }

        public void FinishStripePaymentID(int id, string paymentIntentId)
        {
            var orderFromDb = _db.OrderHeaders?.FirstOrDefault(u => u.Id == id);

            if (orderFromDb != null)
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
            }
        }
    }
}
