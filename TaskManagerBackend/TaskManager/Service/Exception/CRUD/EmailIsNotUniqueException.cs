namespace TaskManager.Service.Exception.CRUD
{
    public class EmailIsNotUniqueException : System.Exception
    {
        public EmailIsNotUniqueException() { }

        public EmailIsNotUniqueException(string message) : base(message) { }

        public EmailIsNotUniqueException(string message, System.Exception inner) : base(message, inner) { }
    }
}
