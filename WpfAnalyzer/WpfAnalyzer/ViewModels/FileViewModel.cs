using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAnalyzer.ViewModels
{
    public class FileViewModel : IFileSystemViewModel
    {
        private FileInfo fileInfo;

        public FileViewModel(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        public List<IFileSystemViewModel> Children { get; } = new List<IFileSystemViewModel>();

        public bool IsCodeFile { get; set; }
        public int LineCount { get; set; }
        public string Name => fileInfo.Name;
    }
}
