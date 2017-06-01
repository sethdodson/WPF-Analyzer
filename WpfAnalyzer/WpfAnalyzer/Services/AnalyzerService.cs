using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAnalyzer.ViewModels;

namespace WpfAnalyzer.Services
{
    public class AnalyzerService : IAnalyzerService
    {
        public IFileSystemViewModel Analyze(string path)
        {
            return CreateFileSystemViewModel(new DirectoryInfo(path));
        }

        private IFileSystemViewModel CreateFileSystemViewModel(DirectoryInfo directoryInfo)
        {
            var directoryViewModel = new DirectoryViewModel(directoryInfo);
            AddChildDirectories(directoryInfo, directoryViewModel);
            AddChildFiles(directoryInfo, directoryViewModel);
            directoryViewModel.LineCount = GetLineCountForDirectory(directoryViewModel);
            directoryViewModel.NumberOfFilesWithCode = GetNumberOfFilesWithCode(directoryViewModel);
            directoryViewModel.NumberOfFilesProcessed = GetNumberOfFilesProcessed(directoryViewModel);
            return directoryViewModel;
        }

        private void AddChildDirectories(DirectoryInfo directoryInfo, DirectoryViewModel directoryViewModel)
        {
            foreach (var directory in directoryInfo.GetDirectories())
                directoryViewModel.Children.Add(CreateFileSystemViewModel(directory));
        }

        private void AddChildFiles(DirectoryInfo directoryInfo, DirectoryViewModel directoryViewModel)
        {
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                var fileViewModel = new FileViewModel(fileInfo);
                fileViewModel.IsCodeFile = IsCodeFile(fileInfo);
                fileViewModel.LineCount = fileViewModel.IsCodeFile ? GetLineCountForFile(fileInfo) : 0;
                directoryViewModel.Children.Add(fileViewModel);
            }
        }

        private static int GetLineCountForDirectory(DirectoryViewModel directoryViewModel)
        {
            return directoryViewModel
                .Children
                .Select(c => c.LineCount)
                .Sum();
        }

        private static int GetNumberOfFilesWithCode(DirectoryViewModel directoryViewModel)
        {
            var codeFileCount = (from child in directoryViewModel.Children
                                 where child.IsCodeFile
                                 select child).Count();
            var numberOfFilesWithCodeFromDirectories = (from child in directoryViewModel.Children.OfType<DirectoryViewModel>()
                                                        select child.NumberOfFilesWithCode).Sum();
            return codeFileCount + numberOfFilesWithCodeFromDirectories;
        }

        private static int GetNumberOfFilesProcessed(DirectoryViewModel fileSystemViewModel)
        {
            var fileCount = (from child in fileSystemViewModel.Children.OfType<FileViewModel>()
                             select child).Count();
            var numberOfFilesProcessedFromDirectories = (from child in fileSystemViewModel.Children.OfType<DirectoryViewModel>()                                                         
                                                         select child.NumberOfFilesProcessed).Sum();
            return fileCount + numberOfFilesProcessedFromDirectories;
        }

        private int GetLineCountForFile(FileInfo fileInfo)
        {
            return File
                .ReadLines(fileInfo.FullName)
                .Count();
        }

        private static bool IsCodeFile(FileInfo fileInfo)
        {
            switch (fileInfo.Extension)
            {
                case ".cs": return true;
                case ".css": return true;
                case ".js": return true;
                case ".aspx": return true;
                case ".html": return true;
                case ".xsl": return true;
                case ".ascx": return true;
                case ".htm": return true;
                case ".xslt": return true;
                default: return false;
            }
        }
    }
}
