using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MosqueMateServices.Helper
{
    public class WinFormHelper
    {
        public static void OpenAppLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                using NotificationHelper notification = new NotificationHelper(true);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
        }
    }
}
