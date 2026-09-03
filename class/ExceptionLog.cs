namespace CodeBehind
{
    public class ExceptionLog
    {
        public ExceptionLog(Exception ex)
        {
            Console.WriteLine("CodeBehind Error:");
            Console.WriteLine(ex);

            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception:");
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
