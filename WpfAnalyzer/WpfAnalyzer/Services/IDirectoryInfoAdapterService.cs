using Thinktecture.IO;

namespace WpfAnalyzer.Services
{
    public interface IDirectoryInfoAdapterService
    {
        IDirectoryInfo CreateDirectoryInfo(string path);
    }
}