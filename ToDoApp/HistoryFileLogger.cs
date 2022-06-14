using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp
{
    public class HistoryFileLogger : ILogger
    {
        public void Log(string toDoLine)
        { 
            var lines = new string[] { toDoLine };
            if (File.Exists("toDoLogs.txt"))
            {
                File.AppendAllLines("toDoLogs.txt", lines);
            }
            else
            {
                File.WriteAllLines("toDoLogs.txt", lines);
            }
                
        }
    }
}
