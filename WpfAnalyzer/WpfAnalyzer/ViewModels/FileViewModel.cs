using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IO;

namespace WpfAnalyzer.ViewModels
{
    public class FileViewModel : IFileSystemViewModel
    {
        private IFileInfo _fileInfo;

        public List<IFileSystemViewModel> Children { get; } = new List<IFileSystemViewModel>();
        public bool IsCodeFile { get; set; }
        public int LineCount { get; set; }
        public string Name => _fileInfo.Name;

        public FileViewModel(IFileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }


    }
}
