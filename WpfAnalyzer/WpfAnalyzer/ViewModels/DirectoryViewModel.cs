using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAnalyzer.ViewModels
{
    public class DirectoryViewModel : IFileSystemViewModel
    {
        private DirectoryInfo _directoryInfo;

        public List<IFileSystemViewModel> Children { get; } = new List<IFileSystemViewModel>();
        public int LineCount { get; set; }
        public int NumberOfFilesWithCode { get; set; }
        public bool IsCodeFile => false;
        public string Name => _directoryInfo.Name;
        public int NumberOfFilesProcessed { get; set; }

        public DirectoryViewModel(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }
    }
}
