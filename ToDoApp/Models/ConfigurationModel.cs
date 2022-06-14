namespace ToDoApp
{
    public class ConfigurationModel
    {
        public static string AppName = "My Daily ToDo list";

        public static DateTime aDate = DateTime.Now;

        public static string TodaysDate()
        {
            return aDate.ToString("MMMM dd");
        }
    }
}
