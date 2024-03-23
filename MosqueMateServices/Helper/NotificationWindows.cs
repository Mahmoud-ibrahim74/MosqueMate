using MosqueMateMedia.Properties;
using MosqueMateServices.AppResources;
using MosqueMateServices.Interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MosqueMateServices.Helper
{
    public class NotificationWindows : INotificationWindows, IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private bool isShowOne = true;
        private bool notifySetting;
        ContextMenuStrip contextmenuStrip;
        ToolStripMenuItem StripMenuItemClose;
        ToolStripMenuItem StripMenuIteAction;
        Action action;
        public NotificationWindows(bool notifySetting, Action action = null)
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = Media.pray;
            _notifyIcon.Visible = true;
            this.notifySetting = notifySetting;
            contextmenuStrip = new ContextMenuStrip();
            StripMenuItemClose = new ToolStripMenuItem("Close");
            StripMenuIteAction = new ToolStripMenuItem("Restart App");
            StripMenuIteAction.Click += StripMenuIteAction_Click;
            StripMenuItemClose.Click += StripMenuItem_Click;
            contextmenuStrip.Items.Add(StripMenuIteAction);
            contextmenuStrip.Items.Add(StripMenuItemClose);
            _notifyIcon.ContextMenuStrip = contextmenuStrip;


            this.action = action;
        }

        private void StripMenuIteAction_Click(object sender, EventArgs e)
        {

            AppHelper.RestartApp();
        }

        private void StripMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
        }

        public void ShowNotification(string title, string message, ToolTipIcon icon)
        {
            try
            {
                if (isShowOne && notifySetting)
                {
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
        public new void Dispose()
        {
            _notifyIcon.Dispose();

        }

        public void ShowUpdateNotification()
        {
            try
            {
                using ResourceJsonRepo resource = new ResourceJsonRepo();
                if (isShowOne && notifySetting)
                {

                    _notifyIcon.ShowBalloonTip(
                                     60000,
                                        "update",
                                       resource["UpdateAviliable"],
                                    ToolTipIcon.Info
                                  );

                    isShowOne = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowWithAction()
        {

        }
    }
}
