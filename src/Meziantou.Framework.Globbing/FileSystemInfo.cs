using System;
using System.IO;

namespace Meziantou.Framework.Globbing
{
    public class FileSystemInfo : IFileSystemInfo
    {
        public FileSystemInfo(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            FullPath = Path.GetFullPath(path);
            Name = Path.GetFileName(path);
        }

        public string Name { get; }
        public string FullPath { get; }

        public static FileSystemInfo Create(string path)
        {
            if (Directory.Exists(path))
                return new DirectoryInfo(path);

            return new FileInfo(path);
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
