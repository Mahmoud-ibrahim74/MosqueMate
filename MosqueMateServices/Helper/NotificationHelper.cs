using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MosqueMateServices.Helper
{
    public class NotificationHelper : INotificationWindows, IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private bool isShowOne = true;
        private bool notifySetting;
        public NotificationHelper(bool notifySetting)
        {
            _notifyIcon = new NotifyIcon();
            this.notifySetting = notifySetting; 
        }

        public void ShowNotification(string title, string message, ToolTipIcon icon)
        {
            try
            {
                if (isShowOne && notifySetting)
                {
                    _notifyIcon.Icon = Media.prayerICO;
                    _notifyIcon.Visible = true;
                    _notifyIcon.ShowBalloonTip(
                                     60000,
                                        title,
                                     message,
                                    icon
                                  );
                    isShowOne = false;  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Dispose()
        {
            //_notifyIcon.Dispose();
        }

        public void ShowUpdateNotification()
        {
            try
            {
                using ResourceJsonRepo resource = new ResourceJsonRepo();   
                if (isShowOne && notifySetting)
                {
                    _notifyIcon.Icon = Media.prayerICO;
                    _notifyIcon.Visible = true;
                    _notifyIcon.ShowBalloonTip(
                                     60000,
                                        "update",
                                       resource["UpdateAviliable"],
                                    ToolTipIcon.Info 
                                  );
                    _notifyIcon.BalloonTipClicked += _notifyIcon_BalloonTipClicked;
                    isShowOne = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            WinFormHelper.OpenAppLink("https://github.com/Mahmoud-ibrahim74");
        }
    }
}
