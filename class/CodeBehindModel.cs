namespace CodeBehind
{
    public abstract class CodeBehindModel
    {
        public string ResponseText = "";

        public void Write(string Text)
        {
            ResponseText += Text;
        }
    }
}
