using System;
using System.Windows.Forms;

namespace MosqueMateServices.Interfaces
{
    public interface INotificationWindows
    {
        public void ShowNotification(string title, string message, ToolTipIcon icon);
        public void ShowUpdateNotification();
        public void ShowWithAction();
    }
}
