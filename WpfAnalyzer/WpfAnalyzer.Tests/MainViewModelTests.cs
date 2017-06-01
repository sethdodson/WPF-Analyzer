using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WpfAnalyzer.Commands;
using WpfAnalyzer.Services;
using WpfAnalyzer.ViewModels;
using WpfAnalyzer.Common;

namespace WpfAnalyzer.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void BrowseCommand_Is_Expected_After_Constructed()
        {
            //Arrange
            var browseCommand = new Mock<IBrowseCommand>();
            var analyzerService = new Mock<IAnalyzerService>();
            var viewModel = new MainWindowViewModel(browseCommand.Object, analyzerService.Object);
            var expected = browseCommand.Object;

            //Act
            var actual = viewModel.BrowseCommand;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TopDirectory_Is_Null_After_Constructed()
        {
            //Arrange
            var browseCommand = new Mock<IBrowseCommand>();
            var analyzerService = new Mock<IAnalyzerService>();
            var viewModel = new MainWindowViewModel(browseCommand.Object, analyzerService.Object);

            //Act
            var actual = viewModel.TopDirectory;

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TopDirectory_Is_Expected_After_BrowseCommand_Raises_FolderSelected()
        {
            //Arrange
            var browseCommand = new Mock<IBrowseCommand>();
            var analyzerService = new Mock<IAnalyzerService>();
            var directoryViewModel = new Mock<IFileSystemViewModel>();
            analyzerService
                .Setup(a => a.Analyze(It.IsAny<string>()))
                .Returns(directoryViewModel.Object);
            var viewModel = new MainWindowViewModel(browseCommand.Object, analyzerService.Object);
            var expected = directoryViewModel.Object;

            //Act
            browseCommand.Raise(b => b.FolderSelected += null, new EventArgs<string>("test"));
            var actual = viewModel.TopDirectory;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TopDirectory_Is_Loaded_With_Expected_Path_Passed_To_AnalyzerService_After_BrowseCommand_Raises_FolderSelected()
        {
            //Arrange
            var browseCommand = new Mock<IBrowseCommand>();
            var analyzerService = new Mock<IAnalyzerService>();
            var directoryViewModel = new Mock<IFileSystemViewModel>();
            analyzerService
                .Setup(a => a.Analyze(It.IsAny<string>()))
                .Returns(directoryViewModel.Object);
            var viewModel = new MainWindowViewModel(browseCommand.Object, analyzerService.Object);
            var expected = "test";

            //Act
            browseCommand.Raise(b => b.FolderSelected += null, new EventArgs<string>("test"));            

            //Assert
            analyzerService.Verify(a => a.Analyze(It.Is<string>(actual => actual == expected)));
        }

        [TestMethod]
        public void TopDirectory_Raises_PropertyChanged_When_Set()
        {
            //Assert
            var browseCommand = new Mock<IBrowseCommand>();
            var analyzerService = new Mock<IAnalyzerService>();
            var topDirectory = new Mock<IFileSystemViewModel>();
            var raised = false;
            var viewModel = new MainWindowViewModel(browseCommand.Object, analyzerService.Object);
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "TopDirectory")
                    raised = true;
            };

            //Act
            viewModel.TopDirectory = topDirectory.Object;

            //Assert
            Assert.IsTrue(raised);
        }
    }
}
