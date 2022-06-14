namespace ToDoApp.Excteptions
{
    public class NotValidDateException: Exception
    {
        public NotValidDateException()
        {

        }

        public NotValidDateException(string message): base(message)
        {

        }

        public NotValidDateException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
