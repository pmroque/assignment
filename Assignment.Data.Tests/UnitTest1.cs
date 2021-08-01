using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Assignment.Data.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using(var context = new AssignmentDBContext())
            {
                var pol = context.Transactions.ToList();

                var pol1 = context.Statuses.ToList();
            }
            
            Assert.AreEqual(1, 1);
        }
    }
}
