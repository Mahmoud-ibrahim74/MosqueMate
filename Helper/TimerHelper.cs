using System;
using System.Windows.Threading;

namespace MosqueMate.Helper
{
    public class TimerHelper : IDisposable
    {
        private DispatcherTimer timer;
        private readonly Action SendAction;
        public TimerHelper(int seconds,Action action)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(seconds);
            SendAction = action;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SendAction();
        }

        public void StartTimer()
        {
            timer.Start();
        }
        public void StopTimer()
        {
            timer.Stop();
        }

        public void Dispose()
        {
            timer = null;
        }
    }
}
