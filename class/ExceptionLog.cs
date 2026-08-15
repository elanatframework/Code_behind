namespace CodeBehind
{
    public class ExceptionLog
    {
        public ExceptionLog(Exception ex)
        {
            Console.WriteLine("CodeBehind Error:");
            Console.WriteLine(ex);
        }
    }
}