using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAnalyzer.Common;
using WpfAnalyzer.Services;

namespace WpfAnalyzer.Commands
{
    public class BrowseCommand : IBrowseCommand
    {
        private readonly IDialogService _dialogService;

        public event EventHandler<EventArgs<string>> FolderSelected;

        public BrowseCommand(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var folderPath = _dialogService.BrowseToFolder();
            if (string.IsNullOrWhiteSpace(folderPath)) return;
            FolderSelected?.Invoke(this, new EventArgs<string>(folderPath));
        }
    }
}
