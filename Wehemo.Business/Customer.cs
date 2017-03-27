using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wehemo.Business
{
    public class Customer : ICustomer
    {
        public Guid Add(string name, string email, string password)//Yeni veri eklemek için
        {
            var customer = new CUSTOMER();
            customer.ID = Guid.NewGuid();//Id için yeni guid tipli bir ifade üretir.
            customer.NAME = name;
            customer.EMAIL = email;
            customer.PASSWORD = password;
            customer.CREATE_DATE = DateTime.Now;//Oluşturulma tarihi olarak o anki zamanı alıyoruz.
            customer.DELETED = false;


            using (WehemoDataContext dc = new WehemoDataContext())//DB ile bağlantı sağladık.
            {
                dc.CUSTOMERs.InsertOnSubmit(customer);//customer ile oluşturulan parametrelere göre verileri ekle
                dc.SubmitChanges();//DB ye ekle
            }
            return customer.ID;//Eklenen veri Id'ye göre ekleneceği için geri dönen değer 
        }
        public void Update(Guid CustomerId, string name, string email, string password)
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var customer = dc.CUSTOMERs.Where(c => c.ID == CustomerId).FirstOrDefault();//Güncelleme işlemi var olan Id'ye göre gerçekleşecek burada Where ile Id seçilir yazılan customerId'ye eşit mi kontrol edilir.Eğer değilse FirstorDefault ile null geçilebilir.
                customer.NAME = name;
                customer.EMAIL = email;
                customer.PASSWORD = password;
                customer.UPDATE_DATE = DateTime.Now;//Güncelleme tarihi
                dc.SubmitChanges();//Değişiklikler DB'ye eklenir.
            }//Void olduğu için herhangi bir değeri geri döndürmedik.
        }
        public Guid? Login(string email, string password)
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var customer = dc.CUSTOMERs.Where(c => c.EMAIL == email && c.PASSWORD == password && !c.DELETED).FirstOrDefault();//Email, password yazılanlara eşit mi diye kontrol edildi ve silinmiş mi diye kontrol edildi.
                if (customer == null)//Eğer silimişse deleted null olabilen değer olduğu için kontrol ettik
                {
                    return null;
                }
                return customer.ID;//Null değilse dönen değer.
            }
        }
        public void Delete(Guid CustomerId)
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var customer = dc.CUSTOMERs.Where(c => c.ID == CustomerId).FirstOrDefault();//Silmek için seçilen Id kontrol edilir.
                customer.DELETED = true;//Silindiği doğrulandı.
                dc.SubmitChanges();//DB de değişiklikler kayıtedildi.
            }
        }
    }
}
