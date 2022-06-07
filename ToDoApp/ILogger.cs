using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp
{
    internal interface ILogger
    {
        void Log(ToDoListModel toDoListModel, ToDoContext context);
    }
}
