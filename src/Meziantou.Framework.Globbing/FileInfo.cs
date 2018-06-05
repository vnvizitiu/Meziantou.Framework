namespace Meziantou.Framework.Globbing
{
    public class FileInfo : FileSystemInfo, IFileInfo
    {
        public FileInfo(string path)
            : base(path)
        {
        }
    }
}
