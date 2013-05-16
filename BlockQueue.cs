using System.Threading;

namespace WebScreenSaver
{
    internal class BlockQueue<T>
    {
        private readonly System.Collections.Concurrent.ConcurrentQueue<T> _queue =
            new System.Collections.Concurrent.ConcurrentQueue<T>();

        public void Enqueue(T value)
        {
            _queue.Enqueue(value);
        }

        public T Dequeue()
        {
            T result;
            while (!_queue.TryDequeue(out result))
            {
                Thread.Sleep(100);
            }
            return result;
        }
    }
}