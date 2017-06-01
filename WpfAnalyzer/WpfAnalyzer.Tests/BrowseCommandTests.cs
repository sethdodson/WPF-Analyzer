using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAnalyzer.Commands;
using WpfAnalyzer.Services;

namespace WpfAnalyzer.Tests
{
    [TestClass]
    public class BrowseCommandTests
    {
        [TestMethod]
        public void CanExecute_Returns_True()
        {
            //Arrange
            var dialogService = new Mock<IDialogService>();
            var browseCommand = new BrowseCommand(dialogService.Object);

            //Act
            var actual = browseCommand.CanExecute(null);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Execute_Raises_FolderSelected_With_Expected_Path_When_BrowseToFolder_Returns_A_String()
        {            
            //Arrange
            var dialogService = new Mock<IDialogService>();
            dialogService
                .Setup(ds => ds.BrowseToFolder())
                .Returns("something");
            var selectedFolder = string.Empty;
            var browseCommand = new BrowseCommand(dialogService.Object);
            browseCommand.FolderSelected += (sender, e) =>
            {
                selectedFolder = e.Data;
            };
            var expected = "something";

            //Act
            browseCommand.Execute(null);

            //Assert
            Assert.AreEqual(expected, selectedFolder);
        }

        [TestMethod]
        public void Execute_Does_Not_Raise_FolderSelected_When_BrowseToFolder_Returns_Empty_String()
        {
            //Arrange
            var dialogService = new Mock<IDialogService>();
            dialogService
                .Setup(ds => ds.BrowseToFolder())
                .Returns(string.Empty);
            var raised = false;
            var browseCommand = new BrowseCommand(dialogService.Object);
            browseCommand.FolderSelected += (sender, e) =>
            {
                raised = true;
            };            

            //Act
            browseCommand.Execute(null);

            //Assert
            Assert.IsFalse(raised);
        }
    }
}
