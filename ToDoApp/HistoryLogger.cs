using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp
{
    public class HistoryLogger : ILogger
    {
        public void Log(ToDoListModel toDoListModel, ToDoContext context)
        {
            context.Add(toDoListModel);
            context.SaveChanges();
        }
    }
}
