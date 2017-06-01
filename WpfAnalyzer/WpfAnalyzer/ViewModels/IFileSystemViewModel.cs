using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAnalyzer.ViewModels
{
    public interface IFileSystemViewModel
    {
        List<IFileSystemViewModel> Children { get; }        
        bool IsCodeFile { get; }
        int LineCount { get; set; }
    }
}
