using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfAnalyzer.Commands;
using WpfAnalyzer.Services;

namespace WpfAnalyzer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAnalyzerService _analyzerService;
        private IFileSystemViewModel _topDirectory;

        public IBrowseCommand BrowseCommand { get; private set; }

        public IFileSystemViewModel TopDirectory
        {
            get { return _topDirectory; }
            set
            {
                _topDirectory = value;
                RaisePropertyChanged("TopDirectory");
            }
        }

        public MainWindowViewModel(IBrowseCommand browseCommand, IAnalyzerService analyzerService)
        {            
            BrowseCommand = browseCommand;
            browseCommand.FolderSelected += OnFolderSelected;
            _analyzerService = analyzerService;
        }

        private void OnFolderSelected(object sender, Common.EventArgs<string> e)
        {
            TopDirectory = _analyzerService.Analyze(e.Data);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
