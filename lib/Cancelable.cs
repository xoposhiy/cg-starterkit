using System;

namespace bot
{
    public class Cancelable : IDisposable
    {
        private readonly Action cancel;

        public Cancelable(Action cancel) { this.cancel = cancel; }

        public void Dispose() { cancel(); }
    }
}