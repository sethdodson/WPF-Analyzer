using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IO;
using Thinktecture.IO.Adapters;

namespace WpfAnalyzer.Services
{
    public class DirectoryInfoAdapterService : IDirectoryInfoAdapterService
    {
        public IDirectoryInfo CreateDirectoryInfo(string path)
        {
            return new DirectoryInfoAdapter(new DirectoryInfo(path));
        }
    }
}
