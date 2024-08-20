using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class VoucherRepository:IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/voucher.csv";

        private readonly Serializer<Voucher> serializer;

        private List<Voucher> vouchers;

        public Subject VoucherSubject;


        public VoucherRepository()
        {
            serializer = new Serializer<Voucher>();
            vouchers = serializer.FromCSV(FilePath);
            VoucherSubject = new Subject();
        }
        public List<Voucher> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public List<Voucher> GetByUserId(int userId)
        {
            vouchers = serializer.FromCSV(FilePath);
            return vouchers.FindAll(v => v.TouristId == userId);
        }
        public List<Voucher> GetValidVouchersByUserId(int userId)
        {
            vouchers = serializer.FromCSV(FilePath);
            return vouchers.FindAll(v => v.TouristId == userId && v.Status == ValidityStatus.VALID);
        }
        public Voucher GetById(int id)
        {
            vouchers = serializer.FromCSV(FilePath);
            return vouchers.Find(v => v.Id == id);
        }
        public int NextId()
        {
            vouchers = serializer.FromCSV(FilePath);
            if (vouchers.Count < 1)
            {
                return 1;
            }
            return vouchers.Max(t => t.Id) + 1;
        }

        public Voucher Add(Voucher voucher)
        {
            vouchers = serializer.FromCSV(FilePath);
            voucher.Id = NextId();
            vouchers.Add(voucher);
            serializer.ToCSV(FilePath, vouchers);
            return voucher;
        }
        public Voucher Update(Voucher voucher)
        {
            vouchers = serializer.FromCSV(FilePath);
            Voucher current = vouchers.Find(v => v.Id == voucher.Id);
            int index = vouchers.IndexOf(current);
            vouchers.Remove(current);
            vouchers.Insert(index, voucher);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, vouchers);
            VoucherSubject.NotifyObservers();
            return voucher;
        }


        public void Delete(Voucher voucher)
        {
            vouchers = serializer.FromCSV(FilePath);
            Voucher founded = vouchers.Find(v => v.Id == voucher.Id);
            vouchers.Remove(founded);
            serializer.ToCSV(FilePath, vouchers);
        }

        public void Subscribe(IObserver observer)
        {
            VoucherSubject.Subscribe(observer);
        }
    }
}
