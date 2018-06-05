using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meziantou.Framework.Globbing.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AllFilesRecursive()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("dir1/file.txt");
            dir.AddChild("dir1/dir2/file.txt");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("**/*")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "dir1/file.txt",
                "dir1/dir2/file.txt"
            }, result);
        }

        [TestMethod]
        public void FileNameEndsWith()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("ConsoleHost.sln");
            dir.AddChild("ContosoWebsite.sln");
            dir.AddChild("FabrikamWebsite.sln");
            dir.AddChild("Website.sln");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("*Website.sln")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "ContosoWebsite.sln",
                "FabrikamWebsite.sln",
                "Website.sln",
            }, result);
        }
        
        [TestMethod]
        public void DirectoryEndsWithAndFileNameEndsWith()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("ContosoWebsite/index.html");
            dir.AddChild("ContosoWebsite/ContosoWebsite.proj");
            dir.AddChild("FabrikamWebsite/index.html");
            dir.AddChild("FabrikamWebsite/FabrikamWebsite.proj");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("*Website/*.proj")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "ContosoWebsite/ContosoWebsite.proj",
                "FabrikamWebsite/FabrikamWebsite.proj",
            }, result);
        }

        [TestMethod]
        public void AnyChar()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("log1.log");
            dir.AddChild("log2.log");
            dir.AddChild("log3.log");
            dir.AddChild("script.sh");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("log?.log")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "log1.log",
                "log2.log",
                "log3.log",
            }, result);
        }

        [TestMethod]
        public void MultipleAnyChar()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("image.tiff");
            dir.AddChild("image.png");
            dir.AddChild("image.ico");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("image.???")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "image.png",
                "image.ico",
            }, result);
        }

        [TestMethod]
        public void CharacterSet()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("SampleA.dat");
            dir.AddChild("SampleB.dat");
            dir.AddChild("SampleC.dat");
            dir.AddChild("SampleD.dat");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("Sample[AC].dat")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "SampleA.dat",
                "SampleC.dat",
            }, result);
        }

        [TestMethod]
        public void CharacterRange()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("SampleA.dat");
            dir.AddChild("SampleB.dat");
            dir.AddChild("SampleC.dat");
            dir.AddChild("SampleD.dat");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("Sample[A-C].dat")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "SampleA.dat",
                "SampleB.dat",
                "SampleC.dat",
            }, result);
        }

        [TestMethod]
        public void CharacterRangeAndSet()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("SampleA.dat");
            dir.AddChild("SampleB.dat");
            dir.AddChild("SampleC.dat");
            dir.AddChild("SampleD.dat");
            dir.AddChild("SampleE.dat");
            dir.AddChild("SampleF.dat");
            dir.AddChild("SampleG.dat");
            dir.AddChild("SampleH.dat");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("Sample[A-CEG].dat")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "SampleA.dat",
                "SampleB.dat",
                "SampleC.dat",
                "SampleE.dat",
                "SampleG.dat",
            }, result);
        }

        [TestMethod]
        public void AllExceptXmlFiles()
        {
            var dir = new InMemoryDirectoryInfo();
            dir.AddChild("ConsoleHost.exe");
            dir.AddChild("ConsoleHost.pdb");
            dir.AddChild("ConsoleHost.xml");
            dir.AddChild("Fabrikam.dll");
            dir.AddChild("Fabrikam.pdb");
            dir.AddChild("Fabrikam.xml");

            var result = new Matcher(System.StringComparison.OrdinalIgnoreCase)
                .AddPattern("*")
                .AddPattern("!*.xml")
                .Execute(dir)
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "ConsoleHost.exe",
                "ConsoleHost.pdb",
                "Fabrikam.dll",
                "Fabrikam.pdb",
            }, result);
        }
    }
}
