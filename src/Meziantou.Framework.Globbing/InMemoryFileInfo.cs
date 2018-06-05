namespace Meziantou.Framework.Globbing
{
    internal class InMemoryFileInfo : InMemoryFileSystemInfo, IFileInfo
    {
        public InMemoryFileInfo(InMemoryFileSystemInfo parent, string name)
            : base(parent, name)
        {
        }
    }
}
