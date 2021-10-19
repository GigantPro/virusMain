using System.Threading;


namespace virusMain
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Update upd = new Update();                                                      //переменная класса обновлений
            Thread t1 = new Thread(() => { upd.CheckForCommands(1, "127.0.0.1"); });        //Поток с запуском проверки обновлений

            //Запускаем вычисление потока
            t1.Start();
        }
    }
}
