using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Meziantou.Framework.Globbing
{
    public class DirectoryInfo : FileSystemInfo, IDirectoryInfo
    {
        public DirectoryInfo(string path)
            : base(path)
        {
        }

        public IEnumerable<IFileSystemInfo> GetChildren()
        {
            return Directory.GetFileSystemEntries(FullPath).Select(Create);
        }

        public IDirectoryInfo GetDirectory(string name)
        {
            var fullPath = Path.Combine(FullPath, name);
            if (Directory.Exists(fullPath))
                return new DirectoryInfo(fullPath);

            return null;
        }

        public IFileInfo GetFile(string name)
        {
            var fullPath = Path.Combine(FullPath, name);
            if (File.Exists(fullPath))
                return new FileInfo(fullPath);

            return null;
        }
    }
}
