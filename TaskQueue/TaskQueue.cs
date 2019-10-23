using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TaskQueueLib
{
    public delegate void TaskDelegate();

    public class TaskQueue
    {
        public List<Thread> Threads;
        private readonly BlockingCollection<TaskDelegate> _tasks;

        public TaskQueue(int threadsCount)
        {
            Threads = new List<Thread>();
            _tasks = new BlockingCollection<TaskDelegate>();
            for (var i = 0; i < threadsCount; i++)
            {
                var thread = new Thread(DoWork) { Name = (i + 1).ToString() };
                thread.Start();
                Threads.Add(thread);
            }
        }

        private void DoWork()
        {
            while (!_tasks.IsCompleted)
            {
                try
                {
                    _tasks.Take()();
                }
                catch
                {
                    break;
                }
            }
        }

        public void WaitAndStop()
        {
            _tasks.CompleteAdding();
            foreach (var thread in Threads)
            {
                thread.Join();
            }
            _tasks.Dispose();
        }

        public void WaitAll(TaskDelegate[] delegates)
        {
            foreach (var task in delegates)
            {
                _tasks.Add(task);
            }
            WaitAndStop();
        }

        public void EnqueueTask(TaskDelegate task)
        {
            _tasks.Add(task);
        }
    }
}