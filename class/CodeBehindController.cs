namespace CodeBehind
{
    public abstract class CodeBehindController
    {
        public object CodeBehindModel { get; protected set; }
        public string ResponseText = "";
        public bool IgnoreViewAndModel = false;

        public void Write(string Text)
        {
            ResponseText += Text;
        }

        public void View(object ModelClass)
        {
            CodeBehindModel = ModelClass;
        }
    }
}
