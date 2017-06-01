using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAnalyzer.Common;

namespace WpfAnalyzer.Commands
{
    public interface IBrowseCommand : ICommand
    {
        event EventHandler<EventArgs<string>> FolderSelected;
    }
}
