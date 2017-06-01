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
        private FileInfo _fileInfo;

        public List<IFileSystemViewModel> Children { get; } = new List<IFileSystemViewModel>();
        public bool IsCodeFile { get; set; }
        public int LineCount { get; set; }
        public string Name => _fileInfo.Name;

        public FileViewModel(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }


    }
}
