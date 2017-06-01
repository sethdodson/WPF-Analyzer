using WpfAnalyzer.ViewModels;

namespace WpfAnalyzer.Services
{
    public interface IAnalyzerService
    {
        IFileSystemViewModel Analyze(string path);
    }
}