using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library__Web_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;

namespace Library__Web_.Tests
{
    [TestClass()]
    public class BookActionTests
    {
        [TestMethod()]
        public void AddBookTest()
        {
            //arrange
            var mockLocker = new Mock<ILogger>();
            var mockRepository = new Mock<IBookRepository>();

            
            //act

            //assert
        }
    }
}