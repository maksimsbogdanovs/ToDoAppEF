namespace ToDoApp.Excteptions
{
    public class NotValidTextException: Exception
    {
        public NotValidTextException()
        {

        }

        public NotValidTextException(string message): base(message)
        {

        }

        public NotValidTextException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
