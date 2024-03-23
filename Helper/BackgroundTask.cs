using MosqueMate.Properties;
using MosqueMateServices.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TagLib.Ogg;
using Application = System.Windows.Application;
using Page = System.Windows.Controls.Page;
namespace MosqueMate.Helper
{
    public static class BackgroundTask
    {
        #region OldCode
        //public static async void ExecuteUIThreadAsync(Action background_action, Action UIaction, MaterialDesignThemes.Wpf.Card cardContainer)
        //{
        //    try
        //    {
        //        cardContainer.IsEnabled = false;
        //        #region Run Dispatcher with UI Thread
        //        await Task.Run(() =>
        //        {
        //            background_action();
        //            // Update the UI using the Dispatcher when needed
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                UIaction();
        //            });
        //        });
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        new NotificationSettings().ShowNotificationAsync("error", ex.Message, Notifications.Wpf.Core.NotificationType.Error);
        //        cardContainer.IsEnabled = true;
        //    }
        //    finally
        //    {
        //        cardContainer.IsEnabled = true;
        //        
        //    }
        //}
        //public static async void ExecuteUIThreadAsync(Action background_action, Action UIaction, Button button)
        //{
        //    try
        //    {
        //        button.IsEnabled = false;
        //        #region Run Dispatcher with UI Thread
        //        await Task.Run(() =>
        //        {
        //            background_action();
        //            // Update the UI using the Dispatcher when needed
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                UIaction();
        //            });
        //        });
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        new NotificationSettings().ShowNotificationAsync("Error", ex.Message, Notifications.Wpf.Core.NotificationType.Error);
        //        button.IsEnabled = true;
        //    }
        //    finally
        //    {
        //        button.IsEnabled = true;
        //        

        //    }
        //}
        //public static async void ExecuteOneUIThread(Action UIaction, Button button)
        //{
        //    try
        //    {
        //        button.IsEnabled = false;
        //        #region Run Dispatcher with UI Thread
        //        await Task.Run(() =>
        //        {
        //            // Update the UI using the Dispatcher when needed
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                UIaction();
        //            });
        //        });
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        new NotificationSettings().ShowNotificationAsync("Error", ex.Message, Notifications.Wpf.Core.NotificationType.Error);
        //        button.IsEnabled = true;
        //    }
        //    finally
        //    {
        //        button.IsEnabled = true;
        //    }
        //} 
        #endregion
        public static async void ExecuteThreadUI(Action UIaction)
        {
            try
            {
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    // Update the UI using the Dispatcher when needed
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UIaction();
                    });
                });
                #endregion
            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
        }
        public static async void ExecuteThreadUI(Action UIaction,FrameworkElement page)
        {
            try
            {
                page.IsEnabled = false;
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    // Update the UI using the Dispatcher when needed
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UIaction();
                    });
                });
                #endregion
            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                page.IsEnabled = true;

            }
            finally
            {
                page.IsEnabled = true;

            }
        }
        public static async void ExecuteThreadUI(Action UIaction, ContentControl window)
        {
            try
            {
                window.IsEnabled = false;
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    // Update the UI using the Dispatcher when needed
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        UIaction();
                    });
                });
                #endregion
            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(Settings.Default.notification);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
            finally
            {
                window.IsEnabled = true;
            }
        }
        public static async void ExecuteThreadUI(Action UIaction, Control control)
        {
            try
            {
                control.IsEnabled = false;
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                 {
                     // Update the UI using the Dispatcher when needed
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         UIaction();
                     });
                 });
                #endregion
            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                control.IsEnabled = false;
            }
            finally
            {
                control.IsEnabled = false;
            }
        }
        public async static void ExecuteNormalTask(Action UIaction)
        {
            try
            {
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    UIaction();
                });
                #endregion

            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
            }
        }

        public async static void ExecuteNormalTaskWithDisable(Action UIaction, Control control)
        {
            try
            {
                control.IsEnabled = false;
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    UIaction();
                });
                #endregion

            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                control.IsEnabled = true;
            }
            finally
            {
                control.IsEnabled = true;
            }
        }
        public async static void ExecuteNormalTaskWithDisable(Action UIaction, Page page)
        {
            try
            {
                page.IsEnabled = false;
                #region Run Dispatcher with UI Thread
                await Task.Run(() =>
                {
                    UIaction();
                });
                #endregion

            }
            catch (Exception ex)
            {
                using NotificationWindows notification = new NotificationWindows(false);
                notification.ShowNotification("Error", ex.Message, System.Windows.Forms.ToolTipIcon.Error);
                page.IsEnabled = true;
            }
            finally
            {
                page.IsEnabled = true;
            }
        }

        public static async Task<string> RunClockAsync(int Milliseconds)
        {
            while (true)
            {
                await Task.Delay(Milliseconds);
                return DateTime.Now.ToString("hh:mm:ss tt");
            }
        }
        public static async void RunTaskWithWhile(int Milliseconds, Action action)
        {
            while (true)
            {
                await Task.Delay(Milliseconds);
                action();
            }
        }
        public static async void RunTaskThreadUIWithWhile(int Milliseconds, Action action)
        {
            while (true)
            {
                await Task.Delay(Milliseconds);
                ExecuteThreadUI(action);
            }
        }
   
    }
}
