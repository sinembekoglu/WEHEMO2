using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wehemo.Business
{
    public interface ICustomer
    {

        Guid Add(string name, string email, string password);//Customer tablosuna yeni veri eklemek için tanımlandı ve geriye verini ID'sini döndüreceği için tipi Guid.
        void Update(Guid CustomerId, string name, string email, string password);//Olan verilerden herhangi birşeyi güncellemek için tanımlanmıştır.Parametrelerden biri Id olan veri günncelleneceği içn ve geriye herhangi bir değer döndürmeyeceği için de void.
        Guid? Login(string email, string password);//Login sayfasına giriş için email ve password gerekli parametrelerimiz onlar olmuştur.Geriye Id değeri döneceği için tipi Guid ve null olma ihtimali gözönüne alınmıştır.
        void Delete(Guid CustomerId);//Geriye değer döndürmeye gerek yok olan bir veri silinecek parametrede Id seçilmiştir bu yüzden.
    }
}

