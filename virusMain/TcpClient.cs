//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;

//public class TcpClient
//{
//    private string serverIp = "127.0.0.1";
//    private int serverTcpPort = 49001;
//    private string myIp = "127.0.0.1";



//    public void GiveEXE()
//    {
//        var tcpEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverTcpPort);

//        var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//        var message = myIp;

//        var data = Encoding.UTF8.GetBytes(message);

//        tcpSocket.Connect(tcpEndPoint);
//        tcpSocket.Send(data);

//        var buffer = new byte[256];
//        var size = 0;
//        new StringBuilder();

//        do
//        {
//            size = tcpSocket.Receive(buffer);
//            //var answer = buffer;
//        } while (tcpSocket.Available > 0);

//        var answ = System.Convert.ToInt32(Encoding.UTF8.GetString(buffer, 0, size));
//        var newBuffer = new byte[answ + 20];

//        do
//        {
//            size = tcpSocket.Receive(newBuffer);
//            //var answer = buffer;
//        } while (tcpSocket.Available > 0);

//        tcpSocket.Shutdown(SocketShutdown.Both);
//        tcpSocket.Close();

//        string outpath = @"C:\temp\test.exe";
//        File.WriteAllBytes(outpath, newBuffer);
//    }
//}
