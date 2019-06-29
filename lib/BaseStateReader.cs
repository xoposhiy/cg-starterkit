using System;
using System.Collections.Generic;
using System.Linq;

namespace bot
{
    public abstract class BaseStateReader<TState, TStateInit>
    {
        protected BaseStateReader(bool logToError = true)
        {
            this.logToError = logToError;
        }

        private readonly bool logToError;
        private Func<string> readLine;

        public TState Read(TStateInit init, string text)
        {
            var lines = text.Split('|');
            var i = 0;
            readLine = () => lines[i++];
            return Read(init);
        }

        public TState Read(string init, string text)
        {
            var stateInit = ReadInit(init);
            return Read(stateInit, text);
        }

        public TStateInit ReadInit(string text)
        {
            var lines = text.Split('|');
            var i = 0;
            readLine = () => lines[i++];
            return ReadInit();
        }

        public TStateInit ReadInitFromConsole()
        {
            var lines = new List<string>();
            readLine = () =>
            {
                var line = Console.ReadLine();
                lines.Add(line);
                return line;
            };
            var init = ReadInit();
            if (logToError)
                Console.Error.WriteLine(string.Join("|", lines));
            return init;
        }

        public TState ReadFromConsole(TStateInit init)
        {
            var lines = new List<string>();
            readLine = () =>
            {
                var line = Console.ReadLine();
                lines.Add(line);
                return line;
            };
            var state = Read(init);
            if (logToError)
                Console.Error.WriteLine(string.Join("|", lines));
            return state;
        }

        protected abstract TState Read(TStateInit init);
        protected abstract TStateInit ReadInit();

        protected string ReadLine() => readLine();
        protected int ReadInt() => int.Parse(readLine());

        protected int[] ReadInts() => readLine().Split().Select(int.Parse).ToArray();

        protected Vec ReadVec() => Vec.Parse(readLine());
    }
}