using System;
using System.ComponentModel;
namespace MosqueMateServices.Helper
{
    public class BackgroundWorkerHelper : IDisposable
    {
        BackgroundWorker worker;
        public string WorkerError { get; set; }
        public BackgroundWorkerHelper(bool enableCancellation)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = enableCancellation;

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                WorkerError = e.Error.ToString();
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var action = e.Argument as Action;
            if (action != null)
            {
                action();
            }
        }

        public void StartTask(Action action)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync(action);
        }
        [Obsolete("This Method Run if Cancellation is enabled")]
        public void StopTask()
        {
            if (worker.WorkerSupportsCancellation)
                worker.CancelAsync();
        }
        public void Dispose()
        {
            worker.Dispose();
        }
    }
}
