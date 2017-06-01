using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Thinktecture.IO;
using WpfAnalyzer.Services;
using WpfAnalyzer.ViewModels;

namespace WpfAnalyzer.Tests
{
    [TestClass]
    public class AnalyzerServiceTests
    {
        [TestMethod]
        public void Analyze_Returns_An_IFileSystemViewModel_Given_A_Path()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var path = "Path";
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var expectedType = typeof(IFileSystemViewModel);

            //Act
            var actual = analyzerService.Analyze(path);

            //Assert
            Assert.IsInstanceOfType(actual, expectedType);
        }

        [TestMethod]
        public void Analyze_Returns_DirectoryViewModel_With_Expected_Name()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            directoryInfo
                .Setup(d => d.Name)
                .Returns("ExpectedName");
            var path = "Path";
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var expected = "ExpectedName";

            //Act
            string actual = ((DirectoryViewModel)analyzerService.Analyze(path)).Name;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Returns_FileSystemViewModel_With_Expected_Number_Of_Child_Directories()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childDirectory1 = new Mock<IDirectoryInfo>();
            var childDirectory2 = new Mock<IDirectoryInfo>();
            directoryInfo
                .Setup(d => d.GetDirectories())
                .Returns(new[] { childDirectory1.Object, childDirectory2.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 2;

            //Act
            var actual = analyzerService.Analyze(path).Children.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Returns_FileSystemViewModel_With_Expected_Number_Of_Files()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childFile1 = new Mock<IFileInfo>();
            var childFile2 = new Mock<IFileInfo>();
            directoryInfo
                .Setup(d => d.GetFiles())
                .Returns(new[] { childFile1.Object, childFile2.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 2;

            //Act
            var actual = analyzerService.Analyze(path).Children.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Returns_FileSystemViewModel_With_Children_With_Expected_Names()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childDirectory1 = new Mock<IDirectoryInfo>();
            childDirectory1
                .Setup(cd => cd.Name)
                .Returns("Child Directory");
            var childFile1 = new Mock<IFileInfo>();
            childFile1
                .Setup(cf => cf.Name)
                .Returns("Child File");
            directoryInfo
                .Setup(d => d.GetDirectories())
                .Returns(new[] { childDirectory1.Object });
            directoryInfo
                .Setup(d => d.GetFiles())
                .Returns(new[] { childFile1.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = new[] { "Child Directory", "Child File" };

            //Act
            var actual = analyzerService
                .Analyze(path)
                .Children
                .Select(c => c.Name)
                .ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Returns_FileSystemViewModel_With_Nested_Children()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childDirectory = new Mock<IDirectoryInfo>();
            var childOfChildDirectory = new Mock<IDirectoryInfo>();
            var childFileOfChildDirectory1 = new Mock<IFileInfo>();
            var childFileOfChildDirectory2 = new Mock<IFileInfo>();
            childDirectory
                .Setup(cd => cd.GetDirectories())
                .Returns(new[] { childOfChildDirectory.Object });
            childDirectory
                .Setup(cd => cd.GetFiles())
                .Returns(new[] { childFileOfChildDirectory1.Object, childFileOfChildDirectory2.Object });
            directoryInfo
                .Setup(d => d.GetDirectories())
                .Returns(new[] { childDirectory.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 3;

            //Act
            var actual = analyzerService
                .Analyze(path)
                .Children[0]
                .Children
                .Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Reads_Lines_From_Expected_Path_When_Getting_LineCount()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            file
                .Setup(f => f.ReadLines(It.IsAny<string>()))
                .Returns(new[] { "line1", "line2", "line3" });
            var childFile = new Mock<IFileInfo>();
            childFile
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            childFile
                .Setup(f => f.FullName)
                .Returns("Path");
            directoryInfo
                .Setup(di => di.GetFiles())
                .Returns(new[] { childFile.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = "Path";

            //Act
            analyzerService.Analyze(path);

            //Assert
            file.Verify(f => f.ReadLines(It.Is<string>(actual => actual == expected)));
        }

        [TestMethod]
        public void Analyze_Sets_LineCount_To_Zero_When_File_Is_Not_Code_File()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            file
                .Setup(f => f.ReadLines(It.IsAny<string>()))
                .Returns(new[] { "line1", "line2", "line3" });
            var childFile = new Mock<IFileInfo>();
            childFile
                .Setup(cf => cf.Extension)
                .Returns(".txt");
            directoryInfo
                .Setup(di => di.GetFiles())
                .Returns(new[] { childFile.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 0;

            //Act
            var actual = analyzerService.Analyze(path).Children[0].LineCount;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Sets_Line_Count_Of_Directories_To_Expected()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            file
                .Setup(f => f.ReadLines(It.IsAny<string>()))
                .Returns(new[] { "line1", "line2", "line3" });
            var childFile = new Mock<IFileInfo>();
            childFile
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            directoryInfo
                .Setup(di => di.GetFiles())
                .Returns(new[] { childFile.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 3;

            //Act
            var actual = analyzerService.Analyze(path).LineCount;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Sets_Expected_NumberOfFilesWithCode_On_Directory_Given_Nested_Directory_And_Files()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childDirectory = new Mock<IDirectoryInfo>();
            var childOfChildDirectory = new Mock<IDirectoryInfo>();
            var childFileOfChildDirectory1 = new Mock<IFileInfo>();
            childFileOfChildDirectory1
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            var childFileOfChildDirectory2 = new Mock<IFileInfo>();
            childFileOfChildDirectory2
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            var childFile1 = new Mock<IFileInfo>();
            childFile1
                .Setup(cf => cf.Extension)
                .Returns(".txt");
            childDirectory
                .Setup(cd => cd.GetDirectories())
                .Returns(new[] { childOfChildDirectory.Object });
            childDirectory
                .Setup(cd => cd.GetFiles())
                .Returns(new[] { childFileOfChildDirectory1.Object, childFileOfChildDirectory2.Object });
            directoryInfo
                .Setup(d => d.GetDirectories())
                .Returns(new[] { childDirectory.Object });
            directoryInfo
                .Setup(d => d.GetFiles())
                .Returns(new[] { childFile1.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 2;

            //Act
            var actual = ((DirectoryViewModel)analyzerService.Analyze(path)).NumberOfFilesWithCode;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Analyze_Returns_FileSystemViewModel_With_LineCount_For_Files_With_Certain_Extensions()
        {

            //unfortunately MSTest does not support theories
            var extensions = new[] { ".cs", ".css", ".js", ".aspx", ".html", ".htm", ".xsl", ".ascx", ".xslt" };

            foreach (var extension in extensions)
            {
                //Arrange
                var directoryInfo = new Mock<IDirectoryInfo>();
                var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
                directoryInfoService
                    .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                    .Returns(directoryInfo.Object);
                var file = new Mock<IFile>();
                file
                    .Setup(f => f.ReadLines(It.IsAny<string>()))
                    .Returns(new[] { "line1", "line2", "line3" });
                var childFile = new Mock<IFileInfo>();
                childFile
                    .Setup(cf => cf.Extension)
                    .Returns(extension);
                directoryInfo
                    .Setup(di => di.GetFiles())
                    .Returns(new[] { childFile.Object });
                var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
                var path = "Path";
                var expected = 3;

                //Act
                var actual = analyzerService.Analyze(path).Children[0].LineCount;

                //Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Analyze_Sets_Expected_NumberOfFilesProcessed_On_Directory_Given_Nested_Directory_And_Files()
        {
            //Arrange
            var directoryInfo = new Mock<IDirectoryInfo>();
            var directoryInfoService = new Mock<IDirectoryInfoAdapterService>();
            directoryInfoService
                .Setup(dis => dis.CreateDirectoryInfo(It.IsAny<string>()))
                .Returns(directoryInfo.Object);
            var file = new Mock<IFile>();
            var childDirectory = new Mock<IDirectoryInfo>();
            var childOfChildDirectory = new Mock<IDirectoryInfo>();
            var childFileOfChildDirectory1 = new Mock<IFileInfo>();
            childFileOfChildDirectory1
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            var childFileOfChildDirectory2 = new Mock<IFileInfo>();
            childFileOfChildDirectory2
                .Setup(cf => cf.Extension)
                .Returns(".cs");
            var childFile1 = new Mock<IFileInfo>();
            childFile1
                .Setup(cf => cf.Extension)
                .Returns(".txt");
            childDirectory
                .Setup(cd => cd.GetDirectories())
                .Returns(new[] { childOfChildDirectory.Object });
            childDirectory
                .Setup(cd => cd.GetFiles())
                .Returns(new[] { childFileOfChildDirectory1.Object, childFileOfChildDirectory2.Object });
            directoryInfo
                .Setup(d => d.GetDirectories())
                .Returns(new[] { childDirectory.Object });
            directoryInfo
                .Setup(d => d.GetFiles())
                .Returns(new[] { childFile1.Object });
            var analyzerService = new AnalyzerService(file.Object, directoryInfoService.Object);
            var path = "Path";
            var expected = 3;

            //Act
            var actual = ((DirectoryViewModel)analyzerService.Analyze(path)).NumberOfFilesProcessed;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
