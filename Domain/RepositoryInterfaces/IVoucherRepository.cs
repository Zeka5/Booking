using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();
        Voucher GetById(int id);
        int NextId();
        Voucher Add(Voucher voucher);
        Voucher Update(Voucher voucher);
        void Delete(Voucher voucher);
        void Subscribe(IObserver observer);
    }
}
