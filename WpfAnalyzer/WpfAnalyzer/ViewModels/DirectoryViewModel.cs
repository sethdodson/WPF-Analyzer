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
        private DirectoryInfo directoryInfo;

        public DirectoryViewModel(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public List<IFileSystemViewModel> Children { get; } = new List<IFileSystemViewModel>();

        public int LineCount { get; set; }
        public int NumberOfFilesWithCode { get; set; }
        public bool IsCodeFile => false;
        public string Name => directoryInfo.Name;
        public int NumberOfFilesProcessed { get; set; }
    }
}
