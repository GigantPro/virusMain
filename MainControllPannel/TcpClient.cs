using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TcpClient
{
    private int serverTcpPort = 49001;



    public void UpgradeMyWorks(string @programm, string ip_str)
    {
        //var myIp = "127.0.0.1"/*give_locale_ip()*/;
        var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip_str), serverTcpPort);

        var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        FileInfo file = new FileInfo(programm);
        long lenght = file.Length;

        var message = "U"+ lenght.ToString();

        var data = Encoding.UTF8.GetBytes(message);
        tcpSocket.Connect(tcpEndPoint);
        tcpSocket.Send(data);
        tcpSocket.Shutdown(SocketShutdown.Both);
        tcpSocket.Close();
        Thread.Sleep(1000);
        var tcpSocketNew = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        tcpSocketNew.Connect(tcpEndPoint);
        tcpSocketNew.Send(File.ReadAllBytes(programm));

        tcpSocketNew.Shutdown(SocketShutdown.Both);
        tcpSocketNew.Close();
    }
    public IPAddress give_locale_ip()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var locale_ip in host.AddressList)
        {
            if (locale_ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return locale_ip;
            }
            return null;
        }
        return null;
    }
}
