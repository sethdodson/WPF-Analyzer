using System.ComponentModel;
using WpfAnalyzer.Commands;

namespace WpfAnalyzer.ViewModels
{
    public interface IMainWindowViewModel
    {
        IBrowseCommand BrowseCommand { get; }
        IFileSystemViewModel TopDirectory { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}