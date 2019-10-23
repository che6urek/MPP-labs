using System.Threading;

namespace lab3
{
    public class Mutex
    {
        private int _threadId;
        public void Lock()
        {
            int currentId = Thread.CurrentThread.ManagedThreadId;
            while (Interlocked.CompareExchange(ref _threadId, currentId, 0) != currentId)
            {
                Thread.Yield();
            }
        }
        public void Unlock()
        {
            Interlocked.CompareExchange(ref _threadId, 0, Thread.CurrentThread.ManagedThreadId);
        }
    }
}