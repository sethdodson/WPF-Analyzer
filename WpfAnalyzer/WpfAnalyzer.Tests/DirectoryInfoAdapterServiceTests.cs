using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAnalyzer.Services;

namespace WpfAnalyzer.Tests
{
    [TestClass]
    public class DirectoryInfoAdapterServiceTests
    {
        [TestMethod]
        public void CreateDirectoryInfo_Returns_DirectoryInfoAdapter_With_Expected_Path_Given_A_Path()
        {
            //Arrange
            var path = @"C:\path";
            var directoryInfoAdapterService = new DirectoryInfoAdapterService();
            var expected = @"C:\path";

            //Act
            var actual = directoryInfoAdapterService.CreateDirectoryInfo(path).FullName;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
