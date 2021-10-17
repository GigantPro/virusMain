using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;

public class UdpServer
{
    // Информация о файле (требуется для получателя)
    [Serializable]
    public class FileDetails
    {
        public string FILETYPE = "";
        public long FILESIZE = 0;
    }

    private static FileDetails fileDet = new FileDetails();

    // Поля, связанные с UdpClient
    private static IPAddress remoteIPAddress;
    private const int remotePort = 5002;
    private static UdpClient sender = new UdpClient();
    private static IPEndPoint endPoint;

    // Filestream object
    private static FileStream fs;

    [STAThread]
    public void mainserver()
    {
        try
        {
            // Получаем удаленный IP-адрес и создаем IPEndPoint
            Console.WriteLine("Введите удаленный IP-адрес");
            remoteIPAddress = IPAddress.Parse(Console.ReadLine().ToString());//"127.0.0.1");
            endPoint = new IPEndPoint(remoteIPAddress, remotePort);

            // Получаем путь файла и его размер (должен быть меньше 8kb)
            Console.WriteLine("Введите путь к файлу и его имя");
            fs = new FileStream(@Console.ReadLine().ToString(), FileMode.Open, FileAccess.Read);

            //if (fs.Length > 8192)
            //{
            //    Console.Write("Файл должен весить меньше 8кБ");
            //    sender.Close();
            //    fs.Close();
            //    return;
            //}

            // Отправляем информацию о файле
            SendFileInfo();

            // Ждем 2 секунды
            Thread.Sleep(2000);

            // Отправляем сам файл
            SendFile();

            Console.ReadLine();

        }
        catch (Exception eR)
        {
            Console.WriteLine(eR.ToString());
        }
    }

    public static void SendFileInfo()
    {

        // Получаем тип и расширение файла
        fileDet.FILETYPE = fs.Name.Substring((int)fs.Name.Length - 3, 3);

        // Получаем длину файла
        fileDet.FILESIZE = fs.Length;

        XmlSerializer fileSerializer = new XmlSerializer(typeof(FileDetails));
        MemoryStream stream = new MemoryStream();

        // Сериализуем объект
        fileSerializer.Serialize(stream, fileDet);

        // Считываем поток в байты
        stream.Position = 0;
        Byte[] bytes = new Byte[stream.Length];
        stream.Read(bytes, 0, Convert.ToInt32(stream.Length));

        Console.WriteLine("Отправка деталей файла...");

        // Отправляем информацию о файле
        sender.Send(bytes, bytes.Length, endPoint);
        stream.Close();

    }

    private static void SendFile()
    {
        // Создаем файловый поток и переводим его в байты
        Byte[] bytes = new Byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);

        Console.WriteLine("Отправка файла размером " + fs.Length + " байт");
        try
        {
            // Отправляем файл
            sender.Send(bytes, bytes.Length, endPoint);
        }
        catch (Exception eR)
        {
            Console.WriteLine(eR.ToString());
        }
        finally
        {
            // Закрываем соединение и очищаем поток
            fs.Close();
            sender.Close();
        }
        Console.WriteLine("Файл успешно отправлен.");
        Console.Read();
    }
}
