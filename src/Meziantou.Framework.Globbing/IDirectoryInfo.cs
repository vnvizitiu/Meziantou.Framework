using System.Collections.Generic;

namespace Meziantou.Framework.Globbing
{
    public interface IDirectoryInfo : IFileSystemInfo
    {
        IEnumerable<IFileSystemInfo> GetChildren();
        IDirectoryInfo GetDirectory(string name);
        IFileInfo GetFile(string name);
    }
}
