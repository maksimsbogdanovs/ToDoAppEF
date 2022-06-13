using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp
{
    public class HistoryFileLogger : ILogger
    {
        public void Log(string toDoLine)
        { 
            var lines = new string[] { toDoLine };
            File.AppendAllLines("toDoLogs.txt", lines);
        }
    }
}
