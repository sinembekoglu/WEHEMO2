using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wehemo.Business
{
    public interface ITest
    {
        bool Add(Guid CustomerId, string url);
        bool TestControl(string url);
        void Delete(Guid TestId);
        Dictionary<Guid, string> List();
        void AddTestResult(Guid TestId, int statuscode);
        DTO_TEST_RESULT[] TestDetail(Guid TestId);
    }
}
