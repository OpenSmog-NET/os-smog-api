using System;

namespace OS.Docker.TestKit
{
    public abstract class DisposableFixture : IDisposable
    {
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            Dispose(true);
            GC.SuppressFinalize(this);

            disposed = true;
        }

        ~DisposableFixture()
        {
            Dispose(false);
        }
    }
}