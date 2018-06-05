using System;
using System.Collections.Generic;
using System.Linq;

namespace Meziantou.Framework.Globbing
{
    internal class InMemoryDirectoryInfo : InMemoryFileSystemInfo, IDirectoryInfo
    {
        private readonly List<InMemoryFileSystemInfo> _children = new List<InMemoryFileSystemInfo>();

        public IReadOnlyList<InMemoryFileSystemInfo> Children => _children;

        public void AddChild(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            bool isDir = path.EndsWith("/");

            var segments = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var dir = this;
            for (int i = 0; i < segments.Length - 2; i++)
            {
                var segment = segments[i];
                var child = dir.Children.OfType<InMemoryDirectoryInfo>().FirstOrDefault(d => d.Name == segment);
                if (child == null)
                {
                    child = new InMemoryDirectoryInfo(dir, segment);
                    dir._children.Add(child);
                }

                dir = child;
            }

            var name = segments[segments.Length - 1];
            if (isDir)
            {
                dir._children.Add(new InMemoryDirectoryInfo(dir, name));
            }
            else
            {
                dir._children.Add(new InMemoryFileInfo(dir, name));
            }
        }

        public InMemoryDirectoryInfo()
            : base(null, null)
        {
        }

        public InMemoryDirectoryInfo(InMemoryFileSystemInfo parent, string name)
            : base(parent, name)
        {
        }

        public IEnumerable<IFileSystemInfo> GetChildren()
        {
            return Children;
        }

        public IDirectoryInfo GetDirectory(string name)
        {
            return GetChildren().OfType<InMemoryDirectoryInfo>().FirstOrDefault();
        }

        public IFileInfo GetFile(string name)
        {
            return GetChildren().OfType<InMemoryFileInfo>().FirstOrDefault();
        }
    }
}
