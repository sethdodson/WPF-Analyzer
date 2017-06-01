using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfAnalyzer.Services
{
    public class DialogService : IDialogService
    {
        public string BrowseToFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    return dialog.SelectedPath;
                else return string.Empty;
            }
        }
    }
}
