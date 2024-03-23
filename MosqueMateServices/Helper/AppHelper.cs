using System;
using System.Diagnostics;
using System.Windows;

namespace MosqueMateServices.Helper
{
    public class AppHelper
    {
        public static void RestartApp()
        {
            // Get the current process information
            Process currentProcess = Process.GetCurrentProcess();
            // Start a new instance of the WPF application
            Process.Start(currentProcess.MainModule.FileName);
            // Close the current instance
            currentProcess.Kill();
        }
    }
}
