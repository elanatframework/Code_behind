namespace CodeBehind
{
    public static class StaticObject
    {
        private static bool StaticObjectHasInitialization { get; set; } = false;
        public static bool PreventAccessDefaultAspx { get; private set; } = false;

        public static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();
            PreventAccessDefaultAspx = options.PreventAccessDefaultAspx;

            StaticObjectHasInitialization = true;
        }
    }
}
