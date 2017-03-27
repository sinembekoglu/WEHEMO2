using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wehemo.Business
{
    public class Test : ITest
    {
        public bool Add(Guid CustomerId, string url)
        {
            if (TestControl(url))
            {
                return false;
            }

            {
                var test = new TEST();
                test.ID = Guid.NewGuid();
                test.CUSTOMER_ID = CustomerId;
                test.URL = url;
                test.CREATE_DATE = DateTime.Now;
                using (WehemoDataContext dc = new WehemoDataContext())
                {
                    dc.TESTs.InsertOnSubmit(test);
                    dc.SubmitChanges();
                }
            }
            return true;


        }
        public bool TestControl(string url)
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var test = dc.TESTs.Where(c => c.URL == url).FirstOrDefault();
                if (test == null)
                {
                    return false;
                }
                return true;
            }

        }
        public void Delete(Guid TestId)
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var test = dc.TESTs.Where(c => c.ID == TestId).FirstOrDefault();
                test.DELETED = true;
                dc.SubmitChanges();
            }
        }
        public Dictionary<Guid, string> List()//Tüm değerleri alacağımız için parametre yazmadık. Directionary ile 2 değer döner.
        {
            using (WehemoDataContext dc = new WehemoDataContext())
            {
                var list = dc.TESTs.Where(c => !c.DELETED).Select(c => new { c.ID, c.URL }).ToDictionary(c => c.ID, c => c.URL);//ilk başta c silinmiş mi diye kontrol ettik sonra tablodan birden fazla select işlemi yaptık en sonda ToDictionary de neyi ifade etmek istediğimizi belirtik.
                return list;
            }

        }
        public void AddTestResult(Guid TestId, int statuscode)//Test_Result tablosuna veri ekleyecek 
        {
            using (WehemoDataContext dc = new WehemoDataContext() )
            {
                var testresult = new TEST_RESULT();
                testresult.TEST_ID = TestId;
                testresult.STATUS_CODE = statuscode;
                testresult.CREATE_DATE = DateTime.Now;
                dc.TEST_RESULTs.InsertOnSubmit(testresult);
                dc.SubmitChanges();
            }
        }

        public DTO_TEST_RESULT[] TestDetail(Guid TestId)
        {

            using (WehemoDataContext dc = new WehemoDataContext())
            {

                var ListTestResult = (from c in dc.TEST_RESULTs
                                      join s in dc.STATUS_CODEs on c.STATUS_CODE equals s.CODE
                                      where c.TEST_ID == TestId
                                      select new DTO_TEST_RESULT
                                      {
                                          STATUS = c.STATUS_CODE == 200 ? true : false,

                                          DATE = c.CREATE_DATE,
                                          DESCRIPTION = s.DESCRIPTION
                                      }).ToArray();
                return ListTestResult;
            }



        }

    }
}
