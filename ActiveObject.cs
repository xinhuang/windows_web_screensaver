using System;
using System.Threading;

namespace WebScreenSaver
{
    public class ActiveObject : IDisposable
    {
        private readonly BlockQueue<Action> _messageQueue = new BlockQueue<Action>();
        private bool _done;
        private readonly Thread _thread;

        public ActiveObject()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        private void Run()
        {
            while (!_done)
            {
                var message = _messageQueue.Dequeue();
                message.Invoke();
            }
        }

        public void Send(Action message)
        {
            _messageQueue.Enqueue(message);
        }

        private void Done()
        {
            _done = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Send(Done);
                _thread.Join();
            }
        }

        ~ActiveObject()
        {
            Dispose(false);
        }
    }
}
