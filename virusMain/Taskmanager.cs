
using Microsoft.Win32.TaskScheduler;

namespace virusMain
{
    class Taskmanager
    {
        public void create(string name_of_task, short time_day, bool delete)
        {
            using (TaskService ts = new TaskService())
            {
                if (delete == false)
                {

                    // Создайте новое определение задачи и назначьте свойства
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "open";

                    // Создайте триггер, который будет запускать задачу в это время через день
                    td.Triggers.Add(new DailyTrigger { DaysInterval = time_day });

                    // Создайте действие, которое будет запускать Блокнот при срабатывании триггера.
                    td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

                    // Зарегистрируйте задачу в корневой папке
                    ts.RootFolder.RegisterTaskDefinition(@name_of_task, td);
                }
                else
                {
                    ts.RootFolder.DeleteTask(name_of_task);
                }
            }
        }

        public void create(string name_of_task, short time_day, string programm_name_exe, string programm_road_open, bool delete)
        {
            using (TaskService ts = new TaskService())
            {
                if (delete == false)
                {

                    // Создайте новое определение задачи и назначьте свойства
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "open";

                    // Создайте триггер, который будет запускать задачу в это время через день
                    td.Triggers.Add(new DailyTrigger { DaysInterval = time_day });

                    // Создайте действие, которое будет запускать Блокнот при срабатывании триггера.
                    td.Actions.Add(new ExecAction(programm_name_exe, programm_road_open, null));

                    // Зарегистрируйте задачу в корневой папке
                    ts.RootFolder.RegisterTaskDefinition(@name_of_task, td);
                }
                else
                {
                    ts.RootFolder.DeleteTask(name_of_task);
                }
            }
        }
    }
}
