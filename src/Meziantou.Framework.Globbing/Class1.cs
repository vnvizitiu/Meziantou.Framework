using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Meziantou.Framework.Globbing.Tests")]

namespace Meziantou.Framework.Globbing
{
    public class Matcher
    {
        private readonly StringComparison _stringComparison;
        private readonly List<Pattern> _patterns = new List<Pattern>();

        public Matcher(StringComparison stringComparison)
        {
            _stringComparison = stringComparison;
        }

        public Matcher AddPattern(string pattern)
        {
            _patterns.Add(Pattern.Parse(pattern));
            return this;
        }

        public IEnumerable<IFileSystemInfo> Execute(IDirectoryInfo folder)
        {
            throw new NotImplementedException();
        }
    }

    internal class Pattern
    {
        private IList<ISegment> Segments { get; }

        public static Pattern Parse(string pattern)
        {
            /* 
             * Any character ?
             * Wildcard *
             * Recursive wildcard **
             * Range [a-z]
             * Not in range [!a-z]
             * Current directory .
             * Parent directory ..
             * Directory Separator /
             * Negate !
             * Match folders by ending with /
             */




            throw new NotImplementedException();
        }
    }

    internal class PatternParser
    {
        public Pattern Build(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            if (pattern.Length == 0)
                throw new PatternParserException("Pattern is empty");

            int index = 0;
            while (index < pattern.Length)
            {
                var c = pattern[c];
                if(c == '/')
                {

                }
                

                index++;
            }

            throw new NotImplementedException();
        }
    }

    internal readonly struct MatchResult
    {
        bool Include { get; }
        bool Recurse { get; }
        bool IsFInished { get; }

        public MatchResult(bool include, bool recurse, bool isFInished) : this()
        {
            Include = include;
            Recurse = recurse;
            IsFInished = isFInished;
        }
    }

    internal interface ISegment
    {
        MatchResult Match(string value);
    }

    internal class LiteralSegment : ISegment
    {
        public LiteralSegment(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public MatchResult Match(string value)
        {
            return new MatchResult(
                include: value == Value,
                recurse: false,
                isFInished: true);
        }
    }

    internal class RecurseDirectorySegment : ISegment
    {
        public RecurseDirectorySegment(Pattern subPattern)
        {
            SubPattern = subPattern;
        }

        public Pattern SubPattern { get; }

        public MatchResult Match(string value)
        {
            return new MatchResult(
                include: false,
                recurse: true,
                isFInished: false);
        }
    }

    public class PatternParserException : Exception
    {
        public PatternParserException() : base()
        {
        }

        protected PatternParserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public PatternParserException(string message) : base(message)
        {
        }

        public PatternParserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
