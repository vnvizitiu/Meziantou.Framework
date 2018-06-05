namespace Meziantou.Framework.Globbing
{
    internal class InMemoryFileSystemInfo : IFileSystemInfo
    {
        public string Name { get; }
        public InMemoryFileSystemInfo Parent { get; }

        public string FullPath
        {
            get
            {
                return Parent?.FullPath + "/" + Name;
            }
        }

        public InMemoryFileSystemInfo(InMemoryFileSystemInfo parent, string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
