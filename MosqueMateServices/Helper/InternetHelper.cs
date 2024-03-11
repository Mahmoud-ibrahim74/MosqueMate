using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Helper
{

    public class InternetHelper
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool InternetAvailableFromMachine()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
        public static bool InternetFromHost(string host = "google.com")
        {
            try
            {
                Ping myPing = new Ping();
                byte[] buffer = new byte[32];
                int timeout = 30;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}