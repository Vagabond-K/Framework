using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading
{
    /// <summary>
    /// 리소스 해제 가능한 Mutex
    /// </summary>
    public class DisposableMutex : WaitHandle
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="initiallyOwned">true to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, false.</param>
        /// <param name="name">The name of the System.Threading.Mutex. If the value is null, the System.Threading.Mutex is unnamed.</param>
        /// <param name="createdNew">When this method returns, contains a Boolean that is true if a local mutex was created (that is, if name is null or an empty string) or if the specified named system mutex was created; false if the specified named system mutex already existed. This parameter is passed uninitialized.</param>
        public DisposableMutex(bool initiallyOwned, string name, out bool createdNew)
        {
            innerMutex = new Mutex(initiallyOwned, name, out createdNew);
        }

        private readonly Mutex innerMutex;

        /// <summary>
        /// When overridden in a derived class, releases the unmanaged resources used by the System.Threading.WaitHandle, and optionally releases the managed resources.
        /// </summary>
        /// <param name="explicitDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool explicitDisposing)
        {
            base.Dispose(explicitDisposing);
            try
            {
                innerMutex.ReleaseMutex();
            }
            catch { }

            try
            {
                innerMutex.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Blocks the current thread until the current System.Threading.WaitHandle receives a signal.
        /// </summary>
        /// <returns>true if the current instance receives a signal. If the current instance is never signaled, System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean) never returns.</returns>
        public override bool WaitOne()
        {
            return innerMutex.WaitOne();
        }

        /// <summary>
        /// Blocks the current thread until the current System.Threading.WaitHandle receives a signal, using a 32-bit signed integer to specify the time interval in milliseconds.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or System.Threading.Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public override bool WaitOne(int millisecondsTimeout)
        {
            return innerMutex.WaitOne(millisecondsTimeout);
        }

        /// <summary>
        /// Blocks the current thread until the current System.Threading.WaitHandle receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or System.Threading.Timeout.Infinite (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public override bool WaitOne(int millisecondsTimeout, bool exitContext)
        {
            return innerMutex.WaitOne(millisecondsTimeout, exitContext);
        }

        /// <summary>
        /// Blocks the current thread until the current instance receives a signal, using a System.TimeSpan to specify the time interval.
        /// </summary>
        /// <param name="timeout">A System.TimeSpan that represents the number of milliseconds to wait, or a System.TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public override bool WaitOne(TimeSpan timeout)
        {
            return innerMutex.WaitOne(timeout);
        }

        /// <summary>
        /// Blocks the current thread until the current instance receives a signal, using a System.TimeSpan to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="timeout">A System.TimeSpan that represents the number of milliseconds to wait, or a System.TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public override bool WaitOne(TimeSpan timeout, bool exitContext)
        {
            return innerMutex.WaitOne(timeout, exitContext);
        }

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>An object of type System.Runtime.Remoting.Lifetime.ILease used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime property.</returns>
        public override object InitializeLifetimeService()
        {
            return innerMutex.InitializeLifetimeService();
        }

        /// <summary>
        /// Releases all resources held by the current System.Threading.WaitHandle.
        /// </summary>
        public override void Close()
        {
            innerMutex.Close();
        }
    }
}
